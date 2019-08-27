using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4723
    /// Description:订单使用的生产版本
    /// </summary>
    public class ProductionOrderVersion : BaseEntity
    {
        public ProductionOrderVersion()
        {
        }

        /// <summary>
        /// 表名: "ProductionOrderVersion"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionOrderVersionId")]
        public String ProductionOrderVersionId
        {
            get ;
            set ;
        }

        /// <summary>
        /// 订单生产版本流水号
        /// </summary>
        [DisplayName("ProductionOrderVersionNumber")]
        public String ProductionOrderVersionNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 生产版本ID
        /// </summary>
        [DisplayName("ProductionVersionId")]
        public String ProductionVersionId
        {
            get;
            set;
        }

        /// <summary>
        /// 生产版本名称
        /// </summary>
        [DisplayName("ProductionVersionName")]
        public String ProductionVersionName
        {
            get;
            set;
        }
        /// <summary>
        /// 时间值
        /// </summary>
        [DisplayName("ProductMakeTimeValue")]
        public int ProductMakeTimeValue
        {
            get;
            set;
        }

        /// <summary>
        /// 时间单位
        /// </summary>
        [DisplayName("ProductMakeUnit")]
        public int ProductMakeUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 产品产能
        /// </summary>
        [DisplayName("ProductMakeValue")]
        public decimal ProductMakeValue
        {
            get;
            set;
        }
        /// <summary>
        /// 产品产能描述
        /// </summary>
        [DisplayName("CapacityRemarks")]
        public string CapacityRemarks
        {
            get;
            set;
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
        /// 销售客户编码
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
        /// 
        /// </summary>
        [DisplayName("MaterialNum")]
        public String MaterialNum
        {
            get;
            set;
        }
        /// <summary>
        /// 生产物料编码
        /// </summary>
        [DisplayName("MaterialName")]
        public String MaterialName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionNo")]
        public String ProductionNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionName")]
        public String ProductionName
        {
            get ;
            set ;
        }

        /// <summary>
        ///订单优先级 
        /// </summary>
        [DisplayName("订单优先级")]
        public int PriorityLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 生产订单录入时间
        /// </summary>
        [DisplayName("ProductionOrderInputDate")]
        public DateTime ProductionOrderInputDate
        {
            get;
            set;
        }
        /// <summary>
        ///录入人工号
        /// </summary>
        [DisplayName("员工工号")]
        public String EmployeeNum
        {
            get;
            set;
        }
        /// <summary>
        /// 录入人姓名
        /// </summary>
        [DisplayName("员工姓名")]
        public String EmployeeName
        {
            get;
            set;
        }

        /// <summary>
        /// 生产订单截止时间
        /// </summary>
        [DisplayName("ProductionOrderEndDate")]
        public DateTime ProductionOrderEndDate
        {
            get ;
            set ;
        }

        /// <summary>
        /// 计划数量
        /// </summary>
        [DisplayName("PlanTotalQty")]
        public Decimal PlanTotalQty
        {
            get ;
            set ;
        }

        /// <summary>
        /// 版本生产数量
        /// </summary>
        [DisplayName("MakeQty")]
        public Decimal MakeQty
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
            get;
            set;
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
        /// 
        /// </summary>
        [DisplayName("Remarks")]
        public String Remarks
        {
            get ;
            set ;
        }

        /// <summary>
        /// 是否已排程
        /// </summary>
        [DisplayName("IsAPS")]
        public int IsAPS
        {
            get;
            set;
        }



    }
}
