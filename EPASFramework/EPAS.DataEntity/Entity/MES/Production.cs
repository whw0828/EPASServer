using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4215
    /// Description:
    /// </summary>
    public class Production:BaseEntity
    {
        public Production()
        {
        }

        /// <summary>
        /// 表名: "Production"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionId")]
        public String ProductionId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionNo")]
        public String ProductionNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionName")]
        public String ProductionName
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
            get;
            set;
        }

        /// <summary>
        /// 销售客户编码
        /// </summary>
        [DisplayName("CustomerNum")]
        public String CustomerNum
        {
            get;
            set;
        }
        /// <summary>
        /// 销售客户名称
        /// </summary>
        [DisplayName("CustomerName")]
        public String CustomerName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Contents")]
        public String Contents
        {
            get ;
            set ;
        }

        /// <summary>
        /// 单位
        /// </summary>
        [DisplayName("UnitCode")]
        public String UnitCode
        {
            get;
            set;
        }
    }
}
