using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5491
    /// Description:
    /// </summary>
    public class WorkOrderProductionResource:BaseEntity
    {
        public WorkOrderProductionResource()
        {
        }

        /// <summary>
        /// 表名: "WorkOrderProductionResource"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderProductionResourceId")]
        public String WorkOrderProductionResourceId
        {
            get ;
            set ;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionMakeWorkOrderId")]
        public String ProductionMakeWorkOrderId
        {
            get;
            set;
        }
        /// <summary>
        /// 生产版本
        /// </summary>
        [DisplayName("ProductionVersionId")]
        public String ProductionVersionId
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
        /// 生产资源主码
        /// </summary>
        [DisplayName("MainProductionResourceCode")]
        public String MainProductionResourceCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 生产资源主码名称
        /// </summary>
        [DisplayName("MainProductionResourceName")]
        public String MainProductionResourceName
        {
            get;
            set;
        }
        /// <summary>
        /// 生产资源副码
        /// </summary>
        [DisplayName("SubProductionResourceCode")]
        public String SubProductionResourceCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 生产资源名称
        /// </summary>
        [DisplayName("SubProductionResourceName")]
        public String SubProductionResourceName
        {
            get;
            set;
        }

        /// <summary>
        /// 工序前设置时间值
        /// </summary>
        [DisplayName("FrontTimeValue")]
        public decimal FrontTimeValue
        {
            get;
            set;
        }
        /// <summary>
        /// 工序前设置时间单位  以秒为单位计量
        /// </summary>
        [DisplayName("FrontTimeUnit")]
        public int FrontTimeUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 工序标准产能时间值
        /// </summary>
        [DisplayName("StandardMakeTimeValue")]
        public decimal StandardMakeTimeValue
        {
            get;
            set;
        }
        /// <summary>
        /// 工序标准产能时间单位  以秒为单位计量
        /// </summary>
        [DisplayName("StandardMakeTimeUnit")]
        public int StandardMakeTimeUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 工序标准产能数量
        /// </summary>
        [DisplayName("StandardMakeValue")]
        public Decimal StandardMakeValue
        {
            get;
            set;
        }

        /// <summary>
        /// 工序后设置时间值
        /// </summary>
        [DisplayName("BackTimeValue")]
        public decimal BackTimeValue
        {
            get;
            set;
        }
        /// <summary>
        /// 工序前后置时间单位  以秒为单位计量
        /// </summary>
        [DisplayName("BackTimeUnit")]
        public int BackTimeUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("FinishProductRate")]
        public Decimal FinishProductRate
        {
            get ;
            set ;
        }
}
}
