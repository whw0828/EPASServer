using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4599
    /// Description:用户角色
    /// </summary>
    public class UserRoleRelationship:BaseEntity
    {
        public UserRoleRelationship()
        {
        }

        /// <summary>
        /// 表名: "UserRoleRelationship"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("UserRoleRelationshipId")]
        public String UserRoleRelationshipId
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
        /// <summary>
        /// UserInfoId
        /// </summary>
        [DisplayName("用户账号")]
        public string UserInfoId
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
