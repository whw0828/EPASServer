using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3715
    /// Description:
    /// </summary>
    public class APSTemplateDetail:BaseEntity
    {
        public APSTemplateDetail()
        {
        }

        /// <summary>
        /// 表名: "APSTemplateDetail"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("APSTemplateDetailId")]
        public String APSTemplateDetailId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassWorkPlanAPSTemplateName")]
        public String ClassWorkPlanAPSTemplateName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassWorkPlanAPSTemplateNum")]
        public String ClassWorkPlanAPSTemplateNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionVersionId")]
        public String ProductionVersionId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProcessOrder")]
        public int? ProcessOrder
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
        [DisplayName("ProductionMakeWorkOrderId")]
        public String ProductionMakeWorkOrderId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderProductionResourceId")]
        public String WorkOrderProductionResourceId
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
        [DisplayName("FrontBomSet")]
        public String FrontBomSet
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MakeBomSet")]
        public String MakeBomSet
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BackBomSet")]
        public String BackBomSet
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("FrontBomValue")]
        public Decimal? FrontBomValue
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MakeBomValue")]
        public Decimal? MakeBomValue
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BackBomValue")]
        public Decimal? BackBomValue
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
        [DisplayName("FinishProductRate")]
        public Decimal? FinishProductRate
        {
            get ;
            set ;
        }
}
}
