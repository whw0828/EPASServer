using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPAS.DataEntity.Enum;
using EPAS.Business.UserSystem;
using EPAS.DataEntity.Entity.Common;
using Microsoft.AspNetCore.Mvc;
using EPAS.Component.DataHub;
using EPAS.DataEntity.Entity.MES;
using EPAS.Business.Utilities;
using EPAS.DataEntity.API;
using EPAS.Component.Json;
using System.Collections;
using EPAS.Component.Utilities;
using EPAS.Business.BaseObject;
using FPA.BaseEntityDataFac;

namespace EPAS.API.Controllers
{

    /// <summary>
    /// 适用于基础数据实体操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseEntityBusinessController : ControllerBase
    {

        /// <summary>
        /// 返回数据实体主键后缀,数据库表中主键命名规则：[表名称]+[后缀]。目前后缀 "Id"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetGUIDSuffix()
        {
            return BusinessHelper.EPASID;
        }
        /// <summary>
        /// 返回数据实体中所有记录
        /// </summary>
        /// <param name="cmd">请求命令</param>
        /// <returns></returns>
        [HttpPost]
        public object GetAllEntitys(OPEntityCmdBase cmd)
        {
            BaseBusinessObject _BaseBusinessObject = new BaseBusinessObject();

            #region 动态实体解析

            Type type = ObjectHelper.GetSingleObjectTypeByName(cmd.Namespace, cmd.EntityTypeName);

            #endregion


            object itemList = typeof(BaseBusinessObject).GetMethod("GetAllEntitys").MakeGenericMethod(type)?.Invoke(_BaseBusinessObject, new object[] {});

            return itemList;

            // List <MenuInfo> itemList = JsonUtil.fromJson<List<MenuInfo>>(cmd.EntityJson);
            //return _SysInfoBusiness.NoTransactionOPEntitys<MenuInfo>(itemList, eopType);
        }


        /// <summary>
        /// 返回数据实体中所有记录
        /// </summary>
        /// <param name="cmd">请求命令</param>
        /// <returns></returns>
        [HttpPost]
        public object GetEntityByField(OPEntityCmdField cmd)
        {
            BaseBusinessObject _BaseBusinessObject = new BaseBusinessObject();

            #region 动态实体解析

            Type type = ObjectHelper.GetSingleObjectTypeByName(cmd.Namespace, cmd.EntityTypeName);

            #endregion


            object itemList = typeof(BaseBusinessObject).GetMethod("GetEntityByField").MakeGenericMethod(type)?.Invoke(_BaseBusinessObject, new object[] { cmd.fieldName,cmd.fieldValue});

            return itemList;

            // List <MenuInfo> itemList = JsonUtil.fromJson<List<MenuInfo>>(cmd.EntityJson);
            //return _SysInfoBusiness.NoTransactionOPEntitys<MenuInfo>(itemList, eopType);
        }

        /// <summary>
        /// 返回数据实体中所有记录
        /// </summary>
        /// <param name="cmd">请求命令</param>
        /// <returns></returns>
        [HttpPost]
        public object GetEntityByBatchField(OPEntityCmdBase cmd)
        {
            BaseBusinessObject _BaseBusinessObject = new BaseBusinessObject();

            #region 动态实体解析

            Type type = ObjectHelper.GetSingleObjectTypeByName(cmd.Namespace, cmd.EntityTypeName);

            #endregion


            object itemList = typeof(BaseBusinessObject).GetMethod("GetEntityByBatchField").MakeGenericMethod(type)?.Invoke(_BaseBusinessObject, new object[] { cmd.Tag });

            return itemList;

            // List <MenuInfo> itemList = JsonUtil.fromJson<List<MenuInfo>>(cmd.EntityJson);
            //return _SysInfoBusiness.NoTransactionOPEntitys<MenuInfo>(itemList, eopType);
        }
        /// <summary>
        /// 实体定向业务 增、删、改操作
        /// </summary>
        /// <param name="cmd">OPEntityCmd</param>
        /// <returns></returns>
        [HttpPost]
        public bool NoTransactionOPEntitys(OPEntityCmd cmd)
        {
            BaseBusinessObject _BaseBusinessObject = new BaseBusinessObject();

            #region 动态实体解析
    
            Type type =ObjectHelper.GetSingleObjectTypeByName(cmd.Namespace, cmd.EntityTypeName);

            Type listType= ObjectHelper.GetListObjectTypeByName(cmd.Namespace, cmd.EntityTypeName); 

            var itemList = typeof(JsonUtil).GetMethod("fromJson").MakeGenericMethod(listType)?.Invoke(null, new object[] { cmd.EntityJson});
            #endregion
   
            EOPType eopType = (EOPType)cmd.optype;

            object result= typeof(BaseBusinessObject).GetMethod("NoTransactionOPEntitys").MakeGenericMethod(type)?.Invoke(_BaseBusinessObject, new object[] { itemList, eopType });

            return (bool)result;

            // List <MenuInfo> itemList = JsonUtil.fromJson<List<MenuInfo>>(cmd.EntityJson);
                                                //return _SysInfoBusiness.NoTransactionOPEntitys<MenuInfo>(itemList, eopType);
        }


        /// <summary>
        /// 检查实体数据是否有重复项
        /// </summary>
        /// <param name="cmd">请求命令</param>
        /// <returns></returns>
        [HttpPost]
        public bool CheckSaveBaseDataEnable(OPEntityCmdSaveCheck cmd)
        {
            BaseBusinessObject _BaseBusinessObject = new BaseBusinessObject();

            #region 动态实体解析

            Type type = ObjectHelper.GetSingleObjectTypeByName(cmd.Namespace, cmd.EntityTypeName);
            Type listType = ObjectHelper.GetListObjectTypeByName(cmd.Namespace, cmd.EntityTypeName);
            var itemList = typeof(JsonUtil).GetMethod("fromJson").MakeGenericMethod(listType)?.Invoke(null, new object[] { cmd.EntityJson });
            #endregion

            object result = typeof(BaseBusinessObject).GetMethod("CheckSaveBaseDataEnable").MakeGenericMethod(type)?.Invoke(_BaseBusinessObject, new object[] { itemList, cmd.fields });

            return (bool)result;

        }

        /// <summary>
        /// 实体复合业务 增、改操作 针对Excel模式的数据保存
        /// </summary>
        /// <param name="cmd">OPEntityCmd</param>
        /// <returns></returns>
        [HttpPost]
        public bool SaveBaseData(OPEntityCmdSave cmd)
        {
            BaseBusinessObject _BaseBusinessObject = new BaseBusinessObject();

            #region 动态实体解析

            Type type = ObjectHelper.GetSingleObjectTypeByName(cmd.Namespace, cmd.EntityTypeName);
            Type listType = ObjectHelper.GetListObjectTypeByName(cmd.Namespace, cmd.EntityTypeName); 

            var itemList = typeof(JsonUtil).GetMethod("fromJson").MakeGenericMethod(listType)?.Invoke(null, new object[] { cmd.EntityJson });
            #endregion

            bool SaveCheckResult = cmd.SaveCheckResult;

            object result = typeof(BaseBusinessObject).GetMethod("SaveBaseData").MakeGenericMethod(type)?.Invoke(_BaseBusinessObject, new object[] { itemList, SaveCheckResult });

            return (bool)result;
        }

        /// <summary>
        /// 返回单号的流水号 例如 生产订单、工单、领料单、销售发货单等
        /// </summary>
        /// <param name="serialtype"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetNewSerialNumber(string serialtype)
        {
            return BaseEntityFac.GetNewSerialNumber(serialtype);
        }

        /// <summary>
        /// 返回服务器端时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetServerTime()
        {
            return BaseEntityFac.GetServerTime().ToString();
        }

    }
}
