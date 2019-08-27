using Chloe.Core;
using Chloe.Core.Visitors;
using Chloe.DbExpressions;
using Chloe.Descriptors;
using Chloe.Entity;
using Chloe.Exceptions;
using Chloe.Infrastructure;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chloe.MySql
{
    public class MySqlContext : DbContext
    {
        DatabaseProvider _databaseProvider;
        public MySqlContext(IDbConnectionFactory dbConnectionFactory)
        {
            PublicHelper.CheckNull(dbConnectionFactory);
            this._databaseProvider = new DatabaseProvider(dbConnectionFactory);
        }
        public MySqlContext(Func<IDbConnection> dbConnectionFactory) : this(new DbConnectionFactory(dbConnectionFactory))
        {
        }


        public override IDatabaseProvider DatabaseProvider
        {
            get { return this._databaseProvider; }
        }

        [Obsolete("This method will be removed in future versions.")]
        public override void InsertRange<TEntity>(List<TEntity> entities, bool keepIdentity = false, string table = null)
        {
            /*
             * 将 entities 分批插入数据库
             * 每批生成 insert into TableName(...) values(...),(...)... 
             * 该方法相对循环一条一条插入，速度提升 2/3 这样
             */

            PublicHelper.CheckNull(entities);
            if (entities.Count == 0)
                return;

            int maxParameters = 1000;
            int batchSize = 60; /* 每批实体大小，此值通过测试得出相对插入速度比较快的一个值 */

            TypeDescriptor typeDescriptor = EntityTypeContainer.GetDescriptor(typeof(TEntity));

            var e = typeDescriptor.PropertyDescriptors as IEnumerable<PropertyDescriptor>;
            if (keepIdentity == false)
                e = e.Where(a => a.IsAutoIncrement == false);
            List<PropertyDescriptor> mappingPropertyDescriptors = e.ToList();
            int maxDbParamsCount = maxParameters - mappingPropertyDescriptors.Count; /* 控制一个 sql 的参数个数 */

            DbTable dbTable = string.IsNullOrEmpty(table) ? typeDescriptor.Table : new DbTable(table, typeDescriptor.Table.Schema);
            string sqlTemplate = AppendInsertRangeSqlTemplate(dbTable, mappingPropertyDescriptors);

            Action insertAction = () =>
            {
                int batchCount = 0;
                List<DbParam> dbParams = new List<DbParam>();
                StringBuilder sqlBuilder = new StringBuilder();
                for (int i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];

                    if (batchCount > 0)
                        sqlBuilder.Append(",");

                    sqlBuilder.Append("(");
                    for (int j = 0; j < mappingPropertyDescriptors.Count; j++)
                    {
                        if (j > 0)
                            sqlBuilder.Append(",");

                        PropertyDescriptor mappingPropertyDescriptor = mappingPropertyDescriptors[j];

                        object val = mappingPropertyDescriptor.GetValue(entity);
                        if (val == null)
                        {
                            sqlBuilder.Append("NULL");
                            continue;
                        }

                        Type valType = val.GetType();
                        if (valType.IsEnum)
                        {
                            val = Convert.ChangeType(val, Enum.GetUnderlyingType(valType));
                            valType = val.GetType();
                        }

                        if (Utils.IsToStringableNumericType(valType))
                        {
                            sqlBuilder.Append(val.ToString());
                            continue;
                        }

                        if (val is bool)
                        {
                            if ((bool)val == true)
                                sqlBuilder.AppendFormat("1");
                            else
                                sqlBuilder.AppendFormat("0");
                            continue;
                        }

                        string paramName = UtilConstants.ParameterNamePrefix + dbParams.Count.ToString();
                        DbParam dbParam = new DbParam(paramName, val) { DbType = mappingPropertyDescriptor.Column.DbType };
                        dbParams.Add(dbParam);
                        sqlBuilder.Append(paramName);
                    }
                    sqlBuilder.Append(")");

                    batchCount++;

                    if ((batchCount >= 20 && dbParams.Count >= 400/*参数个数太多也会影响速度*/) || dbParams.Count >= maxDbParamsCount || batchCount >= batchSize || (i + 1) == entities.Count)
                    {
                        sqlBuilder.Insert(0, sqlTemplate);
                        string sql = sqlBuilder.ToString();
                        this.Session.ExecuteNonQuery(sql, dbParams.ToArray());

                        sqlBuilder.Clear();
                        dbParams.Clear();
                        batchCount = 0;
                    }
                }
            };

            Action fAction = insertAction;

            if (this.Session.IsInTransaction)
            {
                fAction();
            }
            else
            {
                /* 因为分批插入，所以需要开启事务保证数据一致性 */
                this.Session.BeginTransaction();
                try
                {
                    fAction();
                    this.Session.CommitTransaction();
                }
                catch
                {
                    if (this.Session.IsInTransaction)
                        this.Session.RollbackTransaction();
                    throw;
                }
            }
        }
        public override void InsertRange<TEntity>(List<TEntity> entities, string table)
        {
            /*
             * 将 entities 分批插入数据库
             * 每批生成 insert into TableName(...) values(...),(...)... 
             * 该方法相对循环一条一条插入，速度提升 2/3 这样
             */

            PublicHelper.CheckNull(entities);
            if (entities.Count == 0)
                return;

            int maxParameters = 1000;
            int batchSize = 60; /* 每批实体大小，此值通过测试得出相对插入速度比较快的一个值 */

            TypeDescriptor typeDescriptor = EntityTypeContainer.GetDescriptor(typeof(TEntity));

            List<PropertyDescriptor> mappingPropertyDescriptors = typeDescriptor.PropertyDescriptors.Where(a => a.IsAutoIncrement == false).ToList();
            int maxDbParamsCount = maxParameters - mappingPropertyDescriptors.Count; /* 控制一个 sql 的参数个数 */

            DbTable dbTable = string.IsNullOrEmpty(table) ? typeDescriptor.Table : new DbTable(table, typeDescriptor.Table.Schema);
            string sqlTemplate = AppendInsertRangeSqlTemplate(dbTable, mappingPropertyDescriptors);

            Action insertAction = () =>
            {
                int batchCount = 0;
                List<DbParam> dbParams = new List<DbParam>();
                StringBuilder sqlBuilder = new StringBuilder();
                for (int i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];

                    if (batchCount > 0)
                        sqlBuilder.Append(",");

                    sqlBuilder.Append("(");
                    for (int j = 0; j < mappingPropertyDescriptors.Count; j++)
                    {
                        if (j > 0)
                            sqlBuilder.Append(",");

                        PropertyDescriptor mappingPropertyDescriptor = mappingPropertyDescriptors[j];

                        object val = mappingPropertyDescriptor.GetValue(entity);
                        if (val == null)
                        {
                            sqlBuilder.Append("NULL");
                            continue;
                        }

                        Type valType = val.GetType();
                        if (valType.IsEnum)
                        {
                            val = Convert.ChangeType(val, Enum.GetUnderlyingType(valType));
                            valType = val.GetType();
                        }

                        if (Utils.IsToStringableNumericType(valType))
                        {
                            sqlBuilder.Append(val.ToString());
                            continue;
                        }

                        if (val is bool)
                        {
                            if ((bool)val == true)
                                sqlBuilder.AppendFormat("1");
                            else
                                sqlBuilder.AppendFormat("0");
                            continue;
                        }

                        string paramName = UtilConstants.ParameterNamePrefix + dbParams.Count.ToString();
                        DbParam dbParam = new DbParam(paramName, val) { DbType = mappingPropertyDescriptor.Column.DbType };
                        dbParams.Add(dbParam);
                        sqlBuilder.Append(paramName);
                    }
                    sqlBuilder.Append(")");

                    batchCount++;

                    if ((batchCount >= 20 && dbParams.Count >= 400/*参数个数太多也会影响速度*/) || dbParams.Count >= maxDbParamsCount || batchCount >= batchSize || (i + 1) == entities.Count)
                    {
                        sqlBuilder.Insert(0, sqlTemplate);
                        string sql = sqlBuilder.ToString();
                        this.Session.ExecuteNonQuery(sql, dbParams.ToArray());

                        sqlBuilder.Clear();
                        dbParams.Clear();
                        batchCount = 0;
                    }
                }
            };

            Action fAction = insertAction;

            if (this.Session.IsInTransaction)
            {
                fAction();
            }
            else
            {
                /* 因为分批插入，所以需要开启事务保证数据一致性 */
                this.Session.BeginTransaction();
                try
                {
                    fAction();
                    this.Session.CommitTransaction();
                }
                catch
                {
                    if (this.Session.IsInTransaction)
                        this.Session.RollbackTransaction();
                    throw;
                }
            }
        }

        static string AppendInsertRangeSqlTemplate(DbTable table, List<PropertyDescriptor> mappingPropertyDescriptors)
        {
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append("INSERT INTO ");
            sqlBuilder.Append(AppendTableName(table));
            sqlBuilder.Append("(");

            for (int i = 0; i < mappingPropertyDescriptors.Count; i++)
            {
                PropertyDescriptor mappingPropertyDescriptor = mappingPropertyDescriptors[i];
                if (i > 0)
                    sqlBuilder.Append(",");
                sqlBuilder.Append(Utils.QuoteName(mappingPropertyDescriptor.Column.Name));
            }

            sqlBuilder.Append(") VALUES");

            string sqlTemplate = sqlBuilder.ToString();
            return sqlTemplate;
        }

        static string AppendTableName(DbTable table)
        {
            return Utils.QuoteName(table.Name);
        }

        public virtual int Update<TEntity>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TEntity>> content, int limits)
        {
            return this.Update<TEntity>(condition, content, null, limits);
        }
        public virtual int Update<TEntity>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TEntity>> content, string table, int limits)
        {
            PublicHelper.CheckNull(condition);
            PublicHelper.CheckNull(content);

            TypeDescriptor typeDescriptor = EntityTypeContainer.GetDescriptor(typeof(TEntity));

            Dictionary<MemberInfo, Expression> updateColumns = InitMemberExtractor.Extract(content);

            DbTable explicitDbTable = null;
            if (table != null)
                explicitDbTable = new DbTable(table, typeDescriptor.Table.Schema);
            DefaultExpressionParser expressionParser = typeDescriptor.GetExpressionParser(explicitDbTable);

            DbExpression conditionExp = expressionParser.ParseFilterPredicate(condition);

            MySqlDbUpdateExpression e = new MySqlDbUpdateExpression(explicitDbTable ?? typeDescriptor.Table, conditionExp);

            foreach (var kv in updateColumns)
            {
                MemberInfo key = kv.Key;
                PropertyDescriptor propertyDescriptor = typeDescriptor.TryGetPropertyDescriptor(key);

                if (propertyDescriptor == null)
                    throw new ChloeException(string.Format("The member '{0}' does not map any column.", key.Name));

                if (propertyDescriptor.IsPrimaryKey)
                    throw new ChloeException(string.Format("Could not update the primary key '{0}'.", propertyDescriptor.Column.Name));

                if (propertyDescriptor.IsAutoIncrement)
                    throw new ChloeException(string.Format("Could not update the identity column '{0}'.", propertyDescriptor.Column.Name));

                e.UpdateColumns.Add(propertyDescriptor.Column, expressionParser.Parse(kv.Value));
            }

            e.Limits = limits;

            if (e.UpdateColumns.Count == 0)
                return 0;

            return this.ExecuteNonQuery(e);
        }

        public virtual int Delete<TEntity>(Expression<Func<TEntity, bool>> condition, int limits)
        {
            return this.Delete(condition, null, limits);
        }
        public virtual int Delete<TEntity>(Expression<Func<TEntity, bool>> condition, string table, int limits)
        {
            PublicHelper.CheckNull(condition);

            TypeDescriptor typeDescriptor = EntityTypeContainer.GetDescriptor(typeof(TEntity));

            DbTable explicitDbTable = null;
            if (table != null)
                explicitDbTable = new DbTable(table, typeDescriptor.Table.Schema);
            DefaultExpressionParser expressionParser = typeDescriptor.GetExpressionParser(explicitDbTable);
            DbExpression conditionExp = expressionParser.ParseFilterPredicate(condition);

            MySqlDbDeleteExpression e = new MySqlDbDeleteExpression(explicitDbTable ?? typeDescriptor.Table, conditionExp);
            e.Limits = limits;

            return this.ExecuteNonQuery(e);
        }
    }
}
