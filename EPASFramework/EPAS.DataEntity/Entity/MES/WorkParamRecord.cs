using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5600
    /// Description:
    /// </summary>
    public class WorkParamRecord:BaseEntity
    {
        public WorkParamRecord()
        {
        }

        /// <summary>
        /// 表名: "WorkParamRecord"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkParamRecordId")]
        public String WorkParamRecordId
        {
            get ;
            set ;
        }
        /// <summary>
        /// ProductionOperationRecordId
        /// </summary>
        [DisplayName("机械工票ID")]
        public String ProductionOperationRecordId
        {
            get ;
            set ;
        }
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
        /// 
        /// </summary>
        [DisplayName("MainBarCode")]
        public String MainBarCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderParamId")]
        public String WorkOrderParamId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TechnologyParamRecordValue")]
        public String TechnologyParamRecordValue
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrdeId")]
        public String WorkOrdeId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialNumberId")]
        public String MaterialNumberId
        {
            get ;
            set ;
        }
}
}
