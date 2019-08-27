using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5680
    /// Description:
    /// </summary>
    public class WorkPosition:BaseEntity
    {
        public WorkPosition()
        {
        }

        /// <summary>
        /// 表名: "WorkPosition"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkPositionId")]
        public String WorkPositionId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkPositionNum")]
        public String WorkPositionNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkPositionName")]
        public String WorkPositionName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TemplateId")]
        public String TemplateId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Comment")]
        public String Comment
        {
            get ;
            set ;
        }
}
}
