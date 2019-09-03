using EPAS.Component.Utilities;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Entity.MES;
using EPAS.DataEntity.Entity.Temp;
using FPA.BaseEntityDataFac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPA.Business.APS
{
    /// <summary>
    /// APS排程业务
    /// </summary>
   public  class APSBusiness
    {
        /// <summary>
        /// 根据日期获取排程计划
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <param name="TaskSheetType"></param>
        /// <returns></returns>
        public static List<ScheduleClassWorkPlan> GetScheduleClassWorkPlan(DateTime dtStart, DateTime dtEnd, int TaskSheetType)
        {
            List<ClassWorkPlan> cwpList = new List<ClassWorkPlan>();
            //int tst = (int)TaskSheetType.PS;//只获取工序工单

            cwpList = BaseEntityFac.GetEntityByField<ClassWorkPlan>
                (x =>  x.PlanWorkDate >= dtStart
            && x.PlanWorkDate <= dtEnd &&
                x.TaskSheetType == TaskSheetType);

            List<ScheduleClassWorkPlan> scpList = new List<ScheduleClassWorkPlan>();

            scpList = ObjectHelper.ObjectListMap<ClassWorkPlan, ScheduleClassWorkPlan>(cwpList);

            foreach (var item in scpList)
            {
                //计算大计划的欠数
                item.OweQty = item.ClassPlanQty - item.ClassRealQty;
                if (item.OweQty < 0)
                {
                    item.OweQty = 0;//订单超量报工
                }
            }
            return scpList;
        }
        /// <summary>
        /// 返回工序日计划可以使用的设备信息
        /// </summary>
        /// <param name="ProductionMakeWorkOrderId"></param>
        /// <param name="ProductionVersionId"></param>
        /// <param name="PlanQty"></param>
        /// <returns></returns>
        public static List<PlanWorkOrderProductionResource> GetNewPlanWorkOrderProductionResource(string WorkPlanNo)
        {
            ClassWorkPlan item=BaseEntityFac.GetSingleEntityByField<ClassWorkPlan>(x => x.WorkPlanNo == WorkPlanNo);
            if(item==null)
            {
                return null;
            }
            string ProductionMakeWorkOrderId = item.ProductionMakeWorkOrderId;
            string ProductionVersionId = item.ProductionVersionId;

            decimal PlanQty = item.ClassPlanQty-item.ClassRealQty;//分配欠数

            if(PlanQty<0)
            {
                PlanQty = 0;//报工数量已经大于计划数量。注意根据实际业务调整 2019.9.2
            }

            List<PlanWorkOrderProductionResource> itemList = new List<PlanWorkOrderProductionResource>();

            List<WorkOrderProductionResource> itemSourceList=BaseEntityFac.GetEntityByField<WorkOrderProductionResource>(x=>x.ProductionMakeWorkOrderId== ProductionMakeWorkOrderId 
            && x.ProductionVersionId== ProductionVersionId);

            itemList = ObjectHelper.ObjectListMap<WorkOrderProductionResource, PlanWorkOrderProductionResource>(itemSourceList);

            //工序只有一台设备的情况
            if(itemList!=null && itemList.Count==1)
            {
                itemList.ForEach(x => {
                    x.Qty = PlanQty;
                });
            }

            return itemList;
        }

        /// <summary>
        /// 删除未使用的生产计划，如果该计划对应生产料单，则作废料单。
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public static  bool DeleteNotUsedPlan(List<ClassWorkPlan> itemList, string CEmployeeNum, string CEmployeeName)
        {
            bool result = false;
            List<ClassReceiveMaterialPlan> crmpList = new List<ClassReceiveMaterialPlan>();
            foreach(var item in itemList)
            {
                string ClassWorkPlanId = item.ClassWorkPlanId;
                List<ClassReceiveMaterialPlan> crmpTempList = BaseEntityFac.GetEntityByField<ClassReceiveMaterialPlan>
                    (x=>x.ClassWorkPlanId== ClassWorkPlanId);

                if(crmpTempList!=null && crmpTempList.Count>0)
                {
                    //去掉重复料单记录
                    crmpTempList.ForEach(x =>
                    {
                        ClassReceiveMaterialPlan temp = crmpList.Find(y => y.ClassReceiveMaterialPlanId == x.ClassReceiveMaterialPlanId);

                        if(temp==null)
                        {
                            crmpList.Add(temp);
                        }
                    });
                }
            }

            BaseEntityFac.TransactionOPEntitys<ClassWorkPlan>((cn, transaction) =>
            {

                result = BaseEntityFac.TransactionOPEntitysAdd<ClassWorkPlan>(cn, transaction, EOPType.Insert, itemList);

                // res = BaseEntityFac.TransactionOPEntitysAdd<ProductionOrderVersion>(cn, transaction, EOPType.Update, povList);

                return result;
            });


            return result;
        }
    }
}
