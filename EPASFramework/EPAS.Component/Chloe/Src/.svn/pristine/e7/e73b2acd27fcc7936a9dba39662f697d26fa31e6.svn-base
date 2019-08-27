using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chloe.Entity
{
    public class EntityPropertyBuilder<TProperty> : IEntityPropertyBuilder<TProperty>
    {
        public EntityPropertyBuilder(EntityProperty entityProperty)
        {
            this.EntityProperty = entityProperty;
        }
        public EntityProperty EntityProperty { get; private set; }

        IEntityPropertyBuilder AsNonGenericBuilder()
        {
            return this;
        }

        public IEntityPropertyBuilder<TProperty> MapTo(string column)
        {
            this.AsNonGenericBuilder().MapTo(column);
            return this;
        }
        IEntityPropertyBuilder IEntityPropertyBuilder.MapTo(string column)
        {
            this.EntityProperty.ColumnName = column;
            return this;
        }

        public IEntityPropertyBuilder<TProperty> HasAnnotation(object value)
        {
            this.AsNonGenericBuilder().HasAnnotation(value);
            return this;
        }
        IEntityPropertyBuilder IEntityPropertyBuilder.HasAnnotation(object value)
        {
            if (value == null)
                throw new ArgumentNullException();

            this.EntityProperty.Annotations.Add(value);
            return this;
        }

        public IEntityPropertyBuilder<TProperty> IsPrimaryKey(bool isPrimaryKey = true)
        {
            this.AsNonGenericBuilder().IsPrimaryKey(isPrimaryKey);
            return this;
        }
        IEntityPropertyBuilder IEntityPropertyBuilder.IsPrimaryKey(bool isPrimaryKey)
        {
            this.EntityProperty.IsPrimaryKey = isPrimaryKey;
            return this;
        }

        public IEntityPropertyBuilder<TProperty> IsAutoIncrement(bool isAutoIncrement = true)
        {
            this.AsNonGenericBuilder().IsAutoIncrement(isAutoIncrement);
            return this;
        }
        IEntityPropertyBuilder IEntityPropertyBuilder.IsAutoIncrement(bool isAutoIncrement)
        {
            this.EntityProperty.IsAutoIncrement = isAutoIncrement;
            if (isAutoIncrement)
            {
                this.EntityProperty.SequenceName = null;
            }

            return this;
        }

        public IEntityPropertyBuilder<TProperty> HasDbType(DbType? dbType)
        {
            this.AsNonGenericBuilder().HasDbType(dbType);
            return this;
        }
        IEntityPropertyBuilder IEntityPropertyBuilder.HasDbType(DbType? dbType)
        {
            this.EntityProperty.DbType = dbType;
            return this;
        }

        public IEntityPropertyBuilder<TProperty> HasSize(int? size)
        {
            this.AsNonGenericBuilder().HasSize(size);
            return this;
        }
        IEntityPropertyBuilder IEntityPropertyBuilder.HasSize(int? size)
        {
            this.EntityProperty.Size = size;
            return this;
        }

        public IEntityPropertyBuilder<TProperty> HasScale(byte? scale)
        {
            this.AsNonGenericBuilder().HasScale(scale);
            return this;
        }
        IEntityPropertyBuilder IEntityPropertyBuilder.HasScale(byte? scale)
        {
            this.EntityProperty.Scale = scale;
            return this;
        }

        public IEntityPropertyBuilder<TProperty> HasPrecision(byte? precision)
        {
            this.AsNonGenericBuilder().HasPrecision(precision);
            return this;
        }
        IEntityPropertyBuilder IEntityPropertyBuilder.HasPrecision(byte? precision)
        {
            this.EntityProperty.Precision = precision;
            return this;
        }

        public IEntityPropertyBuilder<TProperty> HasSequence(string name)
        {
            this.AsNonGenericBuilder().HasSequence(name);
            return this;
        }
        IEntityPropertyBuilder IEntityPropertyBuilder.HasSequence(string name)
        {
            this.EntityProperty.SequenceName = name;
            if (!string.IsNullOrEmpty(name))
            {
                this.EntityProperty.IsAutoIncrement = false;
            }

            return this;
        }
    }


}
