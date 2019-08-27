using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3117
    /// Description:
    /// </summary>
    public class CauseNGPurchase:BaseEntity
    {
        public CauseNGPurchase()
        {
        }

        /// <summary>
        /// 表名: "CauseNGPurchase"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CauseNGPurchaseId")]
        public String CauseNGPurchaseId
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
        [DisplayName("PurchaseDeliveryOrderId")]
        public String PurchaseDeliveryOrderId
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
