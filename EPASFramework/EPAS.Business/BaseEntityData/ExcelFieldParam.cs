using EPAS.DataEntity.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAS.DataEntity.Enum;
namespace EPAS.BaseEntityData
{
    public class ExcelFieldParam
    {
        /// <summary>
        /// 
        /// </summary>
        //[DisplayName("实体中字段名称")]
        //public String FieldName
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("是否换算")]
        public CustomEnable IsTrans
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("换算类型")]
        public ExcelTransType  TransType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Excel中字段名称")]
        public String ExcelFieldName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("换算后字段名称")]
        public String  TransFieldName
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        [DisplayName("枚举/类名称")]
        public string ClassName
        {
            get;
            set;
        }
    }
}
