using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5192
    /// Description:一个工序只能输出一种产品
    /// </summary>
    public class WorkOrderOutProduct:BaseEntity
    {
        public WorkOrderOutProduct()
        {
        }

        /// <summary>
        /// 表名: "WorkOrderOutProduct"
        /// </summary>

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
        /// 生产版本ID
        /// </summary>
        [DisplayName("ProductionVersionId")]
        public String ProductionVersionId
        {
            get;
            set;
        }
        /// <summary>
        /// 生产版本中包含的工序ID
        /// </summary>
        [DisplayName("ProductionMakeWorkOrderId")]
        public String ProductionMakeWorkOrderId
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
        /// 物料编码
        /// </summary>
        [DisplayName("MaterialNum")]
        public String MaterialNum
        {
            get ;
            set ;
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
        /// BOM构成数量
        /// </summary>
        [DisplayName("Qty")]
        public decimal Qty
        {
            get;
            set;
        }

        /// <summary>
        ///产品单位
        /// </summary>
        [DisplayName("单位")]
        public string UnitCode
        {
            get;
            set;
        }

        /// <summary>
        ///质检类型
        /// </summary>
        [DisplayName("质检类型")]
        public int QCProcessType
        {
            get;
            set;
        }

        /// <summary>
        ///质检文档编号
        /// </summary>
        [DisplayName("质检文档编号")]
        public string InspectionDocumentNum
        {
            get;
            set;
        }
    }
}
