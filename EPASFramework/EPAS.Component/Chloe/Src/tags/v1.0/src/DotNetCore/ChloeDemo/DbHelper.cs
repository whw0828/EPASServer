using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChloeDemo
{
    public class DbHelper
    {
        public static readonly string ConnectionString = "Data Source = .;Initial Catalog = Chloe;Integrated Security = SSPI;";

        public static DbConnection CreateConnection()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            return conn;
        }

        /// <summary>
        /// 返回结果集的第一行第一列
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlString, params SqlParameter[] cmdParams)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    if (cmdParams != null)
                    {
                        cmd.Parameters.AddRange(cmdParams);
                    }
                    conn.Open();
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return obj;
                }
            }
        }
        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public static int ExecuteNonQurey(string sqlString, params SqlParameter[] cmdParams)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    if (cmdParams != null)
                    {
                        cmd.Parameters.AddRange(cmdParams);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 返回SqlDataReader 调用完此方法需将reader关闭
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sqlString, params SqlParameter[] cmdParams)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            if (cmdParams != null)
            {
                cmd.Parameters.AddRange(cmdParams);
            }
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public static bool Execute(Dictionary<string, SqlParameter[]> dt)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    try
                    {
                        foreach (KeyValuePair<string, SqlParameter[]> kv in dt)
                        {
                            cmd.CommandText = kv.Key;
                            if (kv.Value != null)
                            {
                                cmd.Parameters.AddRange(kv.Value);
                            }
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
