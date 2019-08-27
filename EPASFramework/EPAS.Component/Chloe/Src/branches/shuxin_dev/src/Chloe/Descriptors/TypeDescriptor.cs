using Chloe.Core.Visitors;
using Chloe.DbExpressions;
using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Reflection;
using Chloe.InternalExtensions;
using Chloe.Utility;

namespace Chloe.Descriptors
{
    public class TypeDescriptor
    {
        Dictionary<MemberInfo, PropertyDescriptor> _propertyDescriptorMap;
        Dictionary<MemberInfo, DbColumnAccessExpression> _propertyColumnMap;
        DefaultExpressionParser _expressionParser = null;

        public TypeDescriptor(TypeDefinition definition)
        {
            this.Definition = definition;
            this.PropertyDescriptors = this.Definition.Properties.Select(a => new PropertyDescriptor(a, this)).ToList().AsReadOnly();

            this.PrimaryKeys = this.PropertyDescriptors.Where(a => a.Definition.IsPrimaryKey).ToList().AsReadOnly();
            this.AutoIncrement = this.PropertyDescriptors.Where(a => a.Definition.IsAutoIncrement).FirstOrDefault();

            this._propertyDescriptorMap = PublicHelper.Clone(this.PropertyDescriptors.ToDictionary(a => (MemberInfo)a.Definition.Property, a => a));
            this._propertyColumnMap = PublicHelper.Clone(this.PropertyDescriptors.ToDictionary(a => (MemberInfo)a.Definition.Property, a => new DbColumnAccessExpression(this.Definition.Table, a.Definition.Column)));
        }

        public TypeDefinition Definition { get; private set; }
        public ReadOnlyCollection<PropertyDescriptor> PropertyDescriptors { get; private set; }
        public ReadOnlyCollection<PropertyDescriptor> PrimaryKeys { get; private set; }
        /* It will return null if an entity has no auto increment member. */
        public PropertyDescriptor AutoIncrement { get; private set; }

        public DbTable Table { get { return this.Definition.Table; } }

        public DefaultExpressionParser GetExpressionParser(DbTable explicitDbTable)
        {
            if (explicitDbTable == null)
            {
                if (this._expressionParser == null)
                    this._expressionParser = new DefaultExpressionParser(this, null);
                return this._expressionParser;
            }
            else
                return new DefaultExpressionParser(this, explicitDbTable);
        }

        public bool HasPrimaryKey()
        {
            return this.PrimaryKeys.Count > 0;
        }
        public PropertyDescriptor TryGetPropertyDescriptor(MemberInfo member)
        {
            member = member.AsReflectedMemberOf(this.Definition.Type);
            PropertyDescriptor propertyDescriptor;
            this._propertyDescriptorMap.TryGetValue(member, out propertyDescriptor);
            return propertyDescriptor;
        }
        public DbColumnAccessExpression TryGetColumnAccessExpression(MemberInfo member)
        {
            member = member.AsReflectedMemberOf(this.Definition.Type);
            DbColumnAccessExpression dbColumnAccessExpression;
            this._propertyColumnMap.TryGetValue(member, out dbColumnAccessExpression);
            return dbColumnAccessExpression;
        }
    }
}
