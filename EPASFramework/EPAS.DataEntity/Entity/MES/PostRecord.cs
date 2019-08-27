using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4025
    /// Description:
    /// </summary>
    public class PostRecord:BaseEntity
    {
        public PostRecord()
        {
        }

        /// <summary>
        /// 表名: "PostRecord"
        /// </summary>

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
        [DisplayName("ClassWorkPlanId")]
        public String ClassWorkPlanId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderPositionId")]
        public String WorkOrderPositionId
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
        [DisplayName("EmployeeNum")]
        public String EmployeeNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EmployeeName")]
        public String EmployeeName
        {
            get ;
            set ;
        }
}
}
