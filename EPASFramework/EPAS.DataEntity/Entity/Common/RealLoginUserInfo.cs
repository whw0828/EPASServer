using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4171
    /// Description:
    /// </summary>
    public class RealLoginUserInfo:BaseEntity
    {
        public RealLoginUserInfo()
        {
        }

        /// <summary>
        /// 表名: "RealLoginUserInfo"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RealLoginUserInfoId")]
        public string RealLoginUserInfoId
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
        /// LastTime
        /// </summary>
        [DisplayName("上次登录时间")]
        public DateTime? LastTime
        {
            get ;
            set ;
        }
        /// <summary>
        /// LastIp
        /// </summary>
        [DisplayName("上次登录IP")]
        public string LastIp
        {
            get ;
            set ;
        }
        /// <summary>
        /// Mac
        /// </summary>
        [DisplayName("登录电脑信息")]
        public String Mac
        {
            get ;
            set ;
        }
}
}
