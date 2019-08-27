using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4041
    /// Description:公用代码
    /// </summary>
    public class PublicCodeInfo:BaseEntity
    {
        public PublicCodeInfo()
        {
        }

        /// <summary>
        /// 表名: "PublicCodeInfo"
        /// </summary>

        /// <summary>
        /// PublicCodeName
        /// </summary>
        [DisplayName("名称")]
        public String PublicCodeName
        {
            get ;
            set ;
        }
        /// <summary>
        /// PublicCode
        /// </summary>
        [DisplayName("代码")]
        public String PublicCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// PublicCodeValue
        /// </summary>
        [DisplayName("值")]
        public Decimal PublicCodeValue
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
