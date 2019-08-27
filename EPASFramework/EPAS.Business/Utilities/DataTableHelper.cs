using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using System.Reflection;

namespace FPA.EPAS.Business.Utilities
{


/// <summary>
/// DataTableToModel 的摘要说明
/// </summary>
    public static class DataTableHelper
    {

        /// <summary>
        /// 实体类转换成DataTable
        /// 调用示例：DataTable dt= GetDataTableFromEntity(Entitylist.ToList());
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public static DataTable GetDataTableFromEntity<T>(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            DataTable dt = CreateData(modelList[0]);//创建表结构

            foreach (T model in modelList)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
                }

                dt.Rows.Add(dataRow);
                dataRow.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        private static DataTable CreateData<T>(T model)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.Name != "CTimestamp")//些字段为oracle中的Timesstarmp类型
                {
                    dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
                }
                else
                {
                    dataTable.Columns.Add(new DataColumn(propertyInfo.Name, typeof(DateTime)));
                }
            }
            return dataTable;
        }


        /// <summary>
        /// 将DataTable转为实体对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="sourceDT">原DT</param>
        /// <returns>转换后的实体列表</returns>
        public static List<T> GetEntityFromDataTable<T>(DataTable sourceDT) where T : class
        {
            List<T> list = new List<T>();
            // 获取需要转换的目标类型
            Type type = typeof(T);
            foreach (DataRow dRow in sourceDT.Rows)
            {
                // 实体化目标类型对象
                object obj = Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    // 给目标类型对象的各个属性值赋值
                    prop.SetValue(obj, dRow[prop.Name], null);
                }
                list.Add(obj as T);
            }
            return list;
        }
    

    }

}
