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

namespace EPAS.API.Controllers
{

    /// <summary>
    /// 适用于客户端框架未实现用户模块的场景
    /// 用户、角色、菜单
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SAPDataHubController : ControllerBase
    {

        /// <summary>
        /// 返回SAP中所有物料主数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<MaterialNumber>> GetAllSapMaterials()
        {
         

            //Data Hub 查询命令
            string SqlCommand = "select MaterialNo,MaterialName,Format," +
                "UnitCode,SafetyInventoryLowerLimit,SafetyInventoryUpperLimit from item";

            List<MaterialNumber> itemList = DataHubHelper.GetDataFromDataHub<MaterialNumber>(DataHubHelper.SAPBaseUrl,
                DataHubHelper.SAPApiUrl, DataHubHelper.SAPSubSystemName, SqlCommand);



            return itemList;
        }

        /// <summary>
        /// 返回SAP中所有物料主数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductionOrder>> GetSapProductionOrder()
        {


            //Data Hub 查询命令
            string SqlCommand = "select MaterialNo,MaterialName,Format," +
                "UnitCode,MaterialType,SafetyInventoryLowerLimit,SafetyInventoryUpperLimit from item";

            List<ProductionOrder> itemList = DataHubHelper.GetDataFromDataHub<ProductionOrder>(DataHubHelper.SAPBaseUrl,
                DataHubHelper.SAPApiUrl, DataHubHelper.SAPSubSystemName, SqlCommand);

            return itemList;
        }

        /// <summary>
        /// 用户登录业务
        /// 输入项 用户编号、用户密码
        ///返回值如下：1-"用户不能为空"，2-"密码不能为空"，3-"用户不存在"，4-"密码错误"，5-"登录成功"，6-"登录失败")
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public int UpLoadProductionOrderFromMesToSap(dynamic json)
        {
            string userCode = json.userCode;
            string password=json.password;

            SystemBusiness _UserInfoBusiness = new SystemBusiness();

            LoginStatus status = _UserInfoBusiness.Login(userCode, password);
            return (int)status;
        }
 
    }
}
