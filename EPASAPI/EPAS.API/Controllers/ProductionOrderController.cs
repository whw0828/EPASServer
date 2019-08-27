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

namespace EPAS.API.Controllers
{

    /// <summary>
    /// 生产版本业务操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductionOrderController : ControllerBase
    {

        /// <summary>
        /// 返回生产可用订单
        /// </summary>
        /// <param name="offsetday">向前可以查询订单的天数 默认30天</param>
        /// <returns></returns>
        [HttpPost]
        public object GetLatestProductionOrder(int offsetday=30)
        {
            int orderStatus = (int)ProductionOrderStatus.Release;

            DateTime dt = BaseEntityFac.GetServerTime().AddDays(0-offsetday);

            object itemList = BaseEntityFac.GetEntityByField<ProductionOrder>(x => x.ProductionOrderStatus == orderStatus && x.ProductionOrderEndDate>=dt);

            return itemList;
        }

        /// <summary>
        /// 返回生产订单工序中在制品预定使用库存合计值
        /// </summary>
        /// <param name="ProductionOrderNumbe">生产订单编号</param>
        /// <param name="WorkOrderOutProductId">工序产出品ID</param>
        /// <returns></returns>
        [HttpPost]
        public  decimal InspectRegisterQty(string ProductionOrderNumbe, string WorkOrderOutProductId)
        {
            decimal qty = FPA.Business.ProductionOrder.ProductionOrderBusiness.InspectRegisterQty(ProductionOrderNumbe,
                WorkOrderOutProductId);

            return qty;
        }

        /// <summary>
        /// 返回未完成的工序工单（大计划） 大计划->日计划->设备日计划/人员日计划
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  List<DayClassWorkPlan> GetNotFinishedWorkPlan()
        {

            return FPA.Business.ProductionOrder.ProductionOrderBusiness.GetNotFinishedWorkPlan();
        }

        /// <summary>
        /// 返回工单的进度信息
        /// </summary>
        /// <param name="ProductionOrderNumbe">生产订单编号</param>
        /// <param name="ProductionVersionName">生产版本名称</param>
        /// <param name="TaskSheetType">计划类型</param>
        /// <returns></returns>
        [HttpPost]
        public List<ScheduleClassWorkPlan> GetScheduleClassWorkPlan(string ProductionOrderNumbe, string ProductionVersionName, int TaskSheetType)
        {
            return FPA.Business.ProductionOrder.ProductionOrderBusiness.GetScheduleClassWorkPlan(ProductionOrderNumbe, ProductionVersionName, TaskSheetType);
        }

        /// <summary>
        /// 根据生产版本 将生产订单拆分成工序级别的生产工单
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public object CalculationWorkPlan(OPEntityCmdBase cmd)
        {
           int apsType  = int.Parse(cmd.Tag);

            List<ProductionOrderVersion> itemList = JsonUtil.fromJson<List<ProductionOrderVersion>>(cmd.EntityJson);

            DateTime dt = BaseEntityFac.GetServerTime();
            if (!string.IsNullOrWhiteSpace(cmd.timestamp))
            {
                dt = DateTime.Parse(cmd.timestamp);
            }            
            return FPA.Business.ProductionOrder.ProductionOrderBusiness.CalculationWorkPlan(itemList,dt,apsType);
          
        }

        /// <summary>
        /// 创建工序计划的日生产计划（已经存在 则更新）
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public bool CreateDayWorkPlan(OPEntityCmdBase cmd)
        {
            List<DayClassWorkPlan> itemList = JsonUtil.fromJson<List<DayClassWorkPlan>>(cmd.EntityJson);
            return FPA.Business.ProductionOrder.ProductionOrderBusiness.CreateDayWorkPlan(itemList,cmd.CEmployeeNum,cmd.CEmployeeName);

        }

        /// <summary>
        /// 插入新工序计划到数据库
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost]
        public  bool InsertProcessWorkPlan(OPEntityCmdBase cmd)
        {
            List<ClassWorkPlan> itemList = JsonUtil.fromJson<List<ClassWorkPlan>>(cmd.EntityJson);
            return FPA.Business.ProductionOrder.ProductionOrderBusiness.InsertProcessWorkPlan(itemList, cmd.CEmployeeNum, cmd.CEmployeeName);

        }


    }
}
;