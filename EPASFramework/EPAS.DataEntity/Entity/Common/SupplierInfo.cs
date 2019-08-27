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
    public class SupplierInfo:BaseEntity
    {
        public SupplierInfo()
        {
        }

        /// <summary>
        /// 表名: "SupplierInfo"
        /// </summary>

        /// <summary>
        /// SupplierInfoId
        /// </summary>
        [DisplayName("供应商ID")]
        public String SupplierInfoId
        {
            get ;
            set ;
        }
        /// <summary>
        /// SupplierCode
        /// </summary>
        [DisplayName("(来源于采购订单)")]
        public String SupplierCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// SupplierName
        /// </summary>
        [DisplayName("(来源于采购订单)")]
        public String SupplierName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BusinessRelationCode")]
        public String BusinessRelationCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Address")]
        public String Address
        {
            get ;
            set ;
        }
}
}
