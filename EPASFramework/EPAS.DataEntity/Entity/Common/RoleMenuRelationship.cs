using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4350
    /// Description:
    /// </summary>
    public class RoleMenuRelationship:BaseEntity
    {
        public RoleMenuRelationship()
        {
        }

        /// <summary>
        /// 表名: "RoleMenuRelationship"
        /// </summary>

        /// <summary>
        /// RoleMenuRelationshipId
        /// </summary>
        [DisplayName("ID")]
        public string RoleMenuRelationshipId
        {
            get ;
            set ;
        }
        /// <summary>
        /// MenuInfoId
        /// </summary>
        [DisplayName("菜单ID")]
        public string MenuInfoId
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
