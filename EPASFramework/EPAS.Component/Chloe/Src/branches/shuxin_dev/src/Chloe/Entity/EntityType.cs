using Chloe.DbExpressions;
using Chloe.Exceptions;
using Chloe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chloe.Entity
{
    public class EntityType
    {
        public EntityType(Type type)
        {
            this.Type = type;
            this.TableName = type.Name;

            PropertyInfo[] properties = this.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(a => a.GetSetMethod() != null && a.GetGetMethod() != null).ToArray();

            foreach (PropertyInfo property in properties)
            {
                if (!MappingTypeSystem.IsMappingType(property.PropertyType))
                    continue;

                this.Properties.Add(new EntityProperty(property));
            }
        }

        public Type Type { get; private set; }
        public string TableName { get; set; }
        public string SchemaName { get; set; }
        public List<EntityProperty> Properties { get; private set; } = new List<EntityProperty>();
        public List<object> Annotations { get; private set; } = new List<object>();

        public virtual TypeDefinition MakeDefinition()
        {
            List<PropertyDefinition> properties = this.Properties.Select(a => a.MakeDefinition()).ToList();
            var autoIncrementProperties = properties.Where(a => a.IsAutoIncrement).ToList();
            if (autoIncrementProperties.Count > 1)
            {
                /* 一个实体不能有多个自增成员 */
                throw new NotSupportedException(string.Format("The entity type '{0}' can not define multiple auto increment members.", this.Type.FullName));
            }
            if (autoIncrementProperties.Exists(a => !Utils.IsAutoIncrementType(a.Property.PropertyType)))
            {
                /* 限定自增类型 */
                throw new ChloeException("Auto increment member type must be Int16, Int32 or Int64.");
            }

            var primaryKeys = properties.Where(a => a.IsPrimaryKey).ToList();
            if (primaryKeys.Count > 1 && primaryKeys.Exists(a => a.IsAutoIncrement))
            {
                /* 自增列不能作为联合主键 */
                throw new ChloeException("Auto increment member can not be union key.");
            }

            TypeDefinition definition = new TypeDefinition(this.Type, new DbTable(this.TableName, this.SchemaName), properties, this.Annotations);
            return definition;
        }
    }

}
