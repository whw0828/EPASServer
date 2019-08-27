using Chloe.DbExpressions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Chloe.Entity
{
    public class EntityProperty
    {
        public EntityProperty(PropertyInfo property)
        {
            this.Property = property;
            this.ColumnName = property.Name;
        }
        public PropertyInfo Property { get; private set; }
        public List<object> Annotations { get; private set; } = new List<object>();
        public string ColumnName { get; set; }
        public string SequenceName { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsAutoIncrement { get; set; }
        public DbType? DbType { get; set; }
        public int? Size { get; set; }
        public byte? Scale { get; set; }
        public byte? Precision { get; set; }

        public PropertyDefinition MakeDefinition()
        {
            PropertyDefinition definition = new PropertyDefinition(this.Property, new DbColumn(this.ColumnName, this.Property.PropertyType, this.DbType, this.Size, this.Scale, this.Precision), this.IsPrimaryKey, this.IsAutoIncrement, this.SequenceName, this.Annotations);
            return definition;
        }
    }
}
