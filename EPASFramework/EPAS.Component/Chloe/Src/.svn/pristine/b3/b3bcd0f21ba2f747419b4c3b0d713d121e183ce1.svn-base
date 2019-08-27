using Chloe.DbExpressions;
using System;
using Chloe.Descriptors;
using System.Reflection;
using Chloe.InternalExtensions;
using System.Linq.Expressions;
using System.Collections.Generic;
using Chloe.Utility;
using Chloe.Infrastructure;

namespace Chloe.Query.QueryState
{
    internal sealed class RootQueryState : QueryStateBase
    {
        Type _elementType;
        public RootQueryState(Type elementType, string explicitTableName, LockType @lock, ScopeParameterDictionary scopeParameters, KeyDictionary<string> scopeTables)
            : base(CreateResultElement(elementType, explicitTableName, @lock, scopeParameters, scopeTables))
        {
            this._elementType = elementType;
        }

        public override ResultElement ToFromQueryResult()
        {
            if (this.Result.Condition == null)
            {
                ResultElement result = new ResultElement(this.Result.ScopeParameters, this.Result.ScopeTables);
                result.FromTable = this.Result.FromTable;
                result.MappingObjectExpression = this.Result.MappingObjectExpression;
                return result;
            }

            return base.ToFromQueryResult();
        }

        static ResultElement CreateResultElement(Type type, string explicitTableName, LockType lockType, ScopeParameterDictionary scopeParameters, KeyDictionary<string> scopeTables)
        {
            if (type.IsAbstract || type.IsInterface)
                throw new ArgumentException("The type of input can not be abstract class or interface.");

            //TODO init _resultElement
            ResultElement resultElement = new ResultElement(scopeParameters, scopeTables);

            TypeDescriptor typeDescriptor = EntityTypeContainer.GetDescriptor(type);

            DbTable dbTable = typeDescriptor.Table;
            if (explicitTableName != null)
                dbTable = new DbTable(explicitTableName, dbTable.Schema);
            string alias = resultElement.GenerateUniqueTableAlias(dbTable.Name);

            resultElement.FromTable = CreateRootTable(dbTable, alias, lockType);

            ConstructorInfo constructor = typeDescriptor.Definition.Type.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
                throw new ArgumentException(string.Format("The type of '{0}' does't define a none parameter constructor.", type.FullName));

            MappingObjectExpression moe = new MappingObjectExpression(constructor);

            DbTableSegment tableExp = resultElement.FromTable.Table;
            DbTable table = new DbTable(alias);

            foreach (PropertyDescriptor item in typeDescriptor.PropertyDescriptors)
            {
                DbColumnAccessExpression columnAccessExpression = new DbColumnAccessExpression(table, item.Column);

                moe.AddMappingMemberExpression(item.Property, columnAccessExpression);
                if (item.IsPrimaryKey)
                    moe.PrimaryKey = columnAccessExpression;
            }

            resultElement.MappingObjectExpression = moe;

            return resultElement;
        }
        static DbFromTableExpression CreateRootTable(DbTable table, string alias, LockType lockType)
        {
            DbTableExpression tableExp = new DbTableExpression(table);
            DbTableSegment tableSeg = new DbTableSegment(tableExp, alias, lockType);
            var fromTableExp = new DbFromTableExpression(tableSeg);
            return fromTableExp;
        }
    }
}
