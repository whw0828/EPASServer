using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3177
    /// Description:
    /// </summary>
    public class ClassFeedMaterialRecord:BaseEntity
    {
        public ClassFeedMaterialRecord()
        {
        }

        /// <summary>
        /// 表名: "ClassFeedMaterialRecord"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassFeedMaterialRecordId")]
        public String ClassFeedMaterialRecordId
        {
            get ;
            set ;
        }
        /// <summary>
        /// ClassWorkPlanId
        /// </summary>
        [DisplayName("生产工单ID")]
        public String ClassWorkPlanId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BarCode")]
        public String BarCode
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
        public Decimal FeedQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// EmployeeNum
        /// </summary>
        [DisplayName("员工ID（如果该字段不为空，则工人数为1）")]
        public String EmployeeNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("FeedTime")]
        public DateTime? FeedTime
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
        [DisplayName("MainBarCode")]
        public String MainBarCode
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
        [DisplayName("FeedStatus")]
        public int? FeedStatus
        {
            get ;
            set ;
        }
}
}
