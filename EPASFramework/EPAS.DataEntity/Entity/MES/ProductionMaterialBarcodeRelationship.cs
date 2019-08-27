using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4553
    /// Description:
   //目前只有 生产退料业务 会产生“条码关系数据”
    /// </summary>
    public class ProductionMaterialBarcodeRelationship:BaseEntity
    {
        public ProductionMaterialBarcodeRelationship()
        {
        }

        /// <summary>
        /// 表名: "ProductionMaterialBarcodeRelationship"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionMaterialInfoRelationshipId")]
        public String ProductionMaterialInfoRelationshipId
        {
            get ;
            set ;
        }
        /// <summary>
        /// CBClassWorkPlanId
        /// </summary>
        [DisplayName("生产工单ID")]
        public String CBClassWorkPlanId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SBarCode")]
        public String SBarCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("FBarCode")]
        public String FBarCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// PMClassWorkPlanId
        /// </summary>
        [DisplayName("生产工单ID")]
        public String PMClassWorkPlanId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RelationshipType")]
        public int? RelationshipType
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EnableCount")]
        public int? EnableCount
        {
            get ;
            set ;
        }
}
}
