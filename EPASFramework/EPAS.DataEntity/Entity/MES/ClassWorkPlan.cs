using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3606
    /// Description:生产工单
   
   
   
   
   
    /// </summary>
    public class ClassWorkPlan:BaseEntity
    {
        public ClassWorkPlan()
        {
        }

        /// <summary>
        /// 表名: "ClassWorkPlan"
        /// </summary>

        /// <summary>
        /// ClassWorkPlanId
        /// </summary>
        [DisplayName("生产工单ID")]
        public String ClassWorkPlanId
        {
            get ;
            set ;
        }

        /// <summary>
        /// 生产订单编号
        /// </summary>
        [DisplayName("ProductionOrderNumbe")]
        public String ProductionOrderNumbe
        {
            get;
            set;
        }

        /// <summary>
        /// 生产订单工艺编号
        /// </summary>
        [DisplayName("ProductionOrderVersionNumber")]
        public String ProductionOrderVersionNumber
        {
            get;
            set;
        }

        

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerNum")]
        public String CustomerNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerName")]
        public String CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 父工单编号
        /// </summary>
        [DisplayName("FatherWorkPlanNo")]
        public String FatherWorkPlanNo
        {
            get;
            set;
        }
        /// <summary>
        /// 工单编号
        /// </summary>
        [DisplayName("WorkPlanNo")]
        public String WorkPlanNo
        {
            get ;
            set ;
        }

        /// <summary>
        /// 工单类型 （1-工序工单  2-设备工单  3-人员工单)
        /// </summary>
        [DisplayName("TaskSheetType")]
        public int TaskSheetType
        {
            get;
            set;
        }

        /// <summary>
        /// 版本ID
        /// </summary>
        [DisplayName("ProductionVersionId")]
        public String ProductionVersionId
        {
            get ;
            set ;
        }
        

        /// <summary>
        /// 版本名称
        /// </summary>
        [DisplayName("ProductionVersionName")]
        public String ProductionVersionName
        {
            get;
            set;
        }

        /// <summary>
        /// 版本工序ID
        /// </summary>
        [DisplayName("ProductionMakeWorkOrderId")]
        public String ProductionMakeWorkOrderId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 工序产出品ID
        /// </summary>
        [DisplayName("WorkOrderOutProductId")]
        public String WorkOrderOutProductId
        {
            get ;
            set ;
        }


        /// <summary>
        /// 工序编码
        /// </summary>
        [DisplayName("WorkOrderNum")]
        public String WorkOrderNum
        {
            get;
            set;
        }
        /// <summary>
        /// 工序名称
        /// </summary>
        [DisplayName("WorkOrderName")]
        public String WorkOrderName
        {
            get;
            set;
        }

        /// <summary>
        /// 在制品物料编码
        /// </summary>
        [DisplayName("MaterialNum")]
        public String MaterialNum
        {
            get;
            set;
        }
        /// <summary>
        /// 在制品物料名称
        /// </summary>
        [DisplayName("MaterialName")]
        public String MaterialName
        {
            get;
            set;
        }

        /// <summary>
        /// 主生产设备编码
        /// </summary>
        [DisplayName("MainProductionResourceCode")]
        public String MainProductionResourceCode
        {
            get;
            set;
        }
        /// <summary>
        /// 主生产设备名称
        /// </summary>
        [DisplayName("MainProductionResourceName")]
        public String MainProductionResourceName
        {
            get;
            set;
        }

        /// <summary>
        /// 工装/模具 编码
        /// </summary>
        [DisplayName("SubProductionResourceCode")]
        public String SubProductionResourceCode
        {
            get;
            set;
        }
        /// <summary>
        /// 工装/模具 名称
        /// </summary>
        [DisplayName("SubProductionResourceName")]
        public String SubProductionResourceName
        {
            get;
            set;
        }


        /// <summary>
        /// 工序工位ID
        /// </summary>
        [DisplayName("WorkOrderPositionId")]
        public String WorkOrderPositionId
        {
            get;
            set;
        }

        /// <summary>
        /// 工位编码
        /// </summary>
        [DisplayName("WorkPositionNum")]
        public String WorkPositionNum
        {
            get;
            set;
        }
        /// <summary>
        /// 工位名称
        /// </summary>
        [DisplayName("WorkPositionName")]
        public String WorkPositionName
        {
            get;
            set;
        }

        /// <summary>
        /// 作业员工工号
        /// </summary>
        [DisplayName("EmployeeNum")]
        public String EmployeeNum
        {
            get;
            set;
        }

        /// <summary>
        /// 生产批次
        /// </summary>
        [DisplayName("BatchNumber")]
        public String BatchNumber
        {
            get ;
            set ;
        }
        /// <summary>
        /// 计划日期
        /// </summary>
        [DisplayName("PlanWorkDate")]
        public DateTime PlanWorkDate
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassNoName")]
        public String ClassNoName
        {
            get ;
            set ;
        }
      
        /// <summary>
        /// 工单作业计划数量
        /// </summary>
        [DisplayName("ClassPlanQty")]
        public Decimal ClassPlanQty
        {
            get ;
            set ;
        }

        /// <summary>
        ///产品单位
        /// </summary>
        [DisplayName("单位")]
        public string UnitCode
        {
            get;
            set;
        }

        /// <summary>
        /// 最早开始时间
        /// </summary>
        [DisplayName("EarliestStartTime")]
        public DateTime EarliestStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最晚结束时间
        /// </summary>
        [DisplayName("LatestEndTime")]
        public DateTime LatestEndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        [DisplayName("PlanWorkStartTime")]
        public DateTime PlanWorkStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 计划结束时间
        /// </summary>
        [DisplayName("PlanWorkEndTime")]
        public DateTime PlanWorkEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 实际完成量
        /// </summary>
        [DisplayName("ClassRealQty")]
        public Decimal ClassRealQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassSubmitScrapQty")]
        public Decimal ClassSubmitScrapQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassNoSubmitScrapQty")]
        public Decimal ClassNoSubmitScrapQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TaskSheetSmallType")]
        public int TaskSheetSmallType
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("IsPublish")]
        public int IsPublish
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TaskSheetOPStatus")]
        public int TaskSheetOPStatus
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomFlag")]
        public int CustomFlag
        {
            get ;
            set ;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("HandoverState")]
        public int HandoverState
        {
            get ;
            set ;
        }
        /// <summary>
        /// 质检处理
        /// </summary>
        [DisplayName("QCProcessFlag")]
        public int QCProcessFlag
        {
            get ;
            set ;
        }

        /// <summary>
        /// 质检类型
        /// </summary>
        [DisplayName("QCProcessType")]
        public int QCProcessType
        {
            get;
            set;
        }
        /// <summary>
        /// 质检员姓名
        /// </summary>
        [DisplayName("QCEmployeeName")]
        public String QCEmployeeName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 质检员工号
        /// </summary>
        [DisplayName("QCEmployeeNum")]
        public String QCEmployeeNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCTime")]
        public DateTime? QCTime
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Comments")]
        public String Comments
        {
            get ;
            set ;
        }


    }
}
