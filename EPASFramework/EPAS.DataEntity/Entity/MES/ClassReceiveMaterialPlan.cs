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
        [DisplayName("WorkPlanNo")]
        public String WorkPlanNo
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
            get;
            set;
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
        [DisplayName("WorkOrderNum")]
        public String WorkOrderNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderName")]
        public String WorkOrderName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialNum")]
        public String MaterialNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialName")]
        public String MaterialName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("UseDateTime")]
        public DateTime UseDateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 仓库实际配送
        /// </summary>
        [DisplayName("SendQty")]
        public Decimal SendQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 计划领用
        /// </summary>
        [DisplayName("PlanQty")]
        public Decimal PlanQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 欠数
        /// </summary>
        [DisplayName("OwnQty")]
        public Decimal OwnQty
        {
            get ;
            set ;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("GraphicNo")]
        public String GraphicNo
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Type")]
        public String Type
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Format")]
        public String Format
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("UnitCode")]
        public String UnitCode
        {
            get;
            set;
        }

        /// <summary>
        /// 先进先出
        /// </summary>
        [DisplayName("IOStrategy")]
        public int IOStrategy
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("PlanType")]
        public int PlanType
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CRMStatus")]
        public int CRMStatus
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("HandoverStatus")]
        public int HandoverStatus
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

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("Comment")]
        public String Comment
        {
            get;
            set;
        }
    }
}
