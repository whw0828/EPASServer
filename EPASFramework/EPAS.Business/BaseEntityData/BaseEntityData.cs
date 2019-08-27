using EPAS.PluginManager;
using EPAS.DataEntity.Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using EPAS.Interface.BusinessInterface;
using EPAS.Data.DirectSQLPlugin;
using EPAS.Data.ChloePlugin;
using EPAS.Component.Json;
using EPAS.Business.Utilities;
using EPAS.Component.Excel;
using EPAS.DataEntity.Enum;
using System.Linq;
using EPAS.Component.Utilities;

namespace EPAS.BaseEntityData

{
    public class BaseEntityData<T> : ICustomQueryData<T> where T : class 
    {
        PluginManager<T> pm = PluginManager<T>.Instance;

        public BaseEntityData()
        {
            //配置数据插件
            pm.AddPlugin(DataPluginName.DefaultDataPlugin, new DirectSQLPlugin<T>());
            pm.AddPlugin(DataPluginName.ChloeDataPlugin, new ChloePlugin<T>());

            //Chloe 对实体在主键上进行强耦合，不建议使用 实体操作、事务相关方法。其多表查询（视图）目前有借鉴意义。
            //pm.SetCurrentDataPlugin(DataPluginName.ChloeDataPlugin);

            pm.SetCurrentDataPlugin(DataPluginName.DefaultDataPlugin);
        }

        #region 实体类数据保存（新建保存  修改保存）

        //作废函数 作废原因：通用有局限性  只能校验两个字段
        //public bool CheckSaveBaseDataEnable(List<T> itemList,string idField,string codeField,string nameField)
        //{
        //    foreach (var item in itemList)
        //    {
        //        string codeValue = ObjectHelper.GetObjectValueByFieldName<T>(item, codeField).ToString();
        //        string nameValue = ObjectHelper.GetObjectValueByFieldName<T>(item, nameField).ToString();
        //        string IdValue= ObjectHelper.GetObjectValueByFieldName<T>(item, idField).ToString();

        //        List<T> itemTempList = itemList.FindAll(x =>
        //        ObjectHelper.GetObjectValueByFieldName<T>(x, codeField).ToString() == codeValue);

        //        if (itemTempList.Count > 1)
        //        {
        //            return false;
        //        }
        //        itemTempList = itemList.FindAll(x =>
        //        ObjectHelper.GetObjectValueByFieldName<T>(x, nameField).ToString() == nameValue);

        //        if (itemTempList.Count > 1)
        //        {
        //            return false;
        //        }

            
        //        if (string.IsNullOrWhiteSpace(codeValue) || string.IsNullOrWhiteSpace(nameValue))
        //        {
        //            return false;
        //        }
        //        List<T> itemNowCopyList = this.GetEntityByField(codeField, codeValue);

        //        if (itemNowCopyList != null && itemNowCopyList.Count > 0)
        //        {
        //            if (itemNowCopyList.Count > 1)
        //            {
        //                return false;
        //            }
        //            else
        //            {
               
        //                foreach(var itemCopy in itemNowCopyList)
        //                {
        //                    string IdCopyValue= ObjectHelper.GetObjectValueByFieldName<T>(itemCopy, idField).ToString();
        //                    if (IdCopyValue != IdValue)
        //                    {
        //                        return false;
        //                    }
        //                }
                       
        //            }
        //        }
        //    }

        //    return true;
        //}


