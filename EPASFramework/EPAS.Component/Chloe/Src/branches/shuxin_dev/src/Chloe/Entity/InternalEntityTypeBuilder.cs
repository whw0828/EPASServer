using Chloe.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Chloe.InternalExtensions;

namespace Chloe.Entity
{
    internal class InternalEntityTypeBuilder<TEntity> : EntityTypeBuilder<TEntity>
    {
        public InternalEntityTypeBuilder()
        {
            this.ConfigureTableMapping();
            this.ConfigureColumnMapping();
        }

        void ConfigureTableMapping()
        {
            var entityAttributes = this.EntityType.Type.GetCustomAttributes();
            foreach (Attribute entityAttribute in entityAttributes)
            {
                this.EntityType.Annotations.Add(entityAttribute);

                TableAttribute tableAttribute = entityAttribute as TableAttribute;
                if (tableAttribute != null)
                {
                    if (!string.IsNullOrEmpty(tableAttribute.Name))
                        this.MapTo(tableAttribute.Name);

                    this.HasSchema(tableAttribute.Schema);
                }
            }
        }
        void ConfigureColumnMapping()
        {
            var propertyInfos = this.EntityType.Properties.Select(a => a.Property).ToList();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IEntityPropertyBuilder propertyBuilder = this.Property(propertyInfo.Name);

                propertyBuilder.IsPrimaryKey(false);
                propertyBuilder.IsAutoIncrement(false);

                var propertyAttributes = propertyInfo.GetCustomAttributes();
                foreach (Attribute propertyAttribute in propertyAttributes)
                {
                    if (propertyAttribute is NotMappedAttribute)
                    {
                        this.Ignore(propertyInfo.Name);
                    }

                    propertyBuilder.HasAnnotation(propertyAttribute);

                    if (propertyAttribute is ColumnAttribute)
                    {
                        ColumnAttribute columnAttribute = (ColumnAttribute)propertyAttribute;

                        if (!string.IsNullOrEmpty(columnAttribute.Name))
                            propertyBuilder.MapTo(columnAttribute.Name);

                        propertyBuilder.IsPrimaryKey(columnAttribute.IsPrimaryKey);
                        propertyBuilder.HasDbType(columnAttribute.GetDbType());
                        propertyBuilder.HasSize(columnAttribute.GetSize());
                        propertyBuilder.HasScale(columnAttribute.GetScale());
                        propertyBuilder.HasPrecision(columnAttribute.GetPrecision());
                    }

                    if (propertyAttribute is AutoIncrementAttribute)
                    {
                        propertyBuilder.IsAutoIncrement();
                    }

                    if (propertyAttribute is SequenceAttribute)
                    {
                        propertyBuilder.HasSequence((propertyAttribute as SequenceAttribute).Name);
                    }
                }
            }

            List<EntityProperty> primaryKeys = this.EntityType.Properties.Where(a => a.IsPrimaryKey).ToList();
            if (primaryKeys.Count == 0)
            {
                //如果没有定义任何主键，则从所有映射的属性中查找名为 id 的属性作为主键
                EntityProperty idNameProperty = this.EntityType.Properties.Find(a => a.Property.Name.ToLower() == "id" && !a.Property.IsDefined(typeof(ColumnAttribute)));

                if (idNameProperty != null)
                {
                    idNameProperty.IsPrimaryKey = true;
                    primaryKeys.Add(idNameProperty);
                }
            }

            if (primaryKeys.Count == 1 && this.EntityType.Properties.Count(a => a.IsAutoIncrement) == 0)
            {
                /* 如果没有显示定义自增成员，并且主键只有 1 个，如果该主键满足一定条件，则默认其是自增列 */
                EntityProperty primaryKey = primaryKeys[0];

                if (string.IsNullOrEmpty(primaryKey.SequenceName) && Utils.IsAutoIncrementType(primaryKey.Property.PropertyType) && !primaryKey.Property.IsDefined(typeof(NonAutoIncrementAttribute)))
                {
                    primaryKey.IsAutoIncrement = true;
                }
            }
        }
    }
}
