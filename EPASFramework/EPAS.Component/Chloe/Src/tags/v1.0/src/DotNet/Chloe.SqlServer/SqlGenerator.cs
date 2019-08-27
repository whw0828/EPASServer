using Chloe.Core;
using Chloe.DbExpressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chloe.SqlServer
{
    partial class SqlGenerator : DbExpressionVisitor<DbExpression>
    {
        public const string ParameterPrefix = "@P_";

        internal ISqlBuilder _sqlBuilder = new SqlBuilder();
        List<DbParam> _parameters = new List<DbParam>();

        DbValueExpressionVisitor _valueExpressionVisitor = null;

        static readonly Dictionary<string, Action<DbMethodCallExpression, SqlGenerator>> MethodHandlers = InitMethodHandlers();
        static readonly Dictionary<string, Action<DbAggregateExpression, SqlGenerator>> AggregateHandlers = InitAggregateHandlers();
        static readonly Dictionary<MethodInfo, Action<DbBinaryExpression, SqlGenerator>> BinaryWithMethodHandlers = InitBinaryWithMethodHandlers();
        static readonly Dictionary<Type, string> CastTypeMap = null;

        public static readonly ReadOnlyCollection<DbExpressionType> SafeDbExpressionTypes = null;

        static readonly List<string> CacheParameterNames = null;

        static SqlGenerator()
        {
            List<DbExpressionType> list = new List<DbExpressionType>();
            list.Add(DbExpressionType.MemberAccess);
            list.Add(DbExpressionType.ColumnAccess);
            list.Add(DbExpressionType.Constant);
            list.Add(DbExpressionType.Parameter);
            list.Add(DbExpressionType.Convert);
            SafeDbExpressionTypes = list.AsReadOnly();

            Dictionary<Type, string> castTypeMap = new Dictionary<Type, string>();
            castTypeMap.Add(typeof(string), "NVARCHAR(MAX)");
            castTypeMap.Add(typeof(byte), "TINYINT");
            castTypeMap.Add(typeof(Int16), "SMALLINT");
            castTypeMap.Add(typeof(int), "INT");
            castTypeMap.Add(typeof(long), "BIGINT");
            castTypeMap.Add(typeof(decimal), "DECIMAL(19,0)");//I think this will be a bug.
            castTypeMap.Add(typeof(double), "FLOAT");
            castTypeMap.Add(typeof(float), "REAL");
            castTypeMap.Add(typeof(bool), "BIT");
            castTypeMap.Add(typeof(DateTime), "DATETIME");
            castTypeMap.Add(typeof(Guid), "UNIQUEIDENTIFIER");

            CastTypeMap = Utils.Clone(castTypeMap);

            int cacheParameterNameCount = 2 * 12;
            List<string> cacheParameterNames = new List<string>(cacheParameterNameCount);

            for (int i = 0; i < cacheParameterNameCount; i++)
            {
                string paramName = ParameterPrefix + i.ToString();
                cacheParameterNames.Add(paramName);
            }

            CacheParameterNames = cacheParameterNames;
        }

        public ISqlBuilder SqlBuilder { get { return this._sqlBuilder; } }
        public List<DbParam> Parameters { get { return this._parameters; } }

        DbValueExpressionVisitor ValueExpressionVisitor
        {
            get
            {
                if (this._valueExpressionVisitor == null)
                    this._valueExpressionVisitor = new DbValueExpressionVisitor(this);

                return this._valueExpressionVisitor;
            }
        }

        public static SqlGenerator CreateInstance()
        {
            return new SqlGenerator();
        }

        public override DbExpression Visit(DbEqualExpression exp)
        {
            DbExpression left = exp.Left;
            DbExpression right = exp.Right;

            left = DbExpressionExtensions.ParseDbExpression(left);
            right = DbExpressionExtensions.ParseDbExpression(right);

            //明确 left right 其中一边一定为 null
            if (DbExpressionExtensions.AffirmExpressionRetValueIsNull(right))
            {
                left.Accept(this);
                this._sqlBuilder.Append(" IS NULL");
                return exp;
            }

            if (DbExpressionExtensions.AffirmExpressionRetValueIsNull(left))
            {
                right.Accept(this);
                this._sqlBuilder.Append(" IS NULL");
                return exp;
            }

            left.Accept(this);
            this._sqlBuilder.Append(" = ");
            right.Accept(this);

            return exp;
        }
        public override DbExpression Visit(DbNotEqualExpression exp)
        {
            DbExpression left = exp.Left;
            DbExpression right = exp.Right;

            left = DbExpressionExtensions.ParseDbExpression(left);
            right = DbExpressionExtensions.ParseDbExpression(right);

            //明确 left right 其中一边一定为 null
            if (DbExpressionExtensions.AffirmExpressionRetValueIsNull(right))
            {
                left.Accept(this);
                this._sqlBuilder.Append(" IS NOT NULL");
                return exp;
            }

            if (DbExpressionExtensions.AffirmExpressionRetValueIsNull(left))
            {
                right.Accept(this);
                this._sqlBuilder.Append(" IS NOT NULL");
                return exp;
            }

            left.Accept(this);
            this._sqlBuilder.Append(" <> ");
            right.Accept(this);

            return exp;
        }

        public override DbExpression Visit(DbNotExpression exp)
        {
            this._sqlBuilder.Append("NOT ");
            this._sqlBuilder.Append("(");
            exp.Operand.Accept(this);
            this._sqlBuilder.Append(")");

            return exp;
        }

        public override DbExpression Visit(DbAndExpression exp)
        {
            Stack<DbExpression> operands = GatherBinaryExpressionOperand(exp);
            this.ConcatOperands(operands, " & ");

            return exp;
        }
        public override DbExpression Visit(DbAndAlsoExpression exp)
        {
            Stack<DbExpression> operands = GatherBinaryExpressionOperand(exp);
            this.ConcatOperands(operands, " AND ");

            return exp;
        }
        public override DbExpression Visit(DbOrExpression exp)
        {
            Stack<DbExpression> operands = GatherBinaryExpressionOperand(exp);
            this.ConcatOperands(operands, " | ");

            return exp;
        }
        public override DbExpression Visit(DbOrElseExpression exp)
        {
            Stack<DbExpression> operands = GatherBinaryExpressionOperand(exp);
            this.ConcatOperands(operands, " OR ");

            return exp;
        }

        public override DbExpression Visit(DbConvertExpression exp)
        {
            DbExpression stripedExp = DbExpressionHelper.StripInvalidConvert(exp);

            if (stripedExp.NodeType != DbExpressionType.Convert)
            {
                EnsureDbExpressionReturnCSharpBoolean(stripedExp).Accept(this);
                return exp;
            }

            exp = (DbConvertExpression)stripedExp;

            string dbTypeString;
            if (TryGetCastTargetDbTypeString(exp.Operand.Type, exp.Type, out dbTypeString))
            {
                this.BuildCastState(EnsureDbExpressionReturnCSharpBoolean(exp.Operand), dbTypeString);
            }
            else
                EnsureDbExpressionReturnCSharpBoolean(exp.Operand).Accept(this);

            return exp;
        }
        // +
        public override DbExpression Visit(DbAddExpression exp)
        {
            MethodInfo method = exp.Method;
            if (method != null)
            {
                Action<DbBinaryExpression, SqlGenerator> handler;
                if (BinaryWithMethodHandlers.TryGetValue(method, out handler))
                {
                    handler(exp, this);
                    return exp;
                }

                throw UtilExceptions.NotSupportedMethod(exp.Method);
            }

            Stack<DbExpression> operands = GatherBinaryExpressionOperand(exp);
            this.ConcatOperands(operands, " + ");

            return exp;
        }
        // -
        public override DbExpression Visit(DbSubtractExpression exp)
        {
            Stack<DbExpression> operands = GatherBinaryExpressionOperand(exp);
            this.ConcatOperands(operands, " - ");

            return exp;
        }
        // *
        public override DbExpression Visit(DbMultiplyExpression exp)
        {
            Stack<DbExpression> operands = GatherBinaryExpressionOperand(exp);
            this.ConcatOperands(operands, " * ");

            return exp;
        }
        // /
        public override DbExpression Visit(DbDivideExpression exp)
        {
            Stack<DbExpression> operands = GatherBinaryExpressionOperand(exp);
            this.ConcatOperands(operands, " / ");

            return exp;
        }
        // <
        public override DbExpression Visit(DbLessThanExpression exp)
        {
            exp.Left.Accept(this);
            this._sqlBuilder.Append(" < ");
            exp.Right.Accept(this);

            return exp;
        }
        // <=
        public override DbExpression Visit(DbLessThanOrEqualExpression exp)
        {
            exp.Left.Accept(this);
            this._sqlBuilder.Append(" <= ");
            exp.Right.Accept(this);

            return exp;
        }
        // >
        public override DbExpression Visit(DbGreaterThanExpression exp)
        {
            exp.Left.Accept(this);
            this._sqlBuilder.Append(" > ");
            exp.Right.Accept(this);

            return exp;
        }
        // >=
        public override DbExpression Visit(DbGreaterThanOrEqualExpression exp)
        {
            exp.Left.Accept(this);
            this._sqlBuilder.Append(" >= ");
            exp.Right.Accept(this);

            return exp;
        }

        public override DbExpression Visit(DbConstantExpression exp)
        {
            if (exp.Value == null || exp.Value == DBNull.Value)
            {
                this._sqlBuilder.Append("NULL");
                return exp;
            }

            var objType = exp.Value.GetType();
            if (objType == UtilConstants.TypeOfBoolean)
            {
                this._sqlBuilder.Append(((bool)exp.Value) ? "CAST(1 AS BIT)" : "CAST(0 AS BIT)");
                return exp;
            }
            else if (objType == UtilConstants.TypeOfString)
            {
                this._sqlBuilder.Append("N'", exp.Value, "'");
                return exp;
            }
            else if (objType.IsEnum)
            {
                this._sqlBuilder.Append(((int)exp.Value).ToString());
                return exp;
            }

            this._sqlBuilder.Append(exp.Value);
            return exp;
        }

        // then 部分必须返回 C# type，所以得判断是否是诸如 a>1,a=b,in,like 等等的情况，如果是则将其构建成一个 case when 
        public override DbExpression Visit(DbCaseWhenExpression exp)
        {
            this._sqlBuilder.Append("CASE");
            foreach (var whenThen in exp.WhenThenPairs)
            {
                // then 部分得判断是否是诸如 a>1,a=b,in,like 等等的情况，如果是则将其构建成一个 case when 
                this._sqlBuilder.Append(" WHEN ");
                whenThen.When.Accept(this);
                this._sqlBuilder.Append(" THEN ");
                EnsureDbExpressionReturnCSharpBoolean(whenThen.Then).Accept(this);
            }

            this._sqlBuilder.Append(" ELSE ");
            EnsureDbExpressionReturnCSharpBoolean(exp.Else).Accept(this);
            this._sqlBuilder.Append(" END");

            return exp;
        }

        public override DbExpression Visit(DbTableExpression exp)
        {
            this.QuoteName(exp.Table.Name);

            return exp;
        }

        public override DbExpression Visit(DbColumnAccessExpression exp)
        {
            this.QuoteName(exp.Table.Name);
            this._sqlBuilder.Append(".");
            this.QuoteName(exp.Column.Name);

            return exp;
        }

        public override DbExpression Visit(DbMemberExpression exp)
        {
            MemberInfo member = exp.Member;

            if (member.DeclaringType == UtilConstants.TypeOfDateTime)
            {
                if (member == UtilConstants.PropertyInfo_DateTime_Now)
                {
                    this._sqlBuilder.Append("GETDATE()");
                    return exp;
                }

                if (member == UtilConstants.PropertyInfo_DateTime_UtcNow)
                {
                    this._sqlBuilder.Append("GETUTCDATE()");
                    return exp;
                }

                if (member == UtilConstants.PropertyInfo_DateTime_Today)
                {
                    BuildCastState("GETDATE()", "DATE");
                    return exp;
                }

                if (member == UtilConstants.PropertyInfo_DateTime_Date)
                {
                    BuildCastState(exp.Expression, "DATE");
                    return exp;
                }

                if (IsDbFunction_DATEPART(exp))
                {
                    return exp;
                }
            }


            DbParameterExpression newExp;
            if (DbExpressionExtensions.TryParseToParameterExpression(exp, out newExp))
            {
                return newExp.Accept(this);
            }

            if (member.Name == "Length" && member.DeclaringType == UtilConstants.TypeOfString)
            {
                this._sqlBuilder.Append("LEN(");
                exp.Expression.Accept(this);
                this._sqlBuilder.Append(")");

                return exp;
            }
            else if (member.Name == "Value" && Utils.IsNullable(exp.Expression.Type))
            {
                exp.Expression.Accept(this);
                return exp;
            }

            throw new NotSupportedException(string.Format("'{0}.{1}' is not supported.", member.DeclaringType.FullName, member.Name));
        }
        public override DbExpression Visit(DbParameterExpression exp)
        {
            object val = exp.Value;
            if (val == null)
                val = DBNull.Value;

            string paramName;

            DbParam p;
            if (val == DBNull.Value)
            {
                p = this._parameters.Where(a => Utils.AreEqual(a.Value, val) && a.Type == exp.Type).FirstOrDefault();
            }
            else
                p = this._parameters.Where(a => Utils.AreEqual(a.Value, val)).FirstOrDefault();

            if (p != null)
            {
                this._sqlBuilder.Append(p.Name);
                return exp;
            }

            paramName = GenParameterName(this._parameters.Count);
            this._parameters.Add(DbParam.Create(paramName, val, exp.Type));
            this._sqlBuilder.Append(paramName);
            return exp;
        }

        public override DbExpression Visit(DbSubQueryExpression exp)
        {
            this._sqlBuilder.Append("(");
            exp.SqlQuery.Accept(this);
            this._sqlBuilder.Append(")");

            return exp;
        }
        public override DbExpression Visit(DbSqlQueryExpression exp)
        {
            if (exp.SkipCount != null)
            {
                this.BuildLimitSql(exp);
                return exp;
            }
            else
            {
                //构建常规的查询
                this.BuildGeneralSql(exp);
                return exp;
            }

            throw new NotImplementedException();
        }

        public override DbExpression Visit(DbMethodCallExpression exp)
        {
            Action<DbMethodCallExpression, SqlGenerator> methodHandler;
            if (!MethodHandlers.TryGetValue(exp.Method.Name, out methodHandler))
            {
                throw UtilExceptions.NotSupportedMethod(exp.Method);
            }

            methodHandler(exp, this);
            return exp;
        }

        public override DbExpression Visit(DbFromTableExpression exp)
        {
            this.AppendTableSegment(exp.Table);
            this.VisitDbJoinTableExpressions(exp.JoinTables);

            return exp;
        }

        public override DbExpression Visit(DbJoinTableExpression exp)
        {
            DbJoinTableExpression joinTablePart = exp;
            string joinString = null;

            if (joinTablePart.JoinType == JoinType.InnerJoin)
            {
                joinString = " INNER JOIN ";
            }
            else if (joinTablePart.JoinType == JoinType.LeftJoin)
            {
                joinString = " LEFT JOIN ";
            }
            else if (joinTablePart.JoinType == JoinType.RightJoin)
            {
                joinString = " RIGHT JOIN ";
            }
            else if (joinTablePart.JoinType == JoinType.FullJoin)
            {
                joinString = " FULL JOIN ";
            }
            else
                throw new NotSupportedException("JoinType: " + joinTablePart.JoinType);

            this._sqlBuilder.Append(joinString);
            this.AppendTableSegment(joinTablePart.Table);
            this._sqlBuilder.Append(" ON ");
            joinTablePart.Condition.Accept(this);
            this.VisitDbJoinTableExpressions(joinTablePart.JoinTables);

            return exp;
        }

        public override DbExpression Visit(DbAggregateExpression exp)
        {
            Action<DbAggregateExpression, SqlGenerator> aggregateHandler;
            if (!AggregateHandlers.TryGetValue(exp.Method.Name, out aggregateHandler))
            {
                throw UtilExceptions.NotSupportedMethod(exp.Method);
            }

            aggregateHandler(exp, this);
            return exp;
        }

        public override DbExpression Visit(DbInsertExpression exp)
        {
            this._sqlBuilder.Append("INSERT INTO ");
            this.QuoteName(exp.Table.Name);
            this._sqlBuilder.Append("(");

            bool first = true;
            foreach (var item in exp.InsertColumns)
            {
                if (first)
                    first = false;
                else
                {
                    this._sqlBuilder.Append(",");
                }

                this.QuoteName(item.Key.Name);
            }

            this._sqlBuilder.Append(")");

            this._sqlBuilder.Append(" VALUES(");
            first = true;
            foreach (var item in exp.InsertColumns)
            {
                if (first)
                    first = false;
                else
                {
                    this._sqlBuilder.Append(",");
                }

                item.Value.Accept(this.ValueExpressionVisitor);
            }

            this._sqlBuilder.Append(")");

            return exp;
        }
        public override DbExpression Visit(DbUpdateExpression exp)
        {
            this._sqlBuilder.Append("UPDATE ");
            this.QuoteName(exp.Table.Name);
            this._sqlBuilder.Append(" SET ");

            bool first = true;
            foreach (var item in exp.UpdateColumns)
            {
                if (first)
                    first = false;
                else
                    this._sqlBuilder.Append(",");

                this.QuoteName(item.Key.Name);
                this._sqlBuilder.Append("=");
                item.Value.Accept(this.ValueExpressionVisitor);
            }

            this.BuildWhereState(exp.Condition);

            return exp;
        }
        public override DbExpression Visit(DbDeleteExpression exp)
        {
            this._sqlBuilder.Append("DELETE ");
            this.QuoteName(exp.Table.Name);
            this.BuildWhereState(exp.Condition);

            return exp;
        }


        void AppendTableSegment(DbTableSegment seg)
        {
            seg.Body.Accept(this);
            this._sqlBuilder.Append(" AS ");
            this.QuoteName(seg.Alias);
        }
        internal void AppendColumnSegment(DbColumnSegment seg)
        {
            seg.Body.Accept(this.ValueExpressionVisitor);
            this._sqlBuilder.Append(" AS ");
            this.QuoteName(seg.Alias);
        }
        void AppendOrdering(DbOrdering ordering)
        {
            if (ordering.OrderType == OrderType.Asc)
            {
                ordering.Expression.Accept(this);
                this._sqlBuilder.Append(" ASC");
                return;
            }
            else if (ordering.OrderType == OrderType.Desc)
            {
                ordering.Expression.Accept(this);
                this._sqlBuilder.Append(" DESC");
                return;
            }

            throw new NotSupportedException("OrderType: " + ordering.OrderType);
        }

        void VisitDbJoinTableExpressions(List<DbJoinTableExpression> tables)
        {
            foreach (var table in tables)
            {
                table.Accept(this);
            }
        }
        void BuildGeneralSql(DbSqlQueryExpression exp)
        {
            this._sqlBuilder.Append("SELECT ");
            if (exp.TakeCount != null)
                this._sqlBuilder.Append("TOP (", exp.TakeCount.ToString(), ") ");

            List<DbColumnSegment> columns = exp.ColumnSegments;
            for (int i = 0; i < columns.Count; i++)
            {
                DbColumnSegment column = columns[i];
                if (i > 0)
                    this._sqlBuilder.Append(",");

                this.AppendColumnSegment(column);
            }

            this._sqlBuilder.Append(" FROM ");
            exp.Table.Accept(this);
            this.BuildWhereState(exp.Condition);
            this.BuildGroupState(exp);
            this.BuildOrderState(exp.Orderings);
        }
        protected virtual void BuildLimitSql(DbSqlQueryExpression exp)
        {
            this._sqlBuilder.Append("SELECT ");
            if (exp.TakeCount != null)
                this._sqlBuilder.Append("TOP (", exp.TakeCount.ToString(), ") ");

            string tableAlias = "T";

            List<DbColumnSegment> columns = exp.ColumnSegments;
            for (int i = 0; i < columns.Count; i++)
            {
                DbColumnSegment column = columns[i];
                if (i > 0)
                    this._sqlBuilder.Append(",");

                this.QuoteName(tableAlias);
                this._sqlBuilder.Append(".");
                this.QuoteName(column.Alias);
                this._sqlBuilder.Append(" AS ");
                this.QuoteName(column.Alias);
            }

            this._sqlBuilder.Append(" FROM ");
            this._sqlBuilder.Append("(");

            //------------------------//
            this._sqlBuilder.Append("SELECT ");
            for (int i = 0; i < columns.Count; i++)
            {
                DbColumnSegment column = columns[i];
                if (i > 0)
                    this._sqlBuilder.Append(",");

                column.Body.Accept(this.ValueExpressionVisitor);
                this._sqlBuilder.Append(" AS ");
                this.QuoteName(column.Alias);
            }

            List<DbOrdering> orderings = exp.Orderings;
            if (orderings.Count == 0)
            {
                DbOrdering ordering = new DbOrdering(UtilConstants.DbParameter_1, OrderType.Asc);
                orderings = new List<DbOrdering>(1);
                orderings.Add(ordering);
            }

            string row_numberName = CreateRowNumberName(columns);
            this._sqlBuilder.Append(",ROW_NUMBER() OVER(ORDER BY ");
            this.ConcatOrderings(orderings);
            this._sqlBuilder.Append(") AS ");
            this.QuoteName(row_numberName);
            this._sqlBuilder.Append(" FROM ");
            exp.Table.Accept(this);
            this.BuildWhereState(exp.Condition);
            this.BuildGroupState(exp);
            //------------------------//

            this._sqlBuilder.Append(")");
            this._sqlBuilder.Append(" AS ");
            this.QuoteName(tableAlias);
            this._sqlBuilder.Append(" WHERE ");
            this.QuoteName(tableAlias);
            this._sqlBuilder.Append(".");
            this.QuoteName(row_numberName);
            this._sqlBuilder.Append(" > ");
            this._sqlBuilder.Append(exp.SkipCount.ToString());
        }


        internal void BuildWhereState(DbExpression whereExpression)
        {
            if (whereExpression != null)
            {
                this._sqlBuilder.Append(" WHERE ");
                whereExpression.Accept(this);
            }
        }
        internal void BuildOrderState(List<DbOrdering> orderings)
        {
            if (orderings.Count > 0)
            {
                this._sqlBuilder.Append(" ORDER BY ");
                this.ConcatOrderings(orderings);
            }
        }
        void ConcatOrderings(List<DbOrdering> orderings)
        {
            for (int i = 0; i < orderings.Count; i++)
            {
                if (i > 0)
                {
                    this._sqlBuilder.Append(",");
                }

                this.AppendOrdering(orderings[i]);
            }
        }
        internal void BuildGroupState(DbSqlQueryExpression exp)
        {
            var groupSegments = exp.GroupSegments;
            if (groupSegments.Count == 0)
                return;

            this._sqlBuilder.Append(" GROUP BY ");
            for (int i = 0; i < groupSegments.Count; i++)
            {
                if (i > 0)
                    this._sqlBuilder.Append(",");

                groupSegments[i].Accept(this);
            }

            if (exp.HavingCondition != null)
            {
                this._sqlBuilder.Append(" HAVING ");
                exp.HavingCondition.Accept(this);
            }
        }

        void ConcatOperands(IEnumerable<DbExpression> operands, string connector)
        {
            this._sqlBuilder.Append("(");

            bool first = true;
            foreach (DbExpression operand in operands)
            {
                if (first)
                    first = false;
                else
                    this._sqlBuilder.Append(connector);

                operand.Accept(this);
            }

            this._sqlBuilder.Append(")");
            return;
        }

        void QuoteName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name");

            this._sqlBuilder.Append("[", name, "]");
        }

        void BuildCastState(DbExpression castExp, string targetDbTypeString)
        {
            this._sqlBuilder.Append("CAST(");
            castExp.Accept(this);
            this._sqlBuilder.Append(" AS ", targetDbTypeString, ")");
        }
        void BuildCastState(object castObject, string targetDbTypeString)
        {
            this._sqlBuilder.Append("CAST(", castObject, " AS ", targetDbTypeString, ")");
        }

        bool IsDbFunction_DATEPART(DbMemberExpression exp)
        {
            MemberInfo member = exp.Member;

            if (member == UtilConstants.PropertyInfo_DateTime_Year)
            {
                DbFunction_DATEPART(this, "YEAR", exp.Expression);
                return true;
            }

            if (member == UtilConstants.PropertyInfo_DateTime_Month)
            {
                DbFunction_DATEPART(this, "MONTH", exp.Expression);
                return true;
            }

            if (member == UtilConstants.PropertyInfo_DateTime_Day)
            {
                DbFunction_DATEPART(this, "DAY", exp.Expression);
                return true;
            }

            if (member == UtilConstants.PropertyInfo_DateTime_Hour)
            {
                DbFunction_DATEPART(this, "HOUR", exp.Expression);
                return true;
            }

            if (member == UtilConstants.PropertyInfo_DateTime_Minute)
            {
                DbFunction_DATEPART(this, "MINUTE", exp.Expression);
                return true;
            }

            if (member == UtilConstants.PropertyInfo_DateTime_Second)
            {
                DbFunction_DATEPART(this, "SECOND", exp.Expression);
                return true;
            }

            if (member == UtilConstants.PropertyInfo_DateTime_Millisecond)
            {
                DbFunction_DATEPART(this, "MILLISECOND", exp.Expression);
                return true;
            }

            if (member == UtilConstants.PropertyInfo_DateTime_DayOfWeek)
            {
                this._sqlBuilder.Append("(");
                DbFunction_DATEPART(this, "WEEKDAY", exp.Expression);
                this._sqlBuilder.Append(" - 1)");

                return true;
            }

            return false;
        }



        #region BinaryWithMethodHandlers

        static Dictionary<MethodInfo, Action<DbBinaryExpression, SqlGenerator>> InitBinaryWithMethodHandlers()
        {
            var binaryWithMethodHandlers = new Dictionary<MethodInfo, Action<DbBinaryExpression, SqlGenerator>>();
            binaryWithMethodHandlers.Add(UtilConstants.MethodInfo_String_Concat_String_String, StringConcat);
            binaryWithMethodHandlers.Add(UtilConstants.MethodInfo_String_Concat_Object_Object, StringConcat);

            var ret = Utils.Clone(binaryWithMethodHandlers);
            return ret;
        }

        static void StringConcat(DbBinaryExpression exp, SqlGenerator generator)
        {
            MethodInfo method = exp.Method;

            List<DbExpression> operands = new List<DbExpression>();
            operands.Add(exp.Right);

            DbExpression left = exp.Left;
            DbAddExpression e = null;
            while ((e = (left as DbAddExpression)) != null && (e.Method == UtilConstants.MethodInfo_String_Concat_String_String || e.Method == UtilConstants.MethodInfo_String_Concat_Object_Object))
            {
                operands.Add(e.Right);
                left = e.Left;
            }

            operands.Add(left);

            DbExpression whenExp = null;
            List<DbExpression> operandExps = new List<DbExpression>(operands.Count);
            for (int i = operands.Count - 1; i >= 0; i--)
            {
                DbExpression operand = operands[i];
                DbExpression opBody = operand;
                if (opBody.Type != UtilConstants.TypeOfString)
                {
                    // 需要 cast type
                    opBody = DbExpression.Convert(opBody, UtilConstants.TypeOfString);
                }

                DbExpression equalNullExp = DbExpression.Equal(opBody, UtilConstants.DbConstant_Null_String);

                if (whenExp == null)
                    whenExp = equalNullExp;
                else
                    whenExp = DbExpression.AndAlso(whenExp, equalNullExp);

                DbExpression thenExp = DbConstantExpression.StringEmpty;
                DbCaseWhenExpression.WhenThenExpressionPair whenThenPair = new DbCaseWhenExpression.WhenThenExpressionPair(equalNullExp, thenExp);

                List<DbCaseWhenExpression.WhenThenExpressionPair> whenThenExps = new List<DbCaseWhenExpression.WhenThenExpressionPair>(1);
                whenThenExps.Add(whenThenPair);

                DbExpression elseExp = opBody;

                DbCaseWhenExpression caseWhenExpression = DbExpression.CaseWhen(whenThenExps, elseExp, UtilConstants.TypeOfString);

                operandExps.Add(caseWhenExpression);
            }

            generator._sqlBuilder.Append("CASE", " WHEN ");
            whenExp.Accept(generator);
            generator._sqlBuilder.Append(" THEN ");
            DbConstantExpression.Null.Accept(generator);
            generator._sqlBuilder.Append(" ELSE ");

            generator._sqlBuilder.Append("(");

            for (int i = 0; i < operandExps.Count; i++)
            {
                if (i > 0)
                    generator._sqlBuilder.Append(" + ");

                operandExps[i].Accept(generator);
            }

            generator._sqlBuilder.Append(")");

            generator._sqlBuilder.Append(" END");
        }

        #endregion
    }
}
