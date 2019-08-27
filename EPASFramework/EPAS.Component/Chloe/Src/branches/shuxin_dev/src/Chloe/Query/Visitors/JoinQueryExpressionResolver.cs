using Chloe.DbExpressions;
using Chloe.Descriptors;
using Chloe.Query.QueryExpressions;
using Chloe.Query.QueryState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using Chloe.InternalExtensions;
using Chloe.Infrastructure;

namespace Chloe.Query.Visitors
{
    class JoinQueryExpressionResolver : QueryExpressionVisitor<JoinQueryResult>
    {
        ResultElement _resultElement;
        JoinType _joinType;

        LambdaExpression _conditionExpression;
        ScopeParameterDictionary _scopeParameters;

        JoinQueryExpressionResolver(ResultElement resultElement, JoinType joinType, LambdaExpression conditionExpression, ScopeParameterDictionary scopeParameters)
        {
            this._resultElement = resultElement;
            this._joinType = joinType;
            this._conditionExpression = conditionExpression;
            this._scopeParameters = scopeParameters;
        }

        public static JoinQueryResult Resolve(QueryExpression queryExpression, ResultElement resultElement, JoinType joinType, LambdaExpression conditionExpression, ScopeParameterDictionary scopeParameters)
        {
            JoinQueryExpressionResolver resolver = new JoinQueryExpressionResolver(resultElement, joinType, conditionExpression, scopeParameters);
            return queryExpression.Accept(resolver);
        }

        public override JoinQueryResult Visit(RootQueryExpression exp)
        {
            Type type = exp.ElementType;
            TypeDescriptor typeDescriptor = EntityTypeContainer.GetDescriptor(type);

            string explicitTableName = exp.ExplicitTable;
            DbTable dbTable = typeDescriptor.Table;
            if (explicitTableName != null)
                dbTable = new DbTable(explicitTableName, dbTable.Schema);
            string alias = this._resultElement.GenerateUniqueTableAlias(dbTable.Name);

            DbTableSegment tableSeg = CreateTableSegment(dbTable, alias, exp.Lock);
            MappingObjectExpression moe = new MappingObjectExpression(typeDescriptor.Definition.Type.GetConstructor(Type.EmptyTypes));

            DbTable table = new DbTable(alias);
            foreach (PropertyDescriptor item in typeDescriptor.PropertyDescriptors)
            {
                DbColumnAccessExpression columnAccessExpression = new DbColumnAccessExpression(table, item.Column);
                moe.AddMappingMemberExpression(item.Property, columnAccessExpression);

                if (item.IsPrimaryKey)
                    moe.PrimaryKey = columnAccessExpression;
            }

            //TODO 解析 on 条件表达式
            var scopeParameters = this._scopeParameters.Clone(this._conditionExpression.Parameters.Last(), moe);
            DbExpression condition = GeneralExpressionParser.Parse(this._conditionExpression, scopeParameters, this._resultElement.ScopeTables);

            DbJoinTableExpression joinTable = new DbJoinTableExpression(this._joinType.AsDbJoinType(), tableSeg, condition);

            JoinQueryResult result = new JoinQueryResult();
            result.MappingObjectExpression = moe;
            result.JoinTable = joinTable;

            return result;
        }
        public override JoinQueryResult Visit(WhereExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(OrderExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(SelectExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(SkipExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(TakeExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(AggregateQueryExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(JoinQueryExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(GroupingQueryExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(DistinctExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }

        JoinQueryResult Visit(QueryExpression exp)
        {
            IQueryState state = QueryExpressionResolver.Resolve(exp, this._scopeParameters, this._resultElement.ScopeTables);
            JoinQueryResult ret = state.ToJoinQueryResult(this._joinType, this._conditionExpression, this._scopeParameters, this._resultElement.ScopeTables, this._resultElement.GenerateUniqueTableAlias());
            return ret;
        }
        static DbTableSegment CreateTableSegment(DbTable table, string alias, LockType @lock)
        {
            DbTableExpression tableExp = new DbTableExpression(table);
            DbTableSegment tableSeg = new DbTableSegment(tableExp, alias, @lock);
            return tableSeg;
        }
    }
}
