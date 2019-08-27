using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3785
    /// Description:
    /// </summary>
    public class ClassWorkPlanRelationship:BaseEntity
    {
        public ClassWorkPlanRelationship()
        {
        }

        /// <summary>
        /// 表名: "ClassWorkPlanRelationship"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassWorkPlanRelationshipId")]
        public String ClassWorkPlanRelationshipId
        {
            get ;
            set ;
        }
        /// <summary>
        /// ClassWorkPlanIdSourceId
        /// </summary>
        [DisplayName("生产工单ID")]
        public String ClassWorkPlanIdSourceId
        {
            get ;
            set ;
        }
        /// <summary>
        /// ClassWorkPlanIdTargetId
        /// </summary>
        [DisplayName("生产工单ID")]
        public String ClassWorkPlanIdTargetId
        {
            get ;
            set ;
        }
        /// <summary>
        /// ClassPlanQty
        /// </summary>
        [DisplayName("班次计划量")]
        public Decimal ClassPlanQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// ClassRealQty
        /// </summary>
        [DisplayName("班次计划量")]
        public Decimal ClassRealQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// DelayQTy
        /// </summary>
        [DisplayName("班次计划量")]
        public Decimal DelayQTy
        {
            get ;
            set ;
        }
}
}
