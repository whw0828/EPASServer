using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5491
    /// Description:生产设备和工装的配套关系
    /// </summary>
    public class ProductionEquipmentToolingRelationship : BaseEntity
    {
        public ProductionEquipmentToolingRelationship()
        {
        }

        /// <summary>
        /// 表名: "ProductionEquipmentToolingRelationship"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionEquipmentToolingRelationshipId")]
        public String ProductionEquipmentToolingRelationshipId
        {
            get ;
            set ;
        }

      
        /// <summary>
        /// 生产主资源编码
        /// </summary>
        [DisplayName("MainProductionResourceCode")]
        public String MainProductionResourceCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 生产主资源名称
        /// </summary>
        [DisplayName("MainProductionResourceName")]
        public String MainProductionResourceName
        {
            get;
            set;
        }
        /// <summary>
        /// 生产副资源编码
        /// </summary>
        [DisplayName("SubProductionResourceCode")]
        public String SubProductionResourceCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 生产副资源名称
        /// </summary>
        [DisplayName("SubProductionResourceName")]
        public String SubProductionResourceName
        {
            get;
            set;
        }

        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MainProductionResourcesType")]
        public int MainProductionResourcesType
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SubProductionResourcesType")]
        public int SubProductionResourcesType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Comments")]
        public string Comments
        {
            get;
            set;
        }
    }
}
