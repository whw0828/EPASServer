using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5251
    /// Description:
    /// </summary>
    public class WorkOrderParam:BaseEntity
    {
        public WorkOrderParam()
        {
        }

        /// <summary>
        /// 表名: "WorkOrderParam"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderParamId")]
        public String WorkOrderParamId
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
        [DisplayName("TechnologyParamName")]
        public String TechnologyParamName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TechnologyParamValue")]
        public String TechnologyParamValue
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TechnologyParamCurrentValue")]
        public String TechnologyParamCurrentValue
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ParamUnit")]
        public String ParamUnit
        {
            get ;
            set ;
        }
}
}
