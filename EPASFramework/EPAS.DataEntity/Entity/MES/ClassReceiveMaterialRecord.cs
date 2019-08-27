using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3456
    /// Description:需要仓库模块 在 PurchaseBarInfo表中提供条码信息，生产才能获取领料的条码信息（例如 物料编码、物料名称、数量、单位 供应商等）
    /// </summary>
    public class ClassReceiveMaterialRecord:BaseEntity
    {
        public ClassReceiveMaterialRecord()
        {
        }

        /// <summary>
        /// 表名: "ClassReceiveMaterialRecord"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassReceiveMaterialRecordId")]
        public String ClassReceiveMaterialRecordId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassReceiveMaterialPlanId")]
        public String ClassReceiveMaterialPlanId
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
        /// BatchNumber
        /// </summary>
        [DisplayName("标记位+时间（6位）+两位流水")]
        public String BatchNumber
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
        [DisplayName("RealBOMQty")]
        public Decimal RealBOMQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("UnitCode")]
        public String UnitCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// EmployeeNum
        /// </summary>
        [DisplayName("操作人")]
        public String EmployeeNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// CRMPDate
        /// </summary>
        [DisplayName("操作时间")]
        public DateTime? CRMPDate
        {
            get ;
            set ;
        }
}
}
