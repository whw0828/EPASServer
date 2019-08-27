using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4723
    /// Description:销售在生产系统中录入要货要求
    /// </summary>
    public class ProductionOrder:BaseEntity
    {
        public ProductionOrder()
        {
        }

        /// <summary>
        /// 表名: "ProductionOrder"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionOrderId")]
        public String ProductionOrderId
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
        /// 生产物料名称
        /// </summary>
        [DisplayName("CustomerNum")]
        public String CustomerNum
        {
            get ;
            set ;
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
        /// 实际完成入库数量
        /// </summary>
        [DisplayName("RealTotalQty")]
        public Decimal RealTotalQty
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
        /// 生产批次
        /// </summary>
        [DisplayName("BatchNumber")]
        public String BatchNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 报废数量
        /// </summary>
        [DisplayName("RjctQty")]
        public Decimal RjctQty
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
        ///订单优先级 
        /// </summary>
        [DisplayName("订单优先级")]
        public int PriorityLevel
        {
            get;
            set;
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
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionOrderStatus")]
        public int ProductionOrderStatus
        {
            get ;
            set ;
        }

        /// <summary>
        /// 生产版本
        /// </summary>
        [DisplayName("ProductionVersionName")]
        public String ProductionVersionName
        {
            get;
            set;
        }
    }
}
