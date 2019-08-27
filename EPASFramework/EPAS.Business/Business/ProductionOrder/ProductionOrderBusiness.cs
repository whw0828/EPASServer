using EPAS.Business.BaseData;
using EPAS.Component.Calendar;
using EPAS.Component.Utilities;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Entity.MES;
using EPAS.DataEntity.Enum;
using EPAS.DataEntity.Utilities;
using FPA.BaseEntityDataFac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPA.Business.ProductionOrder
{
   public class ProductionOrderBusiness
    {
        #region 工序工单查询

        /// <summary>
        /// 返回可以排日计划的工序工单（大计划）
        /// </summary>
        /// <returns></returns>
        public static List<DayClassWorkPlan> GetNotFinishedWorkPlan()
        {
            List<ClassWorkPlan> cwpList = new List<ClassWorkPlan>();
            int tst = (int)TaskSheetType.PS;//只获取工序工单
            int stop = (int)TaskSheetOPStatus.Stop;
            int finish = (int)TaskSheetOPStatus.Finish;
            cwpList =BaseEntityFac.GetEntityByField<ClassWorkPlan>
                (x => x.TaskSheetOPStatus!= finish  && x.TaskSheetOPStatus != stop && x.TaskSheetType == tst  && (x.ClassPlanQty-x.ClassRealQty)>0);

            List<DayClassWorkPlan> dayList = new List<DayClassWorkPlan>();

            dayList = ObjectHelper.ObjectListMap<ClassWorkPlan, DayClassWorkPlan>(cwpList);

            DateTime dt = BaseEntityFac.GetServerTime();
            DateTime dtFirtDay = dt.Date;
            DateTime dtSecondDay = dt.Date.AddDays(1);
            DateTime dtThirdDay = dt.Date.AddDays(2);

            foreach (var item in dayList)
            {
                //计算大计划的欠数
                item.OweQty = item.ClassPlanQty - item.ClassRealQty;
                if(item.OweQty<0)
                {
                    item.OweQty = 0;//订单超量报工
                }
                //计算日合计量
                item.FirstDay = dtFirtDay;
                item.FirstQty = GetTotalDayPlanQty(item.WorkPlanNo, dtFirtDay);
                item.SecondDay = dtSecondDay;
                item.SecondQty = GetTotalDayPlanQty(item.WorkPlanNo, dtSecondDay);
                item.ThirdDay = dtThirdDay;
                item.ThirdQty = GetTotalDayPlanQty(item.WorkPlanNo, dtThirdDay);


            }
                return dayList;
        }


        /// <summary>
        /// 返回工单的进度信息
        /// </summary>
        /// <param name="ProductionOrderNumbe">生产订单编号</param>
        /// <param name="ProductionVersionName">生产版本名称</param>
        /// <param name="TaskSheetType">计划类型</param>
        /// <returns></returns>
        public static List<ScheduleClassWorkPlan> GetScheduleClassWorkPlan(string ProductionOrderNumbe,string ProductionVersionName,int TaskSheetType)
        {
            List<ClassWorkPlan> cwpList = new List<ClassWorkPlan>();
            //int tst = (int)TaskSheetType.PS;//只获取工序工单
           
            cwpList = BaseEntityFac.GetEntityByField<ClassWorkPlan>
                (x => x.TaskSheetType == TaskSheetType);

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


        public static decimal GetTotalDayPlanQty(string WorkPlanNo,DateTime dt)
        {
            decimal qty = 0;
            List<ClassWorkPlan> tempDayList = BaseEntityFac.GetEntityByField<ClassWorkPlan>(x => x.FatherWorkPlanNo == WorkPlanNo
            && x.PlanWorkDate==dt
            );

            if(tempDayList!=null && tempDayList.Count>0)
            {
                qty = tempDayList.Sum(x => x.ClassPlanQty);
            }

            return qty;
        }

        public static decimal GetTotalDayRealQty(string WorkPlanNo, DateTime dt)
        {
            decimal qty = 0;
            List<ClassWorkPlan> tempDayList = BaseEntityFac.GetEntityByField<ClassWorkPlan>(x => x.FatherWorkPlanNo == WorkPlanNo
            && x.PlanWorkDate == dt
            );

            if (tempDayList != null && tempDayList.Count > 0)
            {
                qty = tempDayList.Sum(x => x.ClassRealQty);
            }

            return qty;
        }
        #endregion

        #region 订单拆分
        /// <summary>
        /// 根据生产版本 将生产订单拆分成工序级别的生产工单
        /// </summary>
        /// <param name="itemList">生产订单</param>
        /// <param name="apsType">排产模式</param>
        /// <param name="dtStart">排产开始时间</param>
        /// <returns></returns>
        public static List<ClassWorkPlan> CalculationWorkPlan(List<ProductionOrderVersion> itemList, DateTime dtStart, int apsType = 0)
        {
            List<ClassWorkPlan> cwpList = new List<ClassWorkPlan>();

            ProductionVersionComBusiness pvcb = new ProductionVersionComBusiness();

            foreach(var item in itemList)
            {
                string ProductionVersionId = item.ProductionVersionId;
                List<ProductionMakeWorkOrderView> workList = pvcb.GetProductionMakeWorkOrderView<ProductionMakeWorkOrder>(x =>
                x.ProductionVersionId == ProductionVersionId);

                if(workList!=null && workList.Count>0)
                {
                    cwpList.AddRange(CreateClassWorkPlans(item, workList));
                }

            }
            

            return cwpList;
        }

        /// <summary>
        /// 计算生产在制品库存抵消值
        /// </summary>
        /// <param name="ProductionOrderNumbe"></param>
        /// <param name="WorkOrderOutProductId"></param>
        /// <returns></returns>
       public static decimal InspectRegisterQty(string ProductionOrderNumbe,string WorkOrderOutProductId)
        {
            decimal qty = 0;

            List< RegisterWarehouseAppointment> itemList=BaseEntityFac.GetEntityByField<RegisterWarehouseAppointment>(x => x.ProductionOrderNumbe == ProductionOrderNumbe
          && x.WorkOrderOutProductId == WorkOrderOutProductId);

            if(itemList!=null && itemList.Count>0)
            {
                qty = itemList.Sum(x => x.RegisterQty);
            }

            if(qty<0)
            {
                qty = 0;
            }


            return qty;
        }
        /// <summary>
        /// 计算工序时间
        /// </summary>
        /// <param name="item">生产订单&生产版本</param>
        /// <param name="newworkList">版本工序清单</param>
        /// <param name="woopList">版本工序产出品清单</param>
        /// <param name="LatestStartTime"></param>
        /// <param name="EarliestStartTime"></param>
        /// <returns></returns>
        public static List<ProductionMakeWorkOrderTime>  CalculationProcessTime(ProductionOrderVersion item,List<ProductionMakeWorkOrderView> newworkList,
                 List<WorkOrderOutProduct> woopList,DateTime LatestStartTime, DateTime EarliestStartTime, List<APSCalendar> ascList)
        {
            int min = newworkList.FirstOrDefault().ProcessOrder;//开始工序顺序号
            int max = newworkList.Max(x => x.ProcessOrder);//末尾工序顺序号

            List<ProductionMakeWorkOrderTime> procesTimeList = new List<ProductionMakeWorkOrderTime>();

            //生产版本的工序产出品清单

            foreach (var pmw in newworkList)
            {
                string ProductionMakeWorkOrderId = pmw.ProductionMakeWorkOrderId;
                ProductionMakeWorkOrderTime procesTime = new ProductionMakeWorkOrderTime();
               
                procesTime.ProductionMakeWorkOrderId = pmw.ProductionMakeWorkOrderId;
              
                if (pmw.ProcessOrder == min)
                {
                    //首工序
                    procesTime.LatestStartTime = LatestStartTime;
                    procesTime.EarliestStartTime = EarliestStartTime;
                }
                else
                {
                  
                    //检查当前工序是否有依赖工序
                    List<ProductionMakeWorkOrderRelation> relationList = BaseEntityFac.GetEntityByField<ProductionMakeWorkOrderRelation>(x => x.ProductionMakeWorkOrderId == ProductionMakeWorkOrderId);

                    if(relationList!=null && relationList.Count>0)
                    {
                        //获取依赖工序的开始时间
                        DateTime tempLatestStartTime=DateTime.Now.AddYears(-1);
                        DateTime tempEarliestStartTime= DateTime.Now.AddYears(-1);
                        foreach (var relation in relationList)
                        {
                            ProductionMakeWorkOrderTime relationTime = procesTimeList.Find(x => x.ProductionMakeWorkOrderId == relation.RProductionMakeWorkOrderId);

                            if (relation.WTR== (int)WorkOrderSequenceType.ES)
                            {
                                decimal spanRelation = relation.ETime * relation.TimeUnit;//秒

                                DateTime tempDt = CalendarHelper.GetAPSWorkTime(relationTime.EarliestEndTime, spanRelation, ascList,false);

                                if(tempEarliestStartTime < tempDt)
                                {
                                    tempEarliestStartTime = tempDt;
                                }
                                tempDt = CalendarHelper.GetAPSWorkTime(relationTime.LatestEndTime, spanRelation, ascList, false);
                                if (tempLatestStartTime < tempDt)
                                {
                                    tempLatestStartTime = tempDt;
                                }
                            }
                            else if (relation.WTR == (int)WorkOrderSequenceType.SS || relation.WTR == (int)WorkOrderSequenceType.SSEE)
                            {
                                decimal spanRelation = relation.STime * relation.TimeUnit;//秒
                                DateTime tempDt = CalendarHelper.GetAPSWorkTime(relationTime.EarliestStartTime, spanRelation, ascList, false);
                                if (tempEarliestStartTime < tempDt)
                                {
                                    tempEarliestStartTime = tempDt;
                                }
                                tempDt = CalendarHelper.GetAPSWorkTime(relationTime.LatestStartTime, spanRelation, ascList, false);
                                if (tempLatestStartTime < tempDt)
                                {
                                    tempLatestStartTime = tempDt;
                                }
                            }
                            //whw 2019年8月9号  暂不支持SSEE的接序方式

                            procesTime.LatestStartTime = tempLatestStartTime;
                            procesTime.EarliestStartTime = tempEarliestStartTime;

                        }

                       

                    }

                }

                #region 计算工序产出品最终BOM构成数量
                List<WorkOrderOutProduct> tempwoopList = woopList.FindAll(x => x.ProductionMakeWorkOrderId == ProductionMakeWorkOrderId);
                if(tempwoopList==null)
                {
                    continue;
                }
                decimal maxBomQty = 0;
                decimal Qty = 0;
                foreach (var tempItem in tempwoopList)
                {
               
                    //去掉库存现有货物抵消值
                   Qty = item.MakeQty * tempItem.Qty- InspectRegisterQty(item.ProductionOrderNumbe, tempItem.WorkOrderOutProductId);//工序需要生产的数量（工序多个产出品时，该值为最大值）

                    if (maxBomQty < Qty)
                    {
                        maxBomQty = Qty;
                    }
                }
               

                #endregion

                decimal spanTime = (pmw.FrontTimeValue * pmw.FrontTimeUnit   //前设置时间
                    + (Qty/pmw.StandardMakeValue)*pmw.StandardMakeTimeValue*pmw.StandardMakeTimeUnit //制造时间
                    +pmw.BackTimeValue*pmw.BackTimeUnit); //后设置时间

                procesTime.spanTime = (Double)spanTime;

                procesTime.LatestEndTime = CalendarHelper.GetAPSWorkTime(procesTime.LatestStartTime, spanTime, ascList, false);
                procesTime.EarliestEndTime = CalendarHelper.GetAPSWorkTime(procesTime.EarliestStartTime, spanTime, ascList, false);


                #region 去掉非作业时间  时间会变长

                

                #endregion


                procesTimeList.Add(procesTime);
                //计算结束时间

                

            }

            return procesTimeList;

        }

        /// <summary>
        /// 获取工厂的年生产日历
        /// </summary>
        /// <returns></returns>
        public static List<APSCalendar> GetAPSCalendarWorkDate(string ResourceCode)
        {
            #region 获取作业时间
            List<APSCalendar> workDate = new List<APSCalendar>();
            DateTime current = BaseEntityFac.GetServerTime();
            int CalendarYear = current.Year;
            //string ResourceCode = ;//*

            string mode = "";//工厂的出勤模式
            //今年
            string WorkWeekValue = string.Empty;
            List<ProductionResourceCalendar> prcItemList = BaseEntityFac.GetEntityByField<ProductionResourceCalendar>
                (x => x.CalendarYear == CalendarYear && x.ResourceCode == ResourceCode);
            if (prcItemList != null && prcItemList.Count > 0)
            {
                ProductionResourceCalendar prc = prcItemList.FirstOrDefault();
                WorkWeekValue = prc.WorkWeekValue;
                mode = prc.Mode;
                List<APSCalendar> tempworkDate = CalendarHelper.CalendarTransformWorkDate(current.Year, prc.WorkWeekValue, prc.Mode, prc.NotWorkDates, prc.Overtime);

                if (tempworkDate != null)
                {
                    workDate.AddRange(tempworkDate);
                }
            }
            //明年
            CalendarYear += 1;
            List<ProductionResourceCalendar> prcNextItemList = BaseEntityFac.GetEntityByField<ProductionResourceCalendar>
             (x => x.CalendarYear == CalendarYear && x.ResourceCode == ResourceCode);
            if (prcNextItemList != null && prcNextItemList.Count > 0)
            {
                ProductionResourceCalendar prc = prcNextItemList.FirstOrDefault();
                List<APSCalendar> tempworkDate = CalendarHelper.CalendarTransformWorkDate(CalendarYear, prc.WorkWeekValue, prc.Mode, prc.NotWorkDates, prc.Overtime);

                if (tempworkDate != null)
                {
                    workDate.AddRange(tempworkDate);
                }
            }
            else
            {
                //未定义明年生产日历，参照今年的生产日历（无法估计节假日和加班日期）
                List<APSCalendar> tempworkDate = CalendarHelper.CalendarTransformWorkDate(CalendarYear, WorkWeekValue, mode);

                if (tempworkDate != null)
                {
                    workDate.AddRange(tempworkDate);
                }
            }

            // CalendarHelper.CalendarTransformWorkDate
            #endregion

            return workDate;
        }

        public static List<ClassWorkPlan> CreateClassWorkPlans(ProductionOrderVersion item,List<ProductionMakeWorkOrderView> workList)
        {
            List<ClassWorkPlan> cwpList = new List<ClassWorkPlan>();

            string ProductionVersionId = item.ProductionVersionId;

            DateTime dt = BaseEntityFac.GetServerTime();
            //工序时间缓存
            List<ProductionMakeWorkOrderTime> procesTimeList = new List<ProductionMakeWorkOrderTime>();

            //获取该生产版本中工序在制品清单
            List<WorkOrderOutProduct> woopList = BaseEntityFac.GetEntityByField<WorkOrderOutProduct>(x => x.ProductionVersionId == ProductionVersionId);

            //跟工序作业顺序计算  升序
            List<ProductionMakeWorkOrderView> newworkList = workList.OrderBy(x => x.ProcessOrder).ToList();
        
            //公式 (订单量/产能)* (时间值)*(时间单位换算成的秒数)
            decimal totalWorkTime = (item.MakeQty / item.ProductMakeValue) * item.ProductMakeTimeValue * item.ProductMakeUnit;//作业时间 单位秒

            //最晚作业开始时间


            List<APSCalendar> ascList= GetAPSCalendarWorkDate(CalendarHelper.ALLResourceCode.ToString());

            DateTime LatestStartTime = CalendarHelper.GetAPSWorkTime(item.ProductionOrderEndDate, totalWorkTime, ascList);

            //获取工序时间(去掉非作业时间)
            List<ProductionMakeWorkOrderTime> tempprocesTimeList = CalculationProcessTime(item, newworkList,
                woopList, LatestStartTime, dt, ascList);

            if(tempprocesTimeList==null)
            {
                return null;
            }

            foreach (var pmw in newworkList)
            {
                //获取工序产出品 可能存在一个工序多个产出品的情况
                List<WorkOrderOutProduct> newwoopList = woopList.FindAll(x => x.WorkOrderNum == pmw.WorkOrderNum
                 && x.ProductionVersionId == pmw.ProductionVersionId);

                if(newwoopList!=null && newwoopList.Count>0)
                {
                    ProductionMakeWorkOrderTime processTime = tempprocesTimeList.Find(x=>
                    x.ProductionMakeWorkOrderId== pmw.ProductionMakeWorkOrderId);
                    foreach (var woopItem in newwoopList)
                    {
                        ClassWorkPlan cwp = new ClassWorkPlan();
                        cwp.ClassWorkPlanId = Guid.NewGuid().ToString();
                        cwp.ProductionOrderNumbe = item.ProductionOrderNumbe;//生产订单编号
                        cwp.ProductionOrderVersionNumber = item.ProductionOrderVersionNumber;//订单工艺编号
                        cwp.CustomerNum = item.CustomerNum;
                        cwp.CustomerName = item.CustomerName;
                        cwp.WorkPlanNo = dt.Year.ToString() + BaseEntityFac.GetNewSerialNumber(EE.Name<ClassWorkPlan>(x => x.WorkPlanNo));//工单流水号
                        cwp.TaskSheetType = (int)TaskSheetType.PS;//工单大分类
                        cwp.FatherWorkPlanNo = string.Empty;//父级工单流水号
                        cwp.ProductionVersionId = pmw.ProductionVersionId;//生产版本ID
                        cwp.ProductionVersionName = pmw.ProductionVersionName;//生产版本名称
                        cwp.ProductionMakeWorkOrderId = pmw.ProductionMakeWorkOrderId;//生产版本中工序ID
                        cwp.WorkOrderOutProductId = woopItem.WorkOrderOutProductId;//工序在制品ID
                        cwp.WorkOrderPositionId = string.Empty;//工序中工位ID
                        cwp.WorkOrderNum = pmw.WorkOrderNum;//工序编码
                        cwp.WorkOrderName = pmw.WorkOrderName;//工序名称
                        cwp.MaterialNum = woopItem.MaterialNum;//在制品编码
                        cwp.MaterialName = woopItem.MaterialName;//在制品名称
                        cwp.BatchNumber = item.BatchNumber;//生产批次

                        cwp.MainProductionResourceCode = string.Empty;//主资源编码
                        cwp.MainProductionResourceName = string.Empty;//主资源名称
                        cwp.SubProductionResourceCode = string.Empty;//副资源编码
                        cwp.SubProductionResourceName = string.Empty;//副资源名称

                        cwp.WorkPositionNum = string.Empty;//工位编码
                        cwp.WorkPositionName = string.Empty;//工位名称
                        cwp.EmployeeNum = string.Empty;//员工工号
                        cwp.PlanWorkDate = dt;          //计划日期
                        cwp.ClassNoName = string.Empty;//班次
                        cwp.EarliestStartTime = processTime.EarliestStartTime;
                        cwp.LatestEndTime = processTime.LatestEndTime;
                        cwp.PlanWorkStartTime = cwp.EarliestStartTime;
                        cwp.PlanWorkEndTime = cwp.LatestEndTime;
                        cwp.ClassPlanQty = item.MakeQty * woopItem.Qty- InspectRegisterQty(item.ProductionOrderNumbe, woopItem.WorkOrderOutProductId);//工单计划作业量 去掉库存抵消量
                        cwp.ClassRealQty = 0;//工单实际作业量
                        cwp.ClassSubmitScrapQty = 0;//报工后出现的废品
                        cwp.ClassNoSubmitScrapQty = 0;//调试出现的废品
                        cwp.UnitCode = item.UnitCode;//物料单位

                        cwp.TaskSheetSmallType = (int)TaskSheetSmallType.Normal;//工单小分类
                        cwp.IsPublish = (int)WhetherValue.No;
                        cwp.CustomFlag = 0;//1-表示插单
                        cwp.TaskSheetOPStatus = (int)TaskSheetOPStatus.NotStart;//工单操作
                        cwp.HandoverState= (int)WhetherValue.No;//交接班

                        cwp.QCProcessType = woopItem.QCProcessType;//质检类型 例如全检 抽检 免检
                        cwp.QCProcessFlag = (int)QCProcessFlag.Untested;//质检状态
                        cwp.QCEmployeeName = string.Empty;//质检员姓名
                        cwp.QCEmployeeNum = string.Empty;//质检员工号

                        cwp.Comments = string.Empty;//工单备注

                        cwpList.Add(cwp);
                    }

                   
                }
            }


            return cwpList;
        }

        #endregion

        #region 创建工序的日生产计划

        private static ClassWorkPlan CopyDayWorkPlan(DayClassWorkPlan item,DateTime PlanWorkDate,decimal ClassPlanQty, string CEmployeeNum, string CEmployeeName)
        {
            #region  检查日计划是否存在
            string ProductionMakeWorkOrderId = item.ProductionMakeWorkOrderId;
            string ProductionVersionName = item.ProductionVersionName;
            string MaterialNum = item.MaterialNum;
            DateTime dt = PlanWorkDate;
            int tst= (int)TaskSheetType.DS;//工序日生产计划

            List<ClassWorkPlan> cwpLatest = BaseEntityFac.GetEntityByField<ClassWorkPlan>(x =>
            x.PlanWorkDate==dt && x.TaskSheetType == tst &&
            x.ProductionMakeWorkOrderId== ProductionMakeWorkOrderId &&
            x.ProductionVersionName== ProductionVersionName 
            );

            if(cwpLatest!=null && cwpLatest.Count>0)
            {
                List<ClassWorkPlan> cwptempLatest = cwpLatest.OrderByDescending(x => x.CreateTime).ToList();

                ClassWorkPlan cwpItem = cwptempLatest.FirstOrDefault();

                cwpItem.ClassPlanQty = ClassPlanQty;//日作业量

                return cwpItem;
            }

            #endregion
            ClassWorkPlan cwp = ObjectHelper.ObjectMap<DayClassWorkPlan, ClassWorkPlan>(item);
            cwp.PlanWorkDate = PlanWorkDate;
            cwp.ClassWorkPlanId = Guid.NewGuid().ToString();//作业日期
            cwp.TaskSheetType = (int)TaskSheetType.DS;//工序日生产计划
            cwp.TaskSheetOPStatus = (int)TaskSheetOPStatus.NotStart;//未开始作业
            cwp.ClassPlanQty = ClassPlanQty;//日作业量
            cwp.ClassRealQty = 0;//实际日报工量
            cwp.ClassNoSubmitScrapQty = 0;//未报工报废
            cwp.ClassSubmitScrapQty = 0;//已报工报废
            cwp.CEmployeeName = CEmployeeName;//创建人姓名
            cwp.CEmployeeNum = CEmployeeNum;//创建人工号
            cwp.CreateTime = BaseEntityFac.GetServerTime();
            cwp.FatherWorkPlanNo = item.WorkPlanNo;
            cwp.WorkPlanNo = item.WorkPlanNo + BaseEntityFac.GetNewSerialNumber("SubWorkPlanNo");//父工单号+流水号
            cwp.QCProcessFlag = (int)QCProcessFlag.Untested;
            cwp.QCProcessType = item.QCProcessType;//复用父级工单
            cwp.QCEmployeeName = string.Empty;//质检员姓名
            cwp.QCEmployeeNum = string.Empty;//质检员工号
            return cwp;
        }

        /// <summary>
        /// 创建工序计划的日生产计划（已经存在 则更新）
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="CEmployeeNum"></param>
        /// <param name="CEmployeeName"></param>
        /// <returns></returns>
        public static bool CreateDayWorkPlan(List<DayClassWorkPlan> itemList,string CEmployeeNum,string CEmployeeName)
        {
            bool result = false;

            List<ClassWorkPlan> cwpList = new List<ClassWorkPlan>();
            if (itemList != null && itemList.Count > 0)
            {
                foreach (var item in itemList)
                {
                    //第一天
                    if(item.FirstQty>0)
                    {
                        ClassWorkPlan cwp=CopyDayWorkPlan(item,item.FirstDay,item.FirstQty, CEmployeeNum, CEmployeeName);

                        cwpList.Add(cwp);
                    }

                    //第二天
                    if (item.SecondQty > 0)
                    {
                        ClassWorkPlan cwp = CopyDayWorkPlan(item, item.SecondDay, item.SecondQty, CEmployeeNum, CEmployeeName);

                        cwpList.Add(cwp);
                    }

                    //第三天
                    if (item.ThirdQty > 0)
                    {
                        ClassWorkPlan cwp = CopyDayWorkPlan(item, item.ThirdDay, item.ThirdQty, CEmployeeNum, CEmployeeName);

                        cwpList.Add(cwp);
                    }

                }

                result=BaseEntityFac.SaveBaseData<ClassWorkPlan>(cwpList);

            }
            return result;
        }

        /// <summary>
        /// 插入新工序计划到数据库
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="CEmployeeNum"></param>
        /// <param name="CEmployeeName"></param>
        /// <returns></returns>
        public static bool InsertProcessWorkPlan(List<ClassWorkPlan> itemList, string CEmployeeNum, string CEmployeeName)
        {
            bool result = false;

            if (itemList != null && itemList.Count > 0)
            {
                List<ProductionOrderVersion> povList = new List<ProductionOrderVersion>();
                //遍历更新
                itemList.ForEach(x =>
                {
                    x.CEmployeeName = CEmployeeName;
                    x.CEmployeeNum = CEmployeeNum;
                    x.CreateTime = BaseEntityFac.GetServerTime();

                    ProductionOrderVersion pov = povList.Find(y => y.ProductionOrderVersionNumber == x.ProductionOrderVersionNumber);

                    if(pov==null)
                    {
                        string ProductionOrderVersionNumber = x.ProductionOrderVersionNumber;
                        List<ProductionOrderVersion> tempList = BaseEntityFac.GetEntityByField<ProductionOrderVersion>
                        (m => m.ProductionOrderVersionNumber== ProductionOrderVersionNumber);

                        if(tempList!=null && tempList.Count>0)
                        {
                            povList.AddRange(tempList);
                        }
                    }

                });

                povList.ForEach(x =>
                {
                    x.IsAPS = (int)WhetherValue.Yes;
                });

              return  BaseEntityFac.TransactionOPEntitys<ClassWorkPlan>((cn, transaction) =>
                {
                    bool res = false;
                    res = BaseEntityFac.TransactionOPEntitysAdd<ClassWorkPlan>(cn, transaction, EOPType.Insert, itemList);

                    res = BaseEntityFac.TransactionOPEntitysAdd<ProductionOrderVersion>(cn, transaction, EOPType.Update, povList);

                    return res;
                });

            }

            return result;

        }
        #endregion
    }
}
