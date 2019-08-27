using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3483
    /// Description:数据权限功能表
    /// </summary>
    public class DataAuth:BaseEntity
    {
        public DataAuth()
        {
        }

        /// <summary>
        /// 表名: "DataAuth"
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
        /// DataAuthFuncCode
        /// </summary>
        [DisplayName("权限代码")]
        public String DataAuthFuncCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// DataAuthFuncName
        /// </summary>
        [DisplayName("权限名称")]
        public String DataAuthFuncName
        {
            get ;
            set ;
        }
        /// <summary>
        /// DataAuthFuncParentId
        /// </summary>
        [DisplayName("权限父ID")]
        public String DataAuthFuncParentId
        {
            get ;
            set ;
        }
        /// <summary>
        /// OrderNo
        /// </summary>
        [DisplayName("权限顺序号")]
        public Decimal OrderNo
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
        /// <summary>
        /// DataAuthLevel
        /// </summary>
        [DisplayName("权限等级")]
        public Decimal? DataAuthLevel
        {
            get ;
            set ;
        }
        /// <summary>
        /// DataId
        /// </summary>
        [DisplayName("部门ID")]
        public String DataId
        {
            get ;
            set ;
        }
}
}
