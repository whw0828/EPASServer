using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4290
    /// Description:部门信息
    /// </summary>
    public class Department: BaseEntity
    {
        public Department()
        {
        }

        /// <summary>
        /// 表名: "Departmen"
        /// </summary>

        /// <summary>
        /// DepartmentId
        /// </summary>
        [DisplayName("部门ID")]
        public string DepartmentId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 部门编号
        /// </summary>
        [DisplayName("DepartmentNum")]
        public string DepartmentNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// DepartmentName
        /// </summary>
        [DisplayName("部门名称")]
        public string DepartmentName
        {
            get ;
            set ;
        }
}
}
