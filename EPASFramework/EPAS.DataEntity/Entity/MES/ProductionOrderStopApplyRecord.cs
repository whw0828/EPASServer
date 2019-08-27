using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4723
    /// Description:生产订单停止作业申请 目前上传钉钉
    /// </summary>
    public class ProductionOrderStopApplyRecord : BaseEntity
    {
        public ProductionOrderStopApplyRecord()
        {
        }

        /// <summary>
        /// 表名: "ProductionOrderStopApplyRecordId"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionOrderStopApplyRecordId")]
        public String ProductionOrderStopApplyRecordId
        {
            get ;
            set ;
        }

        

        /// <summary>
        /// 申请流水号
        /// </summary>
        [DisplayName("ApplyNumber")]
        public String ApplyNumber
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
        /// 差额数量
        /// </summary>
        [DisplayName("DQty")]
        public Decimal DQty
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
        /// 停单原因
        /// </summary>
        [DisplayName("StopReason")]
        public String StopReason
        {
            get ;
            set ;
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        [DisplayName("ApplyDate")]
        public DateTime ApplyDate
        {
            get ;
            set ;
        }

        /// <summary>
        /// 申请人
        /// </summary>
        [DisplayName("EmployeeName")]
        public string EmployeeName
        {
            get;
            set;
        }

        /// <summary>
        /// 第三方返回
        /// </summary>
        [DisplayName("ThirdId")]
        public string ThirdId
        {
            get;
            set;
        }

        
    }
}
