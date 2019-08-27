using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5491
    /// Description:
    /// </summary>
    public class CompetitiveEquipment: BaseEntity
    {
        public CompetitiveEquipment()
        {
        }

        /// <summary>
        /// 表名: "CompetitiveEquipment"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CompetitiveEquipmentId")]
        public String CompetitiveEquipmentId
        {
            get ;
            set ;
        }

        /// <summary>
        /// 生产版本
        /// </summary>
        [DisplayName("ProductionVersionId")]
        public String ProductionVersionId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 工序编码
        /// </summary>
        [DisplayName("WorkOrderNum")]
        public String WorkOrderNum
        {
            get;
            set;
        }
        /// <summary>
        /// 工序名称
        /// </summary>
        [DisplayName("WorkOrderName")]
        public String WorkOrderName
        {
            get;
            set;
        }
        /// <summary>
        /// 生产资源主码
        /// </summary>
        [DisplayName("MainProductionResourceCode")]
        public String MainProductionResourceCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 生产资源主码名称
        /// </summary>
        [DisplayName("MainProductionResourceName")]
        public String MainProductionResourceName
        {
            get;
            set;
        }
        /// <summary>
        /// 生产资源副码
        /// </summary>
        [DisplayName("SubProductionResourceCode")]
        public String SubProductionResourceCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 生产资源名称
        /// </summary>
        [DisplayName("SubProductionResourceName")]
        public String SubProductionResourceName
        {
            get;
            set;
        }
        /// <summary>
        /// 竞争优先级
        /// </summary>
        [DisplayName("PriorityLevel")]
        public int PriorityLevel
        {
            get;
            set;
        }
        


    }
}
