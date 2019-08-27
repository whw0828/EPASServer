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
using EPAS.Business.BaseData;

namespace EPAS.API.Controllers
{

    /// <summary>
    /// 生产版本业务操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductionVersionComBusinessController : ControllerBase
    {

        /// <summary>
        /// 返回生产版本中包含的工序；自定义 where条件 返回ProductionMakeWorkOrderView
        /// </summary>
        /// <param name="cmd">请求命令</param>
        /// <returns></returns>
        [HttpPost]
        public object GetProductionMakeWorkOrderView(OPEntityCmdBase cmd)
        {
            ProductionVersionComBusiness _BaseBusinessObject = new ProductionVersionComBusiness();

            object itemList = _BaseBusinessObject.GetProductionMakeWorkOrderView(cmd.Tag);

            return itemList;
        }


    }
}
