using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4290
    /// Description:角色信息
    /// </summary>
    public class RoleInfo:BaseEntity
    {
        public RoleInfo()
        {
        }

        /// <summary>
        /// 表名: "RoleInfo"
        /// </summary>

        /// <summary>
        /// RoleInfoId
        /// </summary>
        [DisplayName("角色ID")]
        public string RoleInfoId
        {
            get ;
            set ;
        }
        /// <summary>
        /// RoleName
        /// </summary>
        [DisplayName("角色名")]
        public string RoleName
        {
            get ;
            set ;
        }
        /// <summary>
        /// Comments
        /// </summary>
        [DisplayName("描述")]
        public string Comments
        {
            get ;
            set ;
        }
}
}
