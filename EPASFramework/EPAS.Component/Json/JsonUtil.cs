using Jil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EPAS.Component.Json
{
    public class JsonUtil
    {
        #region jil Json
        /// <summary>
        /// 实体转为JSON 默认使用 NewtonSoft
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string toJson(object t)
        {
            try
            {
                string temp = NewtonSoft_ObjectToJson(t);


              return temp;
            }
            catch(Exception ex)
            {
                return "NewtonSoft_ObjectToJson Error";
            }
        }

        /// <summary>
        /// json转为实体  默认使用 NewtonSoft
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T fromJson<T>(string strJson) where T : class
        {
            T t = null;
            if (strJson == null)
                return null;

            try
            {
                t = NewtonSoft_JsonToObject<T>(strJson);

            }
            catch
            {
                t = null;
            }

            return t;
        }

        /// <summary>
        /// 实体转为JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string toJsonIncludeInherited(object t)
        {
            return JSON.Serialize(t, Options.IncludeInherited);
        }



        /// <summary>
        /// JIL Json转实体 非特殊情况 请使用[fromJson]方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T JIL_JsonToObject<T>(string strJson) where T : class
        {
            T t = null;
            if (strJson == null)
                return null;

            try
            {
               t = JSON.Deserialize<T>(strJson);

            }
            catch
            {
                return null;
            }

            return t;
        }

        /// <summary>
        /// JIL 实体转Json 非特殊情况 请使用[toJson]方法
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JIL_ObjectToJson(object t)
        {
            try
            {
                return JSON.Serialize(t, Options.IncludeInheritedUtc);
            }
            catch
            {
                return "JIL_ObjectToJson Error";
            }
        }


        /// <summary>
        /// json转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T fromJsonIncludeInherited<T>(string strJson) where T : class
        {
            T t = null;
            if (strJson == null)
                return null;

            t = JSON.Deserialize<T>(strJson, Options.IncludeInheritedUtc);
            return t;
        }

        /// <summary>
        /// 将json转为动态对象 默认使用 NewtonSoft
        /// </summary>
        /// <param name="aJson"></param>
        /// <returns></returns>
        public static dynamic GetDynamic(string aJson)
        {
            return NewtonSoft_JsonToDynamic(aJson);
            //return JSON.DeserializeDynamic(aJson);
        }

        public static string ReplaceDateTime(string json)
        {
            return Regex.Replace(json, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime datetime = new DateTime(1970, 1, 1);
                datetime = datetime.AddMilliseconds(long.Parse(match.Groups[1].Value));
                datetime = datetime.ToLocalTime();
                return datetime.ToString("yyyy-MM-dd HH:mm:ss");
            });
        }
        #endregion

        #region NewtonSoft Json

        public static T NewtonSoft_JsonToObject<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }

        public static string NewtonSoft_ObjectToJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public static dynamic NewtonSoft_JsonToDynamic(string json)
        {
            try
            {
                return NewtonSoft_JsonToObject<dynamic>(json);
            }
            catch (System.Exception)
            {
                return null;
            }
        }


        #endregion


        #region byte 序列化
        /// <summary>
        /// 将可序列化对象转成Byte数组
        /// </summary>
        /// <param name="obj">对象(对象不能为空)</param>
        /// <returns>返回相关数组</returns>
        public static byte[] ObjectToByteArray<T>(T obj) where T : ISerializable
        {
            if (obj == null)
            {
                byte[] byteArr = new byte[] { };
                return byteArr;
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Close();
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 将可序列化对象转成的byte数组还原为对象
        /// </summary>
        /// <param name="byteArry">byte数组</param>
        /// <returns>相关对象</returns>
        public static T ByteArrayToObject<T>(byte[] byteArry) where T : ISerializable
        {
            if (byteArry != null && byteArry.Length > 0)
            {
                MemoryStream ms = new MemoryStream(byteArry);
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(ms);
            }
            else
            {
                return default(T);
            }
        }
        #endregion
    }
}
