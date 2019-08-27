using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3057
    /// Description:
    /// </summary>
    public class CauseNGProduction:BaseEntity
    {
        public CauseNGProduction()
        {
        }

        /// <summary>
        /// 表名: "CauseNGProduction"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CauseNGProductionId")]
        public String CauseNGProductionId
        {
            get ;
            set ;
        }
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
        [DisplayName("CauseOfNonconformingProductId")]
        public String CauseOfNonconformingProductId
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
        [DisplayName("QCInspector")]
        public String QCInspector
        {
            get ;
            set ;
        }
}
}
