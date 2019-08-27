using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5311
    /// Description:
    /// </summary>
    public class WorkOrderPosition:BaseEntity
    {
        public WorkOrderPosition()
        {
        }

        /// <summary>
        /// 表名: "WorkOrderPosition"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WorkOrderPositionId")]
        public String WorkOrderPositionId
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
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionMakeWorkOrderId")]
        public String ProductionMakeWorkOrderId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 工序中工位作业顺序号
        /// </summary>
        [DisplayName("WOPOrderNum")]
        public int WOPOrderNum
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
        /// 工位编码
        /// </summary>
        [DisplayName("WorkPositionNum")]
        public String WorkPositionNum
        {
            get;
            set;
        }
        /// <summary>
        /// 工位名称
        /// </summary>
        [DisplayName("WorkPositionName")]
        public String WorkPositionName
        {
            get;
            set;
        }

        /// <summary>
        /// 当前作业指导书编号
        /// </summary>
        [DisplayName("WorkInstructionNum")]
        public String WorkInstructionNum
        {
            get;
            set;
        }

        /// <summary>
        /// 当前工艺卡编号
        /// </summary>
        [DisplayName("ProcessCardNum")]
        public String ProcessCardNum
        {
            get;
            set;
        }
    }
}
