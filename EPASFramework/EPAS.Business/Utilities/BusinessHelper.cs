using EPAS.BaseEntityData;
using EPAS.DataEntity.Enum;
using EPAS.DataEntity.Entity.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Business.Utilities
{
    public  class BusinessHelper
    {

        public static string EPASID = "Id";//数据库表设计主键=   表名称+“Id”

        public static string EPASResetPassword = "123";//用户登录初始密码

        public static string RootParentMenuId = "MenuRoot";


        public static string ImplementCode = "EMAS-19-W-S-W";//实施工号

        //被创建的用户等级=当前用户等级-1
        public static int UserLevel = 9999;//用户等级（最高9999 最低 0）

        /// <summary>
        /// 创建主键值
        /// </summary>
        /// <returns></returns>
        public static string CreateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 获取服务器时间（B/S） 获取本地数据库时间（C/S）
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerTime()
        {
            return DateTime.Now;
        }
        public static void DebugLog(string loginfo)
        {
            Debug.Write(loginfo);
        }



        public static bool StringCompare(string source, string taget)
        {

            if (!string.IsNullOrWhiteSpace(source) && !string.IsNullOrWhiteSpace(taget))
            {
                decimal valueSource = 0;
                decimal valueTaget = 0;
                if (decimal.TryParse(source, out valueSource))
                {
                    if (decimal.TryParse(taget, out valueTaget))
                    {
                        if (valueSource != valueTaget)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return string.Equals(source.Trim(), taget.Trim());
                }

            }
            else if (string.IsNullOrWhiteSpace(source) && string.IsNullOrWhiteSpace(taget))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExcelFieldName">EXCEL中数据对应的字段</param>
        /// <param name="IsTrans">是否换算</param>
        /// <param name="TransType">换算类型</param>
        /// <param name="TransFieldName">换算后的字段</param>
        /// <param name="ClassName">操作表名称或枚举名称</param>
        /// <returns></returns>
        public static ExcelFieldParam AddExcelFieldParam(string ExcelFieldName, CustomEnable IsTrans, ExcelTransType TransType, string TransFieldName="", string ClassName="")
        {
            ExcelFieldParam item = new ExcelFieldParam();
            item.ExcelFieldName = ExcelFieldName;
            item.TransFieldName = TransFieldName;
            item.IsTrans = IsTrans;
            item.TransType = TransType;
            item.ClassName = ClassName;
            return item;
         
        }

  
    }
}
