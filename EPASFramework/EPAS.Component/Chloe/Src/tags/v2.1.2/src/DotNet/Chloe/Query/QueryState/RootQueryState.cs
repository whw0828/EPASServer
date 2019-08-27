using Chloe.DbExpressions;
using System;
using Chloe.Utility;
using Chloe.Descriptors;

namespace Chloe.Query.QueryState
{
    internal sealed class RootQueryState : QueryStateBase
    {
        Type _elementType;
        public RootQueryState(Type elementType)
            : base(CreateResultElement(elementType))
        {
            this._elementType = elementType;
        }

        public override FromQueryResult ToFromQueryResult()
        {
            if (this.Result.Condition == null)
            {
                FromQueryResult result = new FromQueryResult();
                result.FromTable = this.Result.FromTable;
                result.MappingObjectExpression = this.Result.MappingObjectExpression;
                return result;
            }

            return base.ToFromQueryResult();
        }

        static ResultElement CreateResultElement(Type type)
        {
            //TODO init _resultElement
            ResultElement resultElement = new ResultElement();

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(type);

            string alias = resultElement.GenerateUniqueTableAlias(typeDescriptor.Table.Name);

            resultElement.FromTable = CreateRootTable(typeDescriptor.Table, alias);
            MappingObjectExpression moe = new MappingObjectExpression(typeDescriptor.EntityType.GetConstructor(Type.EmptyTypes));

            DbTableSegment tableExp = resultElement.FromTable.Table;
            DbTable table = new DbTable(alias);

            foreach (MappingMemberDescriptor item in typeDescriptor.MappingMemberDescriptors.Values)
            {
                DbColumnAccessExpression columnAccessExpression = new DbColumnAccessExpression(table, item.Column);

                moe.AddMemberExpression(item.MemberInfo, columnAccessExpression);
                if (item.IsPrimaryKey)
                    moe.PrimaryKey = columnAccessExpression;
            }

            resultElement.MappingObjectExpression = moe;

            return resultElement;
        }
        static DbFromTableExpression CreateRootTable(DbTable table, string alias)
        {
            DbTableExpression tableExp = new DbTableExpression(table);
            DbTableSegment tableSeg = new DbTableSegment(tableExp, alias);
            var fromTableExp = new DbFromTableExpression(tableSeg);
            return fromTableExp;
        }
    }
}
