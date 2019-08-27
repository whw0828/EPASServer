using Chloe.DbExpressions;
using System;
using Chloe.Descriptors;
using System.Reflection;
using Chloe.InternalExtensions;

namespace Chloe.Query.QueryState
{
    internal sealed class RootQueryState : QueryStateBase
    {
        Type _elementType;
        public RootQueryState(Type elementType, string explicitTableName)
            : base(CreateResultElement(elementType, explicitTableName))
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

        static ResultElement CreateResultElement(Type type, string explicitTableName)
        {
            if (type.IsAbstractOrInterface())
                throw new ArgumentException("The type of input can not be abstract class or interface.");

            //TODO init _resultElement
            ResultElement resultElement = new ResultElement();

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(type);

            DbTable dbTable = typeDescriptor.Table;
            if (explicitTableName != null)
                dbTable = new DbTable(explicitTableName, dbTable.Schema);
            string alias = resultElement.GenerateUniqueTableAlias(dbTable.Name);

            resultElement.FromTable = CreateRootTable(dbTable, alias);

            ConstructorInfo constructor = typeDescriptor.EntityType.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
                throw new ArgumentException(string.Format("The type of '{0}' does't define a none parameter constructor.", type.FullName));

            MappingObjectExpression moe = new MappingObjectExpression(constructor);

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
