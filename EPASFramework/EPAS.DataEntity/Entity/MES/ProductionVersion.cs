using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4992
    /// Description:生产版本包括  工序、工序投料、工序产出、前工序依赖（工序关系）、工序可以使用的资源（设备、模具）、工序作业指导书  共6个元素。
    /// </summary>
    public class ProductionVersion:BaseEntity
    {
        public ProductionVersion()
        {
        }

        /// <summary>
        /// 表名: "ProductionVersion"
        /// </summary>

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
            get;
            set;
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
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionVersionPriority")]
        public int? ProductionVersionPriority
        {
            get ;
            set ;
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        [DisplayName("IsEnable")]
        public int IsEnable
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Version")]
        public String Version
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("VersionRecord")]
        public String VersionRecord
        {
            get ;
            set ;
        }

        /// <summary>
        /// 时间值
        /// </summary>
        [DisplayName("ProductMakeTimeValue")]
        public int ProductMakeTimeValue
        {
            get;
            set;
        }

        /// <summary>
        /// 时间单位
        /// </summary>
        [DisplayName("ProductMakeUnit")]
        public int ProductMakeUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 产品产能
        /// </summary>
        [DisplayName("ProductMakeValue")]
        public decimal ProductMakeValue
        {
            get;
            set;
        }
        /// <summary>
        /// 产品产能描述
        /// </summary>
        [DisplayName("CapacityRemarks")]
        public string CapacityRemarks
        {
            get;
            set;
        }
        
    }
}
