using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4434
    /// Description:
    /// </summary>
    public class ProductionMakeWorkOrderRelation:BaseEntity
    {
        public ProductionMakeWorkOrderRelation()
        {
        }

        /// <summary>
        /// 表名: "ProductionMakeWorkOrderRelation"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionMakeWorkOrderRelationId")]
        public String ProductionMakeWorkOrderRelationId
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
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RProductionMakeWorkOrderId")]
        public String RProductionMakeWorkOrderId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RelationType")]
        public int RelationType
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("STime")]
        public int STime
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ETime")]
        public int ETime
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TimeUnit")]
        public int TimeUnit
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WTR")]
        public int WTR
        {
            get ;
            set ;
        }
}
}
