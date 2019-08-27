using Chloe.DbExpressions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chloe.Entity
{
    public class TypeDefinition
    {
        public TypeDefinition(Type entityType, DbTable table, IList<PropertyDefinition> properties, IList<object> annotations)
        {
            Utils.CheckNull(entityType, nameof(entityType));
            Utils.CheckNull(table, nameof(table));
            Utils.CheckNull(properties, nameof(properties));
            Utils.CheckNull(annotations, nameof(annotations));

            this.Type = entityType;
            this.Table = table;
            this.Properties = properties.Where(a => a != null).ToList().AsReadOnly();
            this.Annotations = annotations.Where(a => a != null).ToList().AsReadOnly();
        }
        public Type Type { get; private set; }
        public DbTable Table { get; private set; }
        public ReadOnlyCollection<PropertyDefinition> Properties { get; private set; }
        public ReadOnlyCollection<object> Annotations { get; private set; }
    }

}
