using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.2977
    /// Description:
    /// </summary>
    public class CauseNGProductionAplly:BaseEntity
    {
        public CauseNGProductionAplly()
        {
        }

        /// <summary>
        /// 表名: "CauseNGProductionAplly"
        /// </summary>

        /// <summary>
        /// CauseNGProductionApllyId
        /// </summary>
        [DisplayName("机械工票ID")]
        public String CauseNGProductionApllyId
        {
            get ;
            set ;
        }
        /// <summary>
        /// QCSuspiciousProductRecordId
        /// </summary>
        [DisplayName("残损管理ID")]
        public String QCSuspiciousProductRecordId
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
}
}
