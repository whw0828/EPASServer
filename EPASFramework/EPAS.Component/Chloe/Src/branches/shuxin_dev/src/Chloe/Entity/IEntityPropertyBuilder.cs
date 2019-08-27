using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Chloe.Entity
{
    public interface IEntityPropertyBuilder
    {
        EntityProperty EntityProperty { get; }
        IEntityPropertyBuilder MapTo(string column);
        IEntityPropertyBuilder HasAnnotation(object value);
        IEntityPropertyBuilder IsPrimaryKey(bool isPrimaryKey = true);
        IEntityPropertyBuilder IsAutoIncrement(bool autoIncrement = true);
        IEntityPropertyBuilder HasDbType(DbType? dbType);
        IEntityPropertyBuilder HasSize(int? size);
        IEntityPropertyBuilder HasScale(byte? scale);
        IEntityPropertyBuilder HasPrecision(byte? precision);
        IEntityPropertyBuilder HasSequence(string name);
    }
    public interface IEntityPropertyBuilder<TProperty> : IEntityPropertyBuilder
    {
        new IEntityPropertyBuilder<TProperty> MapTo(string column);
        new IEntityPropertyBuilder<TProperty> HasAnnotation(object value);
        new IEntityPropertyBuilder<TProperty> IsPrimaryKey(bool isPrimaryKey = true);
        new IEntityPropertyBuilder<TProperty> IsAutoIncrement(bool autoIncrement = true);
        new IEntityPropertyBuilder<TProperty> HasDbType(DbType? dbType);
        new IEntityPropertyBuilder<TProperty> HasSize(int? size);
        new IEntityPropertyBuilder<TProperty> HasScale(byte? scale);
        new IEntityPropertyBuilder<TProperty> HasPrecision(byte? precision);
        new IEntityPropertyBuilder<TProperty> HasSequence(string name);
    }
}
