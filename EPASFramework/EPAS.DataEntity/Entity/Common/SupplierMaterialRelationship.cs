using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4480
    /// Description:
    /// </summary>
    public class SupplierMaterialRelationship : BaseEntity
    {
        public SupplierMaterialRelationship()
        {
        }

        /// <summary>
        /// 表名: "SupplierMaterialRelationship"
        /// </summary>

        /// <summary>
        /// SupplierMaterialRelationshipId
        /// </summary>
        [DisplayName("供应商物料关系ID")]
        public String SupplierMaterialRelationshipId
        {
            get ;
            set ;
        }
        /// <summary>
        /// SupplierCode
        /// </summary>
        [DisplayName("供应商编码")]
        public String SupplierCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 物料编码
        /// </summary>
        [DisplayName("物料编码")]
        public String MaterialNum
        {
            get ;
            set ;
        }
      
}
}
