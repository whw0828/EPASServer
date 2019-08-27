using EPAS.Component.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAS.Component.Utilities;
namespace EPAS.Component.DataHub
{
    /// <summary>
    /// DataHub通讯封装接口
    /// </summary>
   public class DataHubHelper
    {
        //通用访问路径
        public static string apiUrl = "/api/DatahubService/DoOperation";//钉钉、微信

        #region SAP  Data Hub 定义信息

        public static string SAPBaseUrl = "http://192.168.1.228:9000/S2A";
        public static string SAPApiUrl = "/api/DatahubWebService/DoOperation";

        public static string SAPSubSystemName = "HanaServiceLayer";

        #endregion

        #region Weixin  Data Hub 定义信息

        public static string WeixinBaseUrl = "http://192.168.1.228:9100";
        public static string WexinSubSystemName = "weixin";

        #endregion

        /// <summary>
        /// 读取 Data Hub中数据  2019年8月15日 测试成功 现与于定版
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl"></param>
        /// <param name="apiUrl"></param>
        /// <param name="subSystemNameField"></param>
        /// <param name="commandTextField"></param>
        /// <returns></returns>
        public static List<T> GetDataFromDataHub<T>(string baseUrl, string apiUrl, string subSystemName, string commandText)
        {
            try
            {
                var apiClient = new RestClient(baseUrl);
                //var apiClient = new RestClient("http://39.106.98.161:8000/S2A");    //连接发布测试机器
                //var apiClient = new RestClient("http://192.168.1.110/apiTransform");//连接慧垚测试机器
                var request = new RestRequest(apiUrl, Method.POST);

                request.AddJsonBody(new
                {
                    CommandText = commandText,
                    SubSystemName = subSystemName
                });

                IRestResponse response = apiClient.Execute(request);

                dynamic content = JsonUtil.GetDynamic(response.Content);

                List<dynamic> tempitemlist = JsonUtil.JIL_JsonToObject<List<dynamic>>((string)content.Result.Content);

                List<T> itemlist = ObjectHelper.ToTargetEntitys<T>(tempitemlist);

                return itemlist;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }


        /// <summary>
        /// 上传 Data Hub 数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl"></param>
        /// <param name="apiUrl"></param>
        /// <param name="subSystemNameField"></param>
        /// <param name="commandTextField"></param>
        /// <returns></returns>
        public static bool UpLoadDataToDataHub(string baseUrl, string apiUrl, string subSystemName, string commandText)
        {
            bool result = false;
            try
            {
                var apiClient = new RestClient(baseUrl);
                //var apiClient = new RestClient("http://39.106.98.161:8000/S2A");    //连接发布测试机器
                //var apiClient = new RestClient("http://192.168.1.110/apiTransform");//连接慧垚测试机器
                var request = new RestRequest(apiUrl, Method.POST);

                request.AddJsonBody(new
                {
                    CommandText = commandText,
                    SubSystemName = subSystemName
                });

                IRestResponse response = apiClient.Execute(request);

                dynamic content = JsonUtil.GetDynamic(response.Content);

                int item = 0;

                int.TryParse((string)content.Result.Code, out item);

                if (item == 1)
                {
                    result = true;
                }
                return result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return result;
            }
        }
    }
}
