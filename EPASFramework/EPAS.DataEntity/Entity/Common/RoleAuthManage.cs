using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4230
    /// Description:角色权限管理
    /// </summary>
    public class RoleAuthManage:BaseEntity
    {
        public RoleAuthManage()
        {
        }

        /// <summary>
        /// 表名: "RoleAuthManage"
        /// </summary>

        /// <summary>
        /// DataAuthFuncId
        /// </summary>
        [DisplayName("权限ID")]
        public String DataAuthFuncId
        {
            get ;
            set ;
        }
        /// <summary>
        /// RoleInfoId
        /// </summary>
        [DisplayName("角色ID")]
        public string RoleInfoId
        {
            get ;
            set ;
        }
}
}
