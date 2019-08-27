using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4793
    /// Description:
    /// </summary>
    public class ProductionOrderBatch:BaseEntity
    {
        public ProductionOrderBatch()
        {
        }

        /// <summary>
        /// 表名: "ProductionOrderBatch"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionOrderBatchId")]
        public String ProductionOrderBatchId
        {
            get ;
            set ;
        }
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
        [DisplayName("BatchStartTime")]
        public DateTime BatchStartTime
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BatchEndTime")]
        public DateTime BatchEndTime
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BatchQty")]
        public Decimal BatchQty
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
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Status")]
        public int? Status
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("OrderNo")]
        public int? OrderNo
        {
            get ;
            set ;
        }
}
}
