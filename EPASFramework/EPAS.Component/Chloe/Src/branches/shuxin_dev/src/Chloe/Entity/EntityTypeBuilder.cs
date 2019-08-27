using Chloe.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chloe.Entity
{
    public class EntityTypeBuilder<TEntity> : IEntityTypeBuilder<TEntity>
    {
        public EntityTypeBuilder()
        {
            this.EntityType = new EntityType(typeof(TEntity));

            foreach (EntityProperty property in this.EntityType.Properties)
            {
                if (property.Property.Name.ToLower() == "id")
                {
                    /*默认为主键，且自增*/
                    property.IsPrimaryKey = true;

                    if (Utils.IsAutoIncrementType(property.Property.PropertyType))
                        property.IsAutoIncrement = true;
                }
            }
        }
        public EntityType EntityType { get; private set; }

        IEntityTypeBuilder AsNonGenericBuilder()
        {
            return this;
        }

        public IEntityTypeBuilder<TEntity> MapTo(string table)
        {
            this.AsNonGenericBuilder().MapTo(table);
            return this;
        }
        IEntityTypeBuilder IEntityTypeBuilder.MapTo(string table)
        {
            this.EntityType.TableName = table;
            return this;
        }

        public IEntityTypeBuilder<TEntity> HasSchema(string schema)
        {
            this.AsNonGenericBuilder().HasSchema(schema);
            return this;
        }
        IEntityTypeBuilder IEntityTypeBuilder.HasSchema(string schema)
        {
            this.EntityType.SchemaName = schema;
            return this;
        }

        public IEntityTypeBuilder<TEntity> HasAnnotation(object value)
        {
            this.AsNonGenericBuilder().HasAnnotation(value);
            return this;
        }
        IEntityTypeBuilder IEntityTypeBuilder.HasAnnotation(object value)
        {
            if (value == null)
                throw new ArgumentNullException();

            this.EntityType.Annotations.Add(value);
            return this;
        }

        public IEntityTypeBuilder<TEntity> Ignore(Expression<Func<TEntity, object>> property)
        {
            string propertyName = PropertyNameExtractor.Extract(property);
            this.Ignore(propertyName);
            return this;
        }
        public IEntityTypeBuilder Ignore(string property)
        {
            this.EntityType.Properties.RemoveAll(a => a.Property.Name == property);
            return this;
        }
        public IEntityPropertyBuilder<TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            string propertyName = PropertyNameExtractor.Extract(property);
            IEntityPropertyBuilder<TProperty> propertyBuilder = this.Property(propertyName) as IEntityPropertyBuilder<TProperty>;
            return propertyBuilder;
        }
        public IEntityPropertyBuilder Property(string property)
        {
            EntityProperty entityProperty = this.EntityType.Properties.FirstOrDefault(a => a.Property.Name == property);

            if (entityProperty == null)
                throw new ArgumentException($"The mapping property list doesn't contain property named '{property}'.");

            IEntityPropertyBuilder propertyBuilder = Activator.CreateInstance(typeof(EntityPropertyBuilder<>).MakeGenericType(entityProperty.Property.PropertyType), entityProperty) as IEntityPropertyBuilder;
            return propertyBuilder;
        }
    }
}
