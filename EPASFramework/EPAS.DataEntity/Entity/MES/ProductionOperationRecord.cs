using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4653
    /// Description:生产作业记录
    /// </summary>
    public class ProductionOperationRecord:BaseEntity
    {
        public ProductionOperationRecord()
        {
        }

        /// <summary>
        /// 表名: "ProductionOperationRecord"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionOperationRecordId")]
        public String ProductionOperationRecordId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassWorkPlanId")]
        public String ClassWorkPlanId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkSerialNo")]
        public String WorkSerialNo
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
        [DisplayName("MainProductionResourceId")]
        public String MainProductionResourceId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SubProductionResourceId")]
        public String SubProductionResourceId
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
        [DisplayName("PostRecordId")]
        public String PostRecordId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderId")]
        public String WorkOrderId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkPositionId")]
        public String WorkPositionId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EmployeeInfoId")]
        public String EmployeeInfoId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("StartDate")]
        public DateTime? StartDate
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EndDate")]
        public DateTime? EndDate
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionBarCode")]
        public String ProductionBarCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Qty")]
        public Decimal Qty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BinBarCode")]
        public String BinBarCode
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
}
}
