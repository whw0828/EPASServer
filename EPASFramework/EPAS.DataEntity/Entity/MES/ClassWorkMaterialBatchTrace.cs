using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3536
    /// Description:工序投料中 设定为批次追溯的物料 的投料和产出关系记录
    /// </summary>
    public class ClassWorkMaterialBatchTrace:BaseEntity
    {
        public ClassWorkMaterialBatchTrace()
        {
        }

        /// <summary>
        /// 表名: "ClassWorkMaterialBatchTrace"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassWorkMaterialBatchTraceId")]
        public String ClassWorkMaterialBatchTraceId
        {
            get ;
            set ;
        }
        /// <summary>
        /// ProductionOperationRecordId
        /// </summary>
        [DisplayName("机械工票ID")]
        public String ProductionOperationRecordId
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
        [DisplayName("FeedQty")]
        public Decimal? FeedQty
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
        [DisplayName("SupplierCode")]
        public String SupplierCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SupplierName")]
        public String SupplierName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MainBarCode")]
        public String MainBarCode
        {
            get ;
            set ;
        }
}
}
