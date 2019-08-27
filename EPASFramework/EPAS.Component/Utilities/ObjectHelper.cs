using EPAS.Component.AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Component.Utilities
{
    public class ObjectHelper
    {
        private static Dictionary<string, Type> golableDataEntityTypeMap = new Dictionary<string, Type>();

        /// <summary>
        /// 创建模板实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
        {
         
            T obj = System.Activator.CreateInstance<T>();

            return obj;
        }
  

        /// <summary>
        /// 根据类名 创建实例   作废方法
        /// </summary>
        /// <param name="classNameSpace">命名空间.类名</param>
        /// <returns></returns>
        public static object CreateInstanceByClassName(string classNameSpace)
        {
            Type type = Type.GetType(classNameSpace);//获取类 ""里为 命名空间.类名
            Object obj =(type);

            return obj;
        }

        /// <summary>
        /// 根据实体名称定义 获取单个实体类型
        /// </summary>
        /// <param name="Namespace"></param>
        /// <param name="EntityTypeName"></param>
        /// <returns></returns>
        public static Type GetSingleObjectTypeByName(string Namespace,string EntityTypeName)
        {
            Type type = null;
            string key = Namespace + EntityTypeName;
            if (golableDataEntityTypeMap.ContainsKey(key))
            {
                type = golableDataEntityTypeMap[key];
            }
            else
            {
                type = AppDomain.CurrentDomain.GetAssemblies()?
                             .SelectMany(x => x.GetTypes())?
                             .Where(x => x.Namespace == Namespace)?.Where(t => t.Name
            == EntityTypeName)?.FirstOrDefault();

                if(type!=null)
                {
                    golableDataEntityTypeMap.Add(key, type);
                }
            }

            return type;
        }

        /// <summary>
        /// 根据实体名称定义 获取List实体类型 例如 List<object> 类型
        /// </summary>
        /// <param name="Namespace"></param>
        /// <param name="EntityTypeName"></param>
        /// <returns></returns>
        public static Type GetListObjectTypeByName(string Namespace, string EntityTypeName)
        {
            Type type = GetSingleObjectTypeByName(Namespace, EntityTypeName);

            Type listType = typeof(List<>).MakeGenericType(type);

            return listType;
        }



        /// <summary>
        /// 获取实体对象的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetEntityName<T>()
        {
            T info = System.Activator.CreateInstance<T>();
            Type type = info.GetType();

            return type.Name;
        }

        /// <summary>
        /// 返回模板类的类型 （例如 实体名称、命名空间）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Type GetEntityType<T>()
        {
            Type type = typeof(T);
            return type;
        }


        #region API返回的JSON对象的转换业务
        //如果两个对象中字段转换的类型不存在，请在本文件SetObjectValueByFieldName方法中自定义实现
        //方法有效依赖 使用JiI Json类库转的对象 不支持newton
        public static T ToTargetEntity<T>(object jsonObject)
        {
            var entity = Activator.CreateInstance<T>();

            var properties = entity.GetType().GetProperties();

            if (properties == null)
                return entity;

            Type t = jsonObject.GetType();

            var fi = t.GetField("ObjectMembers", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);


            foreach (var entry in properties)
            {

                var sourceValue = ((IDictionary)fi.GetValue(jsonObject))[entry.Name];

                if (sourceValue != null)
                {

                    SetObjectValueByFieldName<T>(entity, entry.Name, sourceValue.ToString().Replace("\"", ""));
                }

            }
            return entity;
        }

        public static List<T> ToTargetEntitys<T>(IEnumerable source)
        {
            List<T> itemlist = new List<T>();
            foreach (var item in source)
            {
                T entity = ToTargetEntity<T>(item);
                itemlist.Add(entity);
            }

            return itemlist;

        }

        #endregion
        /// <summary>
        /// 获取实体中变量的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetEntityPropertyName<T>(Expression<Func<T, object>> expr)
        {
            var rtn = "";
            if (expr.Body is UnaryExpression)
            {
                rtn = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression)
            {
                rtn = ((MemberExpression)expr.Body).Member.Name;
            }
            else if (expr.Body is ParameterExpression)
            {
                rtn = ((ParameterExpression)expr.Body).Type.Name;
            }
            return rtn;
        }

        /// <summary>
        /// 根据变量名称 获取变量值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetObjectValueByFieldName<T>(T info, string fieldName)
        {

            return info.GetType().GetProperty(fieldName).GetValue(info);

        }

        /// <summary>
        /// 根据变量名称 设置变量值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public static void SetObjectValueByFieldName<T>(T info, string fieldName, object value)
        {
            try
            {
                PropertyInfo p = info.GetType().GetProperty(fieldName);


                Type defineType = p.PropertyType;

                if (defineType == typeof(string))
                {
                    info.GetType().GetProperty(fieldName).SetValue(info, value);
                }
                else if (defineType == typeof(decimal))
                {
                    decimal tempValue = 0;
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        tempValue = 0;
                    }
                    else
                    {
                        tempValue = decimal.Parse(value.ToString());
                    }

                    info.GetType().GetProperty(fieldName).SetValue(info, tempValue);
                }
                else if (defineType == typeof(int))
                {
                    int tempValue = int.Parse(value.ToString());
                    info.GetType().GetProperty(fieldName).SetValue(info, tempValue);
                }
                else if (defineType == typeof(DateTime))
                {
                    //针对 Json Object的特殊处理
                    DateTime tempValue = DateTime.Parse(value.ToString().Replace("\\", ""));
                    info.GetType().GetProperty(fieldName).SetValue(info, tempValue);
                }
                else if (defineType == typeof(DateTime?))
                {
                    if (!string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        //针对 Json Object的特殊处理
                        DateTime tempValue = DateTime.Parse(value.ToString().Replace("\\", ""));
                        info.GetType().GetProperty(fieldName).SetValue(info, tempValue);
                    }

                }


                else
                {
                    if(!defineType.IsGenericType)
                    {
                        //非系统常规类型
                        info.GetType().GetProperty(fieldName).SetValue(info, value);
                    }
                }
  

            }
            catch(Exception ex)
            {
                Debug.Write(ex.Message);
            }



        }

        public static T DeepClone<T>(T oModel)
        {
            var oRes = default(T);
            var oType = typeof(T);

            //得到新的对象对象
            oRes = (T)Activator.CreateInstance(oType);

            //给新的对象复制
            var lstPro = oType.GetProperties();
            foreach (var oPro in lstPro)
            {
                //从旧对象里面取值
                var oValue = oPro.GetValue(oModel);

                //复制给新的对象
                oPro.SetValue(oRes, oValue);
            }

            return oRes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="source"></typeparam>
        /// <typeparam name="taget"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static List<taget> ObjectListMap<source,taget>(List<source> sourceList) where taget : class
        {
            List<taget> itemList = new List<taget>();

            itemList= AutoMapperEx.MapToList<taget>(sourceList);

            return itemList;
        }


        /// <summary>
        /// 单个实体映射
        /// </summary>
        /// <typeparam name="source"></typeparam>
        /// <typeparam name="taget"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static taget ObjectMap<source, taget>(source sourceItem) where taget : class
        {
            taget item = AutoMapperEx.MapTo<taget>(sourceItem);

            return item;
        }

        /// <summary>
        /// 更新列表中字段值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList"></param>
        /// <returns></returns>
        public static List<T> UpdateList<T>(List<T> sourceList) where T : class
        {
            List<T> itemList = new List<T>();

           

            return itemList;
        }



        #region 添加一行数据实体数据

        /// <summary>
        /// 添加一行空白数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <param name="top">默认首行添加</param>
        /// <returns></returns>
        public static List<T> AddRowData<T>(List<T> itemList,string IdSuffix,bool top=true)
        {
            List<T> listInfoNew = new List<T>();

            if (itemList == null)
            {
                itemList = new List<T>();

            }

            T info = ObjectHelper.CreateInstance<T>();

            string tableName = ObjectHelper.GetEntityName<T>();
            string fieldIDName = tableName + IdSuffix;// BusinessHelper.EPASID;//表主键名称

            //给实体类中主键赋值
            ObjectHelper.SetObjectValueByFieldName<T>(info, fieldIDName, Guid.NewGuid().ToString());

            if(top)
            {
                //将数据添加到首行
                listInfoNew.Add(info);
                if (itemList != null && itemList.Count > 0)
                {
                    listInfoNew.AddRange(itemList);
                }
            }else
            {
                //将数据添加到末尾
                if (itemList != null && itemList.Count > 0)
                {
                    listInfoNew.AddRange(itemList);
                }
                listInfoNew.Add(info);
                
            }
           
            return listInfoNew;
        }


        /// <summary>
        /// 新建一条自定义基础数据
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public static List<T> AddRowData<T>(List<T> itemList, string IdSuffix, T info = null, bool top = true) where T : class
        {

            List<T> listInfoNew = new List<T>();

            if (itemList == null)
            {
                itemList = new List<T>();

            }

            string tableName = ObjectHelper.GetEntityName<T>();
            string fieldIDName = tableName + IdSuffix;// BusinessHelper.EPASID;//表主键名称

            //给实体类中主键赋值
            ObjectHelper.SetObjectValueByFieldName<T>(info, fieldIDName, Guid.NewGuid().ToString());


            if (top)
            {
                //将数据添加到首行
                listInfoNew.Add(info);
                if (itemList != null && itemList.Count > 0)
                {
                    listInfoNew.AddRange(itemList);
                }
            }
            else
            {
                //将数据添加到末尾
                if (itemList != null && itemList.Count > 0)
                {
                    listInfoNew.AddRange(itemList);
                }
                listInfoNew.Add(info);

            }
            return listInfoNew;

        }
        #endregion

    }
}
