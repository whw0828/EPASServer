using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5062
    /// Description:
    /// </summary>
    public class WorkOrder:BaseEntity
    {
        public WorkOrder()
        {
        }

        /// <summary>
        /// 表名: "WorkOrder"
        /// </summary>

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
        [DisplayName("WorkOrderNum")]
        public String WorkOrderNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderName")]
        public String WorkOrderName
        {
            get ;
            set ;
        }
}
}
