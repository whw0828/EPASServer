using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5760
    /// Description:
    /// </summary>
    public class SampleProductionQCRecord:BaseEntity
    {
        public SampleProductionQCRecord()
        {
        }

        /// <summary>
        /// 表名: "SampleProductionQCRecord"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SampleProductionQCRecordId")]
        public String SampleProductionQCRecordId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MachineTicketId")]
        public String MachineTicketId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassWorkPlanId")]
        public String ClassWorkPlanId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCItemTypeId")]
        public String QCItemTypeId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BarCode")]
        public String BarCode
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
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BatchNumber")]
        public String BatchNumber
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SampleQty")]
        public Decimal SampleQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCProcessFlag")]
        public int? QCProcessFlag
        {
            get ;
            set ;
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
        [DisplayName("QCSampler")]
        public String QCSampler
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("IsCrapComplete")]
        public int IsCrapComplete
        {
            get ;
            set ;
        }
}
}
