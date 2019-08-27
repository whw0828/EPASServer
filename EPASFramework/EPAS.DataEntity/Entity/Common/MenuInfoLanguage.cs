using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3732
    /// Description:菜单信息
    /// </summary>
    public class MenuInfoLanguage : BaseEntity
    {
        public MenuInfoLanguage()
        {
        }

        /// <summary>
        /// 表名: "MenuInfoLanguage"
        /// </summary>

        /// <summary>
        /// MenuInfoLanguageId
        /// </summary>
        [DisplayName("菜单语言信息ID")]
        public string MenuInfoLanguageId
        {
            get;
            set;
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
        /// MenuName
        /// </summary>
        [DisplayName("菜单名")]
        public string MenuName
        {
            get ;
            set ;
        }

        /// <summary>
        /// LanguageCode
        /// </summary>
        [DisplayName("语言代码")]
        public string LanguageCode
        {
            get ;
            set ;
        }

        /// <summary>
        /// LanguageName
        /// </summary>
        [DisplayName("语言显示名称")]
        public string LanguageName
        {
            get;
            set;
        }



    }
}
