
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Data.SqlClient;

using System.Linq;


namespace EPAS.Component
{
    public class DbConnectionHelper
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        private static readonly string connectionName = "SQLServer";


        //开发环境
        public static  string LicenseConnectStr = "Data Source=mes.zhongkehuazhi.com;Initial Catalog=EPAS;User Id=sa;Password=whw135063;";
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DatabaseType DType = DatabaseType.SqlServer;

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection CreateConnection()
        {
            IDbConnection conn = null;

            #region 从账套信息中获取数据库连接字符串
            //if (string.IsNullOrEmpty(LicenseConnectStr))
            //{
            //    if (WebConfigurationManager.AppSettings["Env"] + "" == "PDAService")
            //    {
            //        LicenseConnectStr = getConnectionStringFromLicenseService();
            //    }


            //}
            #endregion

            string connString = LicenseConnectStr;


            switch (connectionName)
            {
                case "SQLServer":
                    DType = DatabaseType.SqlServer;
                    conn = new SqlConnection(connString);
                    break;
                //case "MySQL":
                //    conn = new MySqlConnection(connString);
                //    break;
                case "Oracle":
                    //DType = DatabaseType.Oracle;
                    //conn = new OracleConnection(connString);
                    //break;
                default:
                    DType = DatabaseType.SqlServer;
                    conn = new SqlConnection(connString);
                    break;
            }
            //conn.Open();

            return conn;
        }

        public static DateTime GetServerTime()
        {
            DateTime servertime = DateTime.Now;
            try
            {
                using (IDbConnection cn = CreateConnection())
                {
                    cn.Open();
                    IDbCommand cmd = cn.CreateCommand();
                    cmd.CommandText = @"select CONVERT(VARCHAR(100),getdate(),21) as servertime";
                    servertime = DateTime.Parse(cmd.ExecuteScalar().ToString());
                    cn.Close();
                    return servertime;
                }
            }
            catch
            {
                return DateTime.Parse("1900-01-01");
            }
        }
        //private static string getConnectionStringFromLicenseService()
        //{
        //    try
        //    {
        //        string ip = WebConfigurationManager.AppSettings["LicenseServerIp"] + "";
        //        string sobId = WebConfigurationManager.AppSettings["SetOfBookId"] + "";
        //        if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(sobId))
        //        {
        //            var client = new LicenseServiceSoapClient();
        //            client.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://" + ip + "/license/LicenseService.asmx");
        //            string deviceId = LicenseManager.GetUniqueId("MES");
        //            string json = client.GetSetOfBookList(deviceId);
        //            var jsonObj = JsonUtil.GetDynamic(json);
        //            if ((bool)jsonObj.ReplySuccess)
        //            {
        //                var result = JsonUtil.fromJson<List<SetOfBook>>((string)jsonObj.Content);
        //                var sob = result.Where(x => x.SetOfBookId == sobId).FirstOrDefault();
        //                if (sob != null)
        //                {
        //                    return sob.SqlServer;
        //                }
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    return string.Empty;
        //}
    }
}
