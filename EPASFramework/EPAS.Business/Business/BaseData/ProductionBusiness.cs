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

namespace EPAS.Business.BaseData
{
    /// <summary>
    /// 客户物料信息业务实现
    /// </summary>
    public class ProductionBusiness : BaseBusinessObject, IQueryData<Production>, IBaseDataBusiness<Production>
    {
        ProductionData _Data = new ProductionData();

        #region 业务数据查询
        /// <summary>
        /// 根据实体类 主键 查询实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public Production GetEntityById(string id)
        {
            return _Data.GetEntityById(id);
        }

        /// <summary>
        /// 返回所有实体信息
        /// </summary>
        /// <returns></returns>
        public List<Production> GetAllEntitys()
        {
            return _Data.GetAllEntitys();
        }

        /// <summary>
        /// 根据实体类中字段 查询实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public List<Production> GetEntityByField(string fieldName, string fieldValue)
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
        public List<Production> CreateBaseData(List<Production> itemList)
        {
            Production info = new Production();
            return this.AddNewRowData<Production>(itemList);
            
        }
        /// <summary>
        /// 基础数据保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public bool SaveBaseData(List<Production> itemList)
        {
            return _Data.SaveBaseData(itemList, CheckSaveBaseDataEnable(itemList));         
        }

        /// <summary>
        /// 基础数据保存检查  防止录入重复数据（例如 编码、名称重复）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <returns></returns>
        private bool CheckSaveBaseDataEnable(List<Production> itemList)
        {
           return true;
          
        }

        /// <summary>
        /// 获取最新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<Production> RefreshBaseData()
        {
          return _Data.GetAllEntitys();
        }

        /// <summary>
        /// 删除基础数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public bool DeleteBaseData(List<Production> itemList)
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
            Dictionary<string, ExcelFieldParam> ncMap = new Dictionary<string, ExcelFieldParam>();

            ncMap.Add("客户物料编码", BusinessHelper.AddExcelFieldParam("ProductionNo", CustomEnable.DisEnable, ExcelTransType.NullTrans));
            ncMap.Add("客户物料名称", BusinessHelper.AddExcelFieldParam("ProductionName", CustomEnable.DisEnable, ExcelTransType.NullTrans));

            ncMap.Add("生产物料编码", BusinessHelper.AddExcelFieldParam("MaterialNum", CustomEnable.Enable,
                ExcelTransType.EntityTrans, "MaterialNumberId", "MaterialNumber"));//需要实体字段换算

            List<Production> itemList = _Data.ExcelDataToEntityList(filePath, sheetName, ncMap);

            return this.SaveBaseData(itemList);
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
