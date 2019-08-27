using System;
using System.Collections.Generic;
using System.Linq;
using EPAS.BaseEntityData;
using EPAS.BDA;
using EPAS.Business.BaseObject;
using EPAS.DataEntity.Enum;
using EPAS.Business.Utilities;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Entity.MES;
using EPAS.Interface.BusinessInterface;

namespace EPAS.Business.UserSystem
{
    /// <summary>
    /// 用户业务
    /// </summary>
    public class UserRoleRelationshipBusiness : BaseBusinessObject,IQueryData<UserRoleRelationship>
    {
        UserRoleRelationshipData _Data = new UserRoleRelationshipData();

        #region 业务数据查询
        /// <summary>
        /// 根据实体类 主键 查询实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public UserRoleRelationship GetEntityById(string id)
        {
            return _Data.GetEntityById(id);
        }

        /// <summary>
        /// 返回所有实体信息
        /// </summary>
        /// <returns></returns>
        public List<UserRoleRelationship> GetAllEntitys()
        {
            return _Data.GetAllEntitys();
        }

        /// <summary>
        /// 根据实体类中字段 查询实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public List<UserRoleRelationship> GetEntityByField(string fieldName, string fieldValue)
        {
            return _Data.GetEntityByField(fieldName, fieldValue);
        }

        #endregion

        #region 业务数据操作

     
     

        /// <summary>
        /// 新添加基础数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList">已存在的基础数据</param>
        /// <returns></returns>
        public List<UserRoleRelationship> CreateBaseData(List<UserRoleRelationship> itemList)
        {
            UserRoleRelationship info = new UserRoleRelationship();

            return this.AddNewRowData<UserRoleRelationship>(itemList);
        }


        /// <summary>
        /// 获取最新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<UserRoleRelationship> RefreshBaseData()
        {
          return _Data.GetAllEntitys();
        }

        /// <summary>
        /// 删除基础数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public bool DeleteBaseData(List<UserRoleRelationship> itemList)
        {
            return _Data.NoTransactionOPEntitys(itemList, EOPType.Delete);
        }

        /// <summary>
        /// 获取文件中基础数据并保存到数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public bool ImportBaseDataFromFile(string filePath,string sheetName)
        {


            return true;
        }


        /// <summary>
        /// 导出数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ExportBaseDataToFile(string filePath)
        {
            return true;
        }

        #endregion

    }
}
