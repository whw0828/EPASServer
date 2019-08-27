using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3376
    /// Description:
    /// </summary>
    public class ClassReceiveMaterialPlan:BaseEntity
    {
        public ClassReceiveMaterialPlan()
        {
        }

        /// <summary>
        /// 表名: "ClassReceiveMaterialPlan"
        /// </summary>

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
        [DisplayName("ReceiveOrder")]
        public int ReceiveOrder
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
        [DisplayName("WorkOrderMaterialInId")]
        public String WorkOrderMaterialInId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CRMSerialNumber")]
        public String CRMSerialNumber
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionId")]
        public String ProductionId
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
        [DisplayName("PlanBOMQty")]
        public Decimal PlanBOMQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RealPrepareBOMQty")]
        public Decimal? RealPrepareBOMQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RealBOMQty")]
        public Decimal? RealBOMQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("PlanCRMDate")]
        public DateTime PlanCRMDate
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RealCRMDate")]
        public DateTime? RealCRMDate
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("PlanType")]
        public int PlanType
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Content")]
        public String Content
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CRMState")]
        public int? CRMState
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("HandoverStatus")]
        public int? HandoverStatus
        {
            get ;
            set ;
        }
        /// <summary>
        /// ThirdReturnGUID
        /// </summary>
        [DisplayName("目前是钉钉业务平台返回值")]
        public String ThirdReturnGUID
        {
            get ;
            set ;
        }
}
}
