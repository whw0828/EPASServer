using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3945
    /// Description:完工装箱
    /// </summary>
    public class FinishPackingDetail:BaseEntity
    {
        public FinishPackingDetail()
        {
        }

        /// <summary>
        /// 表名: "FinishPackingDetail"
        /// </summary>

        /// <summary>
        /// FinishPackingDetailId
        /// </summary>
        [DisplayName("理货ID")]
        public String FinishPackingDetailId
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
        [DisplayName("ProductionBarCode")]
        public String ProductionBarCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// FinishPackingQty
        /// </summary>
        [DisplayName("理货件数")]
        public Decimal FinishPackingQty
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
        [DisplayName("BarStockStatus")]
        public int? BarStockStatus
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCProcessFlag")]
        public int QCProcessFlag
        {
            get ;
            set ;
        }
}
}
