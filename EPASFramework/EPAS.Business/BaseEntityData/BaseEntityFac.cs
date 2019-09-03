using EPAS.BaseEntityData;
using EPAS.Business.Utilities;
using EPAS.Component.Json;
using EPAS.Component.Utilities;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Utilities.SQLServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FPA.BaseEntityDataFac
{
    public static class  BaseEntityFac
    {
        /// <summary>
        /// 获取所有实体信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> GetAllEntitys<T>() where T : class
        {
                BaseEntityData<T> data = new BaseEntityData<T>();
             

           
                 return data.GetAllEntitys();
            
        }

        /// <summary>
        /// 获取所有实体信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetEntityById<T>(string id) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();



            return data.GetEntityById(id);

        }

        /// <summary>
        /// 实体类操作 （增删改）
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="optype"></param>
        /// <returns></returns>
        public static bool NoTransactionOPEntitys<T>(List<T> entitys, EOPType optype) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();
            
            return data.NoTransactionOPEntitys(entitys,optype);

        }

        public static int Execute<T>(string sql, object param = null) where T:class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();

            return data.Execute(sql,param);
        }

        #region  流水号生成
        public static string GetNewSerialNumber(string serialtype)
        {
            BaseEntityData<SerialNumbers> data = new BaseEntityData<SerialNumbers>();

            return data.GetNewSerialNumber(serialtype);

        }
        #endregion

        #region 事务处理
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public static bool TransactionOPEntitysAdd<T>(IDbConnection cn, IDbTransaction transaction, EOPType eop, List<T> itemList) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();

            return data.TransactionOPEntitysAdd<T>(cn, transaction, eop, itemList);

        }
        /// <summary>
        /// 多实体事务操作
        /// </summary>
        /// <param name="itemLists"></param>
        /// <returns></returns>
        public static bool TransactionOPEntitys<T>(Func<IDbConnection, IDbTransaction, bool> fun) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();

            return data.TransactionOPEntitys(fun);
        }
        #endregion



        public static bool CheckSaveBaseDataEnable<T>(List<T> itemList, List<string> Fields) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();

            string idField = ObjectHelper.GetEntityName<T>() + BusinessHelper.EPASID;//数据实体GUID 字段名称
            return data.CheckSaveBaseDataEnable(itemList, idField, Fields);
        }

        public static bool SaveBaseData<T>(List<T> itemList, bool SaveCheckResult=true) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();

            return data.SaveBaseData(itemList, SaveCheckResult);
        }


        /// <summary>
        /// 根据实体中字段自定义查询 旧版本中使用；新版本中使用  GetEntityByField<T>(Expression<Func<T, bool>> infoExps) 替代
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static List<T> GetEntityByField<T>(string fieldName, string fieldValue) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();

            return data.GetEntityByField(fieldName, fieldValue);

        }

        /// <summary>
        /// 实体中组合条件查询，直接被实际业务调用，无法进行框架级别的封装。
        /// 适用于查询结果为多条记录的业务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="infoExps"></param>
        /// <returns></returns>
        public static List<T> GetEntityByField<T>(Expression<Func<T, bool>> infoExps) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();

            string whereSql=MsGenerateSql.DealExpress(infoExps);

            return data.GetEntityByField(whereSql);
        }

        /// <summary>
        /// 实体中组合条件查询，直接被实际业务调用，无法进行框架级别的封装。
        /// 适用于查询结果为单条记录的业务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="infoExps"></param>
        /// <returns></returns>
        public static T GetSingleEntityByField<T>(Expression<Func<T, bool>> infoExps) where T : class
        {

            if (GetEntityByField<T>(infoExps) != null)
            {
                return GetEntityByField<T>(infoExps).FirstOrDefault();
            }

            return null;
        }

        /// <summary>
        /// 不建议使用，新版本中使用  GetEntityByField<T>(Expression<Func<T, bool>> infoExps) 替代
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public static List<T> GetEntityByField<T>(string whereSql) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();

            return data.GetEntityByField(whereSql);
        }


        public static List<T> GetEntityView<T>(string sql) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();
            dynamic msg = data.GetEntityViewJson<T>(sql, new { });

            if ((bool)msg.Msg)
            {
                List<T> itemList = JsonUtil.fromJson<List<T>>((string)(msg.Content));

                return itemList;
            }
            return null;
        }

        /// <summary>
        /// 返回服务器时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerTime()
        {
            return DateTime.Now;
        }

    }

}
