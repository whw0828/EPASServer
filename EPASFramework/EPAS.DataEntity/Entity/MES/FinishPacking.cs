using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3865
    /// Description:完工装箱
    /// </summary>
    public class FinishPacking:BaseEntity
    {
        public FinishPacking()
        {
        }

        /// <summary>
        /// 表名: "FinishPacking"
        /// </summary>

        /// <summary>
        /// FinishPackingId
        /// </summary>
        [DisplayName("理货ID")]
        public String FinishPackingId
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
        [DisplayName("TemplateId")]
        public String TemplateId
        {
            get ;
            set ;
        }
        /// <summary>
        /// FinishPackingQty
        /// </summary>
        [DisplayName("理货件数")]
        public Decimal? FinishPackingQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// FinishPackingWorker
        /// </summary>
        [DisplayName("理货员")]
        public String FinishPackingWorker
        {
            get ;
            set ;
        }
        /// <summary>
        /// FinishPackingTime
        /// </summary>
        [DisplayName("时间")]
        public DateTime? FinishPackingTime
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionOrderNumbe")]
        public String ProductionOrderNumbe
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
        [DisplayName("ProductionNo")]
        public String ProductionNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionName")]
        public String ProductionName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BinSorageStatus")]
        public int? BinSorageStatus
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("StorageInType")]
        public int? StorageInType
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WarehouseId")]
        public String WarehouseId
        {
            get ;
            set ;
        }
        /// <summary>
        /// StorageInfoId
        /// </summary>
        [DisplayName("垛位ID")]
        public String StorageInfoId
        {
            get ;
            set ;
        }
}
}
