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
using FPA.BaseEntityDataFac;
using EPAS.DataEntity.Entity.Temp;

namespace EPAS.API.Controllers
{

    /// <summary>
    /// 生产排程业务
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APSController : ControllerBase
    {

        /// <summary>
        /// 返回生产计划的进度信息
        /// </summary>
        /// <param name="dtStart">生产订单编号</param>
        /// <param name="dtEnd">生产版本名称</param>
        /// <param name="TaskSheetType">计划类型</param>
        /// <returns></returns>
        [HttpPost]
        public List<ScheduleClassWorkPlan> GetScheduleClassWorkPlan(DateTime dtStart, DateTime dtEnd, int TaskSheetType)
        {
            return FPA.Business.APS.APSBusiness.GetScheduleClassWorkPlan(dtStart, dtEnd, TaskSheetType);
        }

        /// <summary>
        /// 返回工序日计划可以使用的设备信息
        /// </summary>
        /// <param name="WorkPlanNo">日计划编号</param>
        /// <returns></returns>
        [HttpPost]
        public  List<PlanWorkOrderProductionResource> GetNewPlanWorkOrderProductionResource(string WorkPlanNo)
        {
            return FPA.Business.APS.APSBusiness.GetNewPlanWorkOrderProductionResource(WorkPlanNo);
        }




    }
}
;