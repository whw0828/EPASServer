namespace EPAS.Component
{
    /// <summary>
    /// 分页管理
    /// </summary>
    public class PagingManager
    {
        #region 获取分页sql语句
        /// <summary>
        /// 获得mssql分页sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="order"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private static string GetPagingMSSql(string sql, string order, int pageSize, int pageIndex)
        {
            int beginPos = pageSize * pageIndex;
            int endPos = beginPos + pageSize;
            string querySql = @"
                SELECT *
                FROM (
	                SELECT *, ROW_NUMBER() OVER(ORDER  BY $(order)) AS RN
	                FROM (
		                $(sql)
	                ) T
                ) T
                WHERE RN > " + beginPos.ToString() + @" AND RN <= " + endPos.ToString() + @"
                ORDER BY $(order)
                ";
            querySql = querySql.Replace("$(order)", order);
            querySql = querySql.Replace("$(sql)", sql);

            return querySql;
        }

        /// <summary>
        /// 获得oracle分页语法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="order"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private static string GetPagingOracle(string sql, string order, int pageSize, int pageIndex)
        {
            int beginPos = pageSize * pageIndex;
            int endPos = beginPos + pageSize;

            string querySql = @"
                SELECT * 
                FROM 
                (
	                SELECT T.*, ROWNUM RN 
	                FROM (
		                $(sql)
	                ) T
	                WHERE ROWNUM <= " + endPos.ToString() + @"
                )
                WHERE RN > " + beginPos.ToString() + @"
                ";

            if (!string.IsNullOrEmpty(order))
            {
                querySql += "ORDER BY " + order;
            }

            querySql = querySql.Replace("$(sql)", sql);
            return querySql;
        }

        /// <summary>
        /// 获得mysql分页语法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="order"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private static string GetPagingMySql(string sql, string order, int pageSize, int pageIndex)
        {
            int beginPos = pageSize * pageIndex;
            int endPos = beginPos + pageSize;

            string querySql = @"
                SELECT *
                FROM (
		                $(sql)
                ) T
                ORDER BY $(order)
                LIMIT  " + beginPos.ToString() + @" , " + endPos.ToString() + @"
                ";
            querySql = querySql.Replace("$(order)", order);
            querySql = querySql.Replace("$(sql)", sql);
            return querySql;
        }

        /// <summary>
        /// 获取分页Sql语句
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="sql">未分页sql</param>
        /// <param name="order">顺序</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public static string GetPagingSql(string sql, string order, int pageSize, int pageIndex, out string totalNumSql)
        {
            totalNumSql = @" SELECT COUNT(1) Total FROM ($(sql)) t";
            totalNumSql = totalNumSql.Replace("$(sql)", sql);

            string pagingSql = sql;
            if (DbConnectionHelper.DType == DatabaseType.Oracle)
            {
                pagingSql = GetPagingOracle(sql, order, pageSize, pageIndex);
            }
            else if (DbConnectionHelper.DType == DatabaseType.SqlServer)
            {
                pagingSql = GetPagingMSSql(sql, order, pageSize, pageIndex);
            }
            else if (DbConnectionHelper.DType == DatabaseType.MySql)
            {
                pagingSql = GetPagingMySql(sql, order, pageSize, pageIndex);
            }
            else
            {
            }

            return pagingSql;
        }

        /// <summary>
        /// 直接获得sql分页
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="sql"></param>
        /// <param name="order"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static string GetPagingSql(string sql, string order, int pageSize, int pageIndex)
        {
            string pagingSql = sql;
            if (DbConnectionHelper.DType == DatabaseType.Oracle)
            {
                pagingSql = GetPagingOracle(sql, order, pageSize, pageIndex);
            }
            else if (DbConnectionHelper.DType == DatabaseType.SqlServer)
            {
                pagingSql = GetPagingMSSql(sql, order, pageSize, pageIndex);
            }
            else if (DbConnectionHelper.DType == DatabaseType.MySql)
            {
                pagingSql = GetPagingMySql(sql, order, pageSize, pageIndex);
            }
            else
            {
            }

            return pagingSql;
        }
        #endregion
    }
}
