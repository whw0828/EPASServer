using Chloe.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Chloe
{
    public static class DbSessionExtension
    {
        public static DataTable ExecuteDataTable(this IDbSession dbSession, string cmdText, params DbParam[] parameters)
        {
            using (var reader = dbSession.ExecuteReader(cmdText, parameters))
            {
                DataTable dt = DbHelper.FillDataTable(reader);
                return dt;
            }
        }
        public static DataTable ExecuteDataTable(this IDbSession dbSession, string cmdText, CommandType cmdType, params DbParam[] parameters)
        {
            using (var reader = dbSession.ExecuteReader(cmdText, cmdType, parameters))
            {
                DataTable dt = DbHelper.FillDataTable(reader);
                return dt;
            }
        }
        public static DataSet ExecuteDataSet(this IDbSession dbSession, string cmdText, params DbParam[] parameters)
        {
            using (var reader = dbSession.ExecuteReader(cmdText, parameters))
            {
                DataSet ds = DbHelper.FillDataSet(reader);
                return ds;
            }
        }
        public static DataSet ExecuteDataSet(this IDbSession dbSession, string cmdText, CommandType cmdType, params DbParam[] parameters)
        {
            using (var reader = dbSession.ExecuteReader(cmdText, cmdType, parameters))
            {
                DataSet ds = DbHelper.FillDataSet(reader);
                return ds;
            }
        }
    }
}
