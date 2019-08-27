using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Chloe.Infrastructure
{
    public class MappingType<T> : MappingType
    {
        public MappingType(DbType dbType) : base(typeof(T), dbType)
        {
        }

        public override IDbDataParameter CreateDataParameter(IDbCommand cmd, DbParam param)
        {
            return base.CreateDataParameter(cmd, param);
        }
        public override object ReadFromDataReader(IDataReader reader, int ordinal)
        {
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value || value == null)
                return null;

            //数据库字段类型与属性类型不一致，则转换类型
            if (value is T)
            {
                return value;
            }

            return Convert.ChangeType(value, this.Type);
        }
    }
}
