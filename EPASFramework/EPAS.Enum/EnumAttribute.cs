using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace EPAS.DataEntity.Entity.Common
{
    public  class EnumData
    {
        [DisplayName("ValueMember")]
        public int ValueMember
        {
            get;
            set;
        }
        [DisplayName("DisplayMember")]
        public string DisplayMember
        {
            get;
            set;
        }
    }
    public class EnumAttribute
    {

        public static List<EnumData> EnumToList<T>()
        {
            List<EnumData> itemList = new List<EnumData>();
            T obj = System.Activator.CreateInstance<T>();
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (var field in fields)
            {
                try
                {
                    System.Enum value = field.GetValue(null) as System.Enum;

                    // 获取枚举常数名称。
                    string name = GetDescription(value);
                    int ivalue = (int)field.GetValue(null);

                    EnumData item = new EnumData();
                    item.DisplayMember = name;
                    item.ValueMember = ivalue;
                    itemList.Add(item);
                }
                catch
                {

                }

            }
            return itemList;
        }


        /// <summary>
        /// 获取枚举中所有的信息  适用于名称转值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, int> GetEnumFields(Type obj)
        {
           
            FieldInfo[] fields = obj.GetFields();

            Dictionary<string, int> enumMap = new Dictionary<string, int>();
            foreach (var field in fields)
            {
                try
                {
                    System.Enum value = field.GetValue(null) as System.Enum;
                    
                    // 获取枚举常数名称。
                   string name = GetDescription(value);
                   int ivalue = (int)field.GetValue(null);

                    enumMap.Add(name, ivalue);
                }
                catch
                {

                }
              
            }
            
            return enumMap;
        }
            /// <summary>
            /// 返回枚举项的描述信息。
            /// </summary>
            /// <param name="value">要获取描述信息的枚举项。</param>
            /// <returns>枚举想的描述信息。</returns>
            public static string GetDescription(System.Enum value)
        {
            Type enumType = value.GetType();
            // 获取枚举常数名称。
            string name = System.Enum.GetName(enumType, value);
            if (name != null)
            {
                // 获取枚举字段。
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo,
                        typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {

                        return LocalEnumLanguage(attr.Description);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Eenu 多国语言定义格式
        ///  语言+","+"显示内容"+";" 
        ///  注意分割符全部是英文
        /// </summary>
        /// <param name="des"></param>
        /// <returns></returns>
        private static string LocalEnumLanguage(string des)
        {
            if(des==null)
            {
                return "";
            }
            string local = des;
            try
            {
                Dictionary<string, string> lanMap = new Dictionary<string, string>();// 语言 显示
                char splitDis = ',';//显示分割符
                char splitEnd = ';';//定义结束分割符

                string[] lanArrar = des.Split(splitEnd);

                //系统语言
                string localPCLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                if (lanArrar != null && lanArrar.Length > 0)
                {
                    foreach (var item in lanArrar)
                    {
                        string[] lanDisplay = item.Split(splitDis);

                        if (lanDisplay != null && lanDisplay.Length > 1)
                        {
                            lanMap.Add(lanDisplay[0], lanDisplay[1]);
                        }
                    }
                }
                if (lanMap.ContainsKey(localPCLanguage))
                {
                    des = lanMap[localPCLanguage];
                }

            }
            catch(Exception ex)
            {
                
            }
          
            return des;
        }
    }


}
