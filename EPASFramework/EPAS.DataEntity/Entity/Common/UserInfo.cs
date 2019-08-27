using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4540
    /// Description:系统用户信息

    /// </summary>
    public class UserInfo : BaseEntity
    {
        public UserInfo()
        {
        }

        /// <summary>
        /// 表名: "UserInfo"
        /// </summary>

        /// <summary>
        /// UserInfoId
        /// </summary>
        [DisplayName("用户账号")]
        public string UserInfoId
        {
            get;
            set;
        }
        /// <summary>
        /// EmployeeInfoId
        /// </summary>
        //[DisplayName("员工ID")]
        //public String EmployeeInfoId
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// EmployeeNum
        /// </summary>
        [DisplayName("员工工号")]
        public string EmployeeNum
        {
            get;
            set;
        }

        /// <summary>
        /// EmployeeName
        /// </summary>
        [DisplayName("员工姓名")]
        public string EmployeeName
        {
            get;
            set;
        }

        /// <summary>
        /// UserPasswd
        /// </summary>
        [DisplayName("密码")]
        public string UserPasswd
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("UserLevel")]
        public int? UserLevel
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("DepartmentName")]
        public string DepartmentName
        {
            get;
            set;
        }
        /// <summary>
        /// Tel
        /// </summary>
        [DisplayName("电话")]
        public string Tel
        {
            get;
            set;
        }
        /// <summary>
        /// Mobile
        /// </summary>
        [DisplayName("手机")]
        public string Mobile
        {
            get;
            set;
        }
        /// <summary>
        /// Email
        /// </summary>
        [DisplayName("邮件")]
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// LockStatus
        /// </summary>
        [DisplayName("锁定")]
        public int LockStatus
        {
            get;
            set;
        }

    /// <summary>
    /// Comments
    /// </summary>
    [DisplayName("备注")]
        public string Comments
        {
            get ;
            set ;
        }
}
}