        public bool CheckSaveBaseDataEnable(List<T> itemList, string idField, List<string> Fields)
        {
            foreach (var item in itemList)
            {
               // string codeValue = ObjectHelper.GetObjectValueByFieldName<T>(item, codeField).ToString();
               // string nameValue = ObjectHelper.GetObjectValueByFieldName<T>(item, nameField).ToString();
                string IdValue = ObjectHelper.GetObjectValueByFieldName<T>(item, idField).ToString();

                for(int i=0;i< Fields.Count;i++)
                {
                    string fieldName = Fields[i];
                    string fieldValue = ObjectHelper.GetObjectValueByFieldName<T>(item, fieldName).ToString();

                    if (string.IsNullOrWhiteSpace(fieldValue))
                    {
                        return false;
                    }

                    List<T> itemTempList = itemList.FindAll(x =>
                    ObjectHelper.GetObjectValueByFieldName<T>(x, fieldName).ToString() == fieldValue);

                    if (itemTempList.Count > 1)
                    {
                        return false;
                    }

                    List<T> itemNowCopyList = this.GetEntityByField(fieldName, fieldValue);

                    if (itemNowCopyList != null && itemNowCopyList.Count > 0)
                    {
                        if (itemNowCopyList.Count > 1)
                        {
                            return false;
                        }
                        else
                        {

                            foreach (var itemCopy in itemNowCopyList)
                            {
                                string IdCopyValue = ObjectHelper.GetObjectValueByFieldName<T>(itemCopy, idField).ToString();
                                if (IdCopyValue != IdValue)
                                {
                                    return false;
                                }
                            }

                        }
                    }

                }
                           
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemList">需要保存的数据</param>
        /// <param name="SaveCheckResult">是否符合保存条件</param>
        /// <returns></returns>
        public bool SaveBaseData(List<T> itemList,bool SaveCheckResult=true)
        {
            List<T> insertList = new List<T>();
            List<T> updateList = new List<T>();

            if(itemList==null)
            {
                return false;
            }
            if (!SaveCheckResult)
            {
                return false;
            }

            foreach (var item in itemList)
            {
                string tableName = ObjectHelper.GetEntityName<T>();
                string fieldName = tableName + BusinessHelper.EPASID;

                string id = ObjectHelper.GetObjectValueByFieldName<T>(item,fieldName).ToString();

                T itemNow = this.GetEntityById(id);

                if (itemNow != null)
                {
                    updateList.Add(item);
                }
                else
                {
                    insertList.Add(item);
                }
            }

            return TransactionOPEntitys((cn, transaction) =>
             {
                 bool result = false;
                 result = TransactionOPEntitysAdd<T>(cn, transaction, EOPType.Insert, insertList);

                 result = TransactionOPEntitysAdd<T>(cn, transaction, EOPType.Update, updateList);

                 return result;
             });

        }


        #endregion


        #region EXCEL数据转实体数据
        public List<T>  ExcelDataToEntityList(string filePath,string sheetName,Dictionary<string, ExcelFieldParam> ncMap)
        {

            Dictionary<int, string> ncMapIndex = new Dictionary<int, string>();


            DataTable dtImport = ExcelHelper.ReadExcelFileToDataTable(filePath, sheetName, true);

            int rowCount = dtImport.Rows.Count;
            int columCount = dtImport.Columns.Count;

            string tableName = ObjectHelper.GetEntityName<T>();
            string fieldIDName = tableName + BusinessHelper.EPASID;//表主键名称

            for (int colum = 0; colum < columCount; colum++)
            {
                string columName = dtImport.Columns[colum].Caption;
                if (ncMap.ContainsKey(columName))
                {
                    ncMapIndex.Add(colum, columName);
                }
            }

            List<T> itemList = new List<T>();
            for (int row = 0; row < rowCount; row++)
            {
                T item = System.Activator.CreateInstance<T>();
                //给实体类中主键赋值
                ObjectHelper.SetObjectValueByFieldName<T>(item, fieldIDName, Guid.NewGuid().ToString());

                for (int colum = 0; colum < columCount; colum++)
                {
                    if (ncMapIndex.ContainsKey(colum))
                    {
                        string displayName = ncMapIndex[colum];
                        string fieldName = ncMap[displayName].ExcelFieldName;

                        object value = dtImport.Rows[row][colum];

                        //需要换算
                        if(ncMap[displayName].IsTrans==CustomEnable.Enable)
                        {
                            Type type = Type.GetType(ncMap[displayName].ClassName);//获取类 ""里为 命名空间.类名

                            if (ncMap[displayName].TransType == ExcelTransType.EnumTrans)
                            {
                                //枚举换算
                                Dictionary<string, int> enumMap = EnumAttribute.GetEnumFields(type);

                                if (enumMap.ContainsKey(value.ToString()))
                                {
                                    value = enumMap[value.ToString()];
                                }
                            }
                            else if (ncMap[displayName].TransType == ExcelTransType.EntityTrans)
                            {
                                //实体换算

                               List<string> strList=GetTransEntityByField<string>(ncMap[displayName].TransFieldName,
                                    ncMap[displayName].ExcelFieldName, value.ToString(), ncMap[displayName].ClassName);

                                if(strList!=null && strList.Count>0)
                                {
                                    value = strList.FirstOrDefault();
                                }

                                fieldName = ncMap[displayName].TransFieldName;

                            }



                        }

                        ObjectHelper.SetObjectValueByFieldName<T>(item, fieldName, value);
                    }

                }

                itemList.Add(item);

            }

            return itemList;
        }
        #endregion


       

        public int Execute(string sql, object param = null)
        {
            int result = -1;
            dynamic msg = JsonUtil.GetDynamic(pm.GetCurrentDataPlugin().Execute(sql, param));

            if ((bool)msg.Msg)
            {
                result = int.Parse(msg.Content);
            }

            return result;
        }

     
        //返回序列号
        public string GetNewSerialNumber(string serialtype)
        {
            return pm.GetCurrentDataPlugin().GetNewSerialNumber(serialtype);
        }

        /// <summary>
        /// 根据实体类 主键 查询实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public T GetEntityById(string id)
        {
            dynamic msg = JsonUtil.GetDynamic(pm.GetCurrentDataPlugin().GetEntityById(id));

            if ((bool)msg.Msg)
            {
                T item = JsonUtil.fromJson<T>((string)(msg.Content));
                return item;
            }

            return null;

        }

        /// <summary>
        /// 返回所有实体信息
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllEntitys()
        {
            dynamic msg = JsonUtil.GetDynamic(pm.GetCurrentDataPlugin().GetAllEntitys());

            if((bool)msg.Msg)
            {
                List<T> itemList = JsonUtil.fromJson<List<T>>((string)(msg.Content));
                return itemList;
            }

            return null;
            
        }



        public List<T> GetEntityByField(string fieldName,string fieldValue)
        {


            string tableName = ObjectHelper.GetEntityName<T>();

            string sql = @"select * from " + " " + tableName + " " + " As [" + tableName + "]  where " + " " + "["+ tableName + "].["+ fieldName +"] = @fieldValue";

            dynamic msg = this.GetEntityViewJson<T>(sql, new { fieldValue = fieldValue });

            if ((bool)msg.Msg)
            {
                List<T> itemList = JsonUtil.fromJson<List<T>>((string)(msg.Content));

                return itemList;
            }

            return null;

        }

        public List<T> GetEntityByField(string whereSql)
        {


            string tableName = ObjectHelper.GetEntityName<T>();

            string sql = @"select * from " + " " + tableName + " "+" As ["+ tableName + "]  where " + " " + whereSql;

            dynamic msg = this.GetEntityViewJson<T>(sql, new {});

            if ((bool)msg.Msg)
            {
                List<T> itemList = JsonUtil.fromJson<List<T>>((string)(msg.Content));

                return itemList;
            }

            return null;

        }



        /// <summary>
        /// 获取外键对应表的字段信息
        /// </summary>
        /// <typeparam name="TV"></typeparam>
        /// <param name="tagetFieldName">目标字段名称</param>
        /// <param name="sourceFieldName">源字段名称</param>
        /// <param name="fieldValue">源字段值</param>
        /// <param name="tableName">外键对应数据库表</param>
        /// <returns></returns>
        public List<TV> GetTransEntityByField<TV>(string tagetFieldName,string sourceFieldName, string fieldValue,string tableName)
        {


            string sql = @"select "+tagetFieldName+" from " + " " + tableName + " " + " where " + " " + sourceFieldName + "= @fieldValue";

            dynamic msg = this.GetEntityViewJson<TV>(sql, new { fieldValue = fieldValue });

            if ((bool)msg.Msg)
            {
                List<TV> itemList = JsonUtil.fromJson<List<TV>>((string)(msg.Content));

                return itemList;
            }

            return null;

        }


        public dynamic GetEntityViewJson<TV>(string sql, object param = null)
        {
            return JsonUtil.GetDynamic(pm.GetCurrentDataPlugin().GetEntityViewJson<TV>(sql,param)); 
        }


        public dynamic GetChloeEntityViewJson<TV>(string sql, object param = null)
        {
            return JsonUtil.GetDynamic(pm.GetCurrentDataPlugin().GetEntityViewJson<TV>(sql, param));
        }

        /// <summary>
        /// 实体类操作 （增删改）
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="optype"></param>
        /// <returns></returns>
        public bool NoTransactionOPEntitys(List<T> entitys, EOPType optype)
        {
            if(entitys==null)
            {
                return false;
            }
            return pm.GetCurrentDataPlugin().NoTransactionOPEntitys(entitys,optype);

        }

        #region 事务处理
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public bool TransactionOPEntitysAdd<TE>(IDbConnection cn, IDbTransaction transaction, EOPType eop, List<TE> itemList) where TE : class
        {

            return pm.GetCurrentDataPlugin().TransactionOPEntitysAdd<TE>(cn,transaction,eop,itemList);

        }
        /// <summary>
        /// 多实体事务操作
        /// </summary>
        /// <param name="itemLists"></param>
        /// <returns></returns>
        public bool TransactionOPEntitys(Func<IDbConnection, IDbTransaction, bool> fun)
        {
            return pm.GetCurrentDataPlugin().TransactionOPEntitysCommit(fun);            
        }
        #endregion

    }
}
