using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5122
    /// Description:获取一道工序中需要的物料编码、以及数量
   
   //前设置：2ps  表示【1秒2个】；   2pm  表示【1分钟2个】
    /// </summary>
    public class WorkOrderMaterialIn:BaseEntity
    {
        public WorkOrderMaterialIn()
        {
        }

        /// <summary>
        /// 表名: "WorkOrderMaterialIn"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderMaterialInId")]
        public String WorkOrderMaterialInId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderOutProductId")]
        public String WorkOrderOutProductId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 物料编码
        /// </summary>
        [DisplayName("MaterialNum")]
        public String MaterialNum
        {
            get;
            set;
        }
        /// <summary>
        /// 物料名称
        /// </summary>
        [DisplayName("MaterialName")]
        public String MaterialName
        {
            get;
            set;
        }
        /// <summary>
        /// ProductionMakeWorkOrderId
        /// </summary>
        [DisplayName("冗余字段")]
        public String ProductionMakeWorkOrderId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Qty")]
        public Decimal Qty
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
        /// <summary>
        /// 损耗率
        /// </summary>
        [DisplayName("ScrapRate")]
        public Decimal ScrapRate
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TraceType")]
        public int? TraceType
        {
            get ;
            set ;
        }
}
}
