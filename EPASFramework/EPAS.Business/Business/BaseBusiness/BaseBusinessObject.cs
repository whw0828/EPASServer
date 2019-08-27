using EPAS.BaseEntityData;
using EPAS.Business.Utilities;
using EPAS.Component.Utilities;
using EPAS.DataEntity.Entity.Common;
using FPA.BaseEntityDataFac;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Business.BaseObject
{
    /// <summary>
    /// 基础业务实现 实现基础数据的维护、查询、重复校验
    /// </summary>
    public class BaseBusinessObject
    {

        #region 实体通用方法
        /// <summary>
        /// 根据实体类中字段 查询实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public List<T> GetEntityByField<T>(string fieldName, string fieldValue) where T : class
        {
            return BaseEntityFac.GetEntityByField<T>(fieldName, fieldValue);
        }

        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public static List<T> GetEntityByBatchField<T>(string whereSql) where T : class
        {
            return BaseEntityFac.GetEntityByField<T>(whereSql);
        }

        /// <summary>
        /// 根据实体类中字段 查询单个实体信息 fieldName对应的fieldValue一定是表中唯一值
        /// 否则返回空
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public T GetSingleEntityByField<T>(string fieldName, string fieldValue) where T : class
        {
            List<T> itemList=GetEntityByField<T>(fieldName,fieldValue);
            if(itemList!=null && itemList.Count==1)
            {
                return itemList.FirstOrDefault();
            }
            return null;
            
        }



        /// <summary>
        /// 获取实体中所有信息
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllEntitys<T>() where T : class
        {
            List<T> itemList = new List<T>();
            itemList = BaseEntityFac.GetAllEntitys<T>();

            return itemList;

        }


        /// <summary>
        /// 获取实体最新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> RefreshEntityData<T>() where T : class
        {
            return BaseEntityFac.GetAllEntitys<T>();
        }



        /// <summary>
        /// 实体 增删改操作
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="eopType"></param>
        /// <returns></returns>
        public bool NoTransactionOPEntitys<T>(List<T> itemList, EOPType eopType) where T:class
        {          
            return BaseEntityFac.NoTransactionOPEntitys<T>(itemList, eopType);
        }

        /// <summary>
        /// 多实体事务操作
        /// </summary>
        /// <param name="itemLists"></param>
        /// <returns></returns>
        public  bool TransactionOPEntitys<T>(Func<IDbConnection, IDbTransaction, bool> fun) where T : class
        {
            BaseEntityData<T> data = new BaseEntityData<T>();

            return data.TransactionOPEntitys(fun);
        }



        #endregion
        #region 添加一行数据

        /// <summary>
        /// 添加一行空白数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public List<T> AddNewRowData<T>(List<T> itemList) 
        {
            List<T> listInfoNew = new List<T>();

            if (itemList == null)
            {
                itemList = new List<T>();

            }

            T info = ObjectHelper.CreateInstance<T>();

            string tableName = ObjectHelper.GetEntityName<T>();
            string fieldIDName = tableName + BusinessHelper.EPASID;//表主键名称

            //给实体类中主键赋值
            ObjectHelper.SetObjectValueByFieldName<T>(info, fieldIDName, Guid.NewGuid().ToString());

            listInfoNew.Add(info);
            if (itemList != null && itemList.Count > 0)
            {
                listInfoNew.AddRange(itemList);
            }
            return listInfoNew;
        }


        /// <summary>
        /// 新建一条自定义基础数据
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public List<T> AddCustomNewRowData<T>(List<T> itemList, T info=null) where T : class
        {
           
            List<T> listInfoNew = new List<T>();

            if (itemList == null)
            {
                itemList = new List<T>();

            }

            if (info == null)
            {
                info = ObjectHelper.CreateInstance<T>();
            }

            listInfoNew.Add(info);
            if (itemList != null && itemList.Count > 0)
            {
                listInfoNew.AddRange(itemList);
            }
            return listInfoNew;

        }
        #endregion

        #region 保存数据（覆盖业务  新建、修改）

        /// <summary>
        /// 保存实体检查
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList">实体数据集</param>
        /// <param name="idField">主键ID</param>
        /// <param name="codeField">Item编码字段</param>
        /// <param name="nameField">Item名称字段</param>
        /// <returns></returns>
        public bool CheckSaveBaseDataEnable<T>(List<T> itemList, List<string> Fields) where T : class
        {

            return BaseEntityFac.CheckSaveBaseDataEnable(itemList, Fields);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <param name="SaveCheckResult"></param>
        /// <returns></returns>
        public  bool SaveBaseData<T>(List<T> itemList, bool SaveCheckResult) where T : class
        {
            return BaseEntityFac.SaveBaseData(itemList, SaveCheckResult);
        }
        #endregion
    }
}
