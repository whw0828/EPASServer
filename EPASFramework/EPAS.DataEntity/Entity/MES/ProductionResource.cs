using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5062
    /// Description:
    /// </summary>
    public class ProductionResource: BaseEntity
    {
        public ProductionResource()
        {
        }

        /// <summary>
        /// 表名: "WorkOrder"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionResourceId")]
        public String ProductionResourceId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionResourceCode")]
        public String ProductionResourceCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionResourceName")]
        public String ProductionResourceName
        {
            get ;
            set ;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionResourceStatus")]
        public int ProductionResourceStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Comments")]
        public String Comments
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TemplateId")]
        public String TemplateId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionResourcesType")]
        public int ProductionResourcesType
        {
            get;
            set;
        }

        /// <summary>
        /// 生产资源编码  递归设计
        /// </summary>
        [DisplayName("GroupProductionResourceCode")]
        public string GroupProductionResourceCode
        {
            get;
            set;
        }
    }
}
