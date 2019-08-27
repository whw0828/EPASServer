using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3612
    /// Description:员工和第三方系统用户关系
    /// </summary>
    public class EmployeesThirdParty : BaseEntity
    {
        public EmployeesThirdParty()
        {
        }

        /// <summary>
        /// 表名: "EmployeesThirdParty"
        /// </summary>

        /// <summary>
        /// EmployeesThirdPartyId
        /// </summary>
        [DisplayName("员工和第三方系统用户关系ID")]
        public String EmployeesThirdPartyId
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
        /// ThirdPartId
        /// </summary>
        [DisplayName("第三方ID")]
        public string ThirdPartId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SysThirdPartName")]
        public String SysThirdPartName
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
