using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5830
    /// Description:
    /// </summary>
    public class ScrapNoticeInfo:BaseEntity
    {
        public ScrapNoticeInfo()
        {
        }

        /// <summary>
        /// 表名: "ScrapNoticeInfo"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ScrapNoticeId")]
        public String ScrapNoticeId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ScrapOrderNum")]
        public String ScrapOrderNum
        {
            get ;
            set ;
        }
}
}
