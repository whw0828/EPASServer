using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3715
    /// Description:
    /// </summary>
    public class APSTemplate : BaseEntity
    {
        public APSTemplate()
        {
        }

        /// <summary>
        /// 表名: "APSTemplate"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("APSTemplateId")]
        public String APSTemplateId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("APSTemplateName")]
        public String APSTemplateName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("APSTemplateNum")]
        public String APSTemplateNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionVersionId")]
        public String ProductionVersionId
        {
            get ;
            set ;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialNum")]
        public String MaterialNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialName")]
        public String MaterialName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionVersionName")]
        public String ProductionVersionName
        {
            get ;
            set ;
        }
      
}
}
