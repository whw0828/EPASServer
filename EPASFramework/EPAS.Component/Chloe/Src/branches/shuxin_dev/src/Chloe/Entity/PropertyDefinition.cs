using Chloe.DbExpressions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;

namespace Chloe.Entity
{
    public class PropertyDefinition
    {
        public PropertyDefinition(PropertyInfo property, DbColumn column, bool isPrimaryKey, bool isAutoIncrement, string sequenceName, IList<object> annotations)
        {
            Utils.CheckNull(property, nameof(property));
            Utils.CheckNull(column, nameof(column));
            Utils.CheckNull(annotations, nameof(annotations));

            this.Property = property;
            this.Column = column;
            this.IsPrimaryKey = isPrimaryKey;
            this.IsAutoIncrement = isAutoIncrement;
            this.SequenceName = sequenceName;
            this.Annotations = annotations.Where(a => a != null).ToList().AsReadOnly();
        }
        public PropertyInfo Property { get; private set; }
        public DbColumn Column { get; private set; }
        public bool IsPrimaryKey { get; private set; }
        public bool IsAutoIncrement { get; private set; }
        public string SequenceName { get; private set; }
        public ReadOnlyCollection<object> Annotations { get; private set; }
    }
}
