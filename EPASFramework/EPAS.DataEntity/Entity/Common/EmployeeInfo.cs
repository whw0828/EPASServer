using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3612
    /// Description:员工信息
    /// </summary>
    public class EmployeeInfo:BaseEntity
    {
        public EmployeeInfo()
        {
        }

        /// <summary>
        /// 表名: "EmployeeInfo"
        /// </summary>

        /// <summary>
        /// EmployeeInfoId
        /// </summary>
        [DisplayName("员工ID")]
        public String EmployeeInfoId
        {
            get ;
            set ;
        }
        /// <summary>
        /// EmployeeNum
        /// </summary>
        [DisplayName("员工工号")]
        public String EmployeeNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// EmployeeName
        /// </summary>
        [DisplayName("员工姓名")]
        public String EmployeeName
        {
            get ;
            set ;
        }
        /// <summary>
        /// EmployeeType
        /// </summary>
        [DisplayName("是否本港人员")]
        public int EmployeeType
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
        /// Comments
        /// </summary>
        [DisplayName("备注")]
        public String Comments
        {
            get ;
            set ;
        }
}
}
