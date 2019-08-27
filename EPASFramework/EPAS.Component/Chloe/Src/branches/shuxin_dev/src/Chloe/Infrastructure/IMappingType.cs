using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Chloe.Infrastructure
{
    public interface IMappingType
    {
        Type Type { get; }
        DbType DbType { get; }
        IDbDataParameter CreateDataParameter(IDbCommand cmd, DbParam dbParam);
        object ReadFromDataReader(IDataReader reader, int ordinal);
    }
}
