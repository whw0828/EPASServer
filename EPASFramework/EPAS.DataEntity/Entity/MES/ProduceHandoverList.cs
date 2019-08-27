using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4094
    /// Description:
    /// </summary>
    public class ProduceHandoverList:BaseEntity
    {
        public ProduceHandoverList()
        {
        }

        /// <summary>
        /// 表名: "ProduceHandoverList"
        /// </summary>

        /// <summary>
        /// ProduceHandoverListId
        /// </summary>
        [DisplayName("火车作业交接ID")]
        public String ProduceHandoverListId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CSWorkPlanNo")]
        public String CSWorkPlanNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("NSWorkPlanNo")]
        public String NSWorkPlanNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BarCode")]
        public String BarCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// SystmBatchNum
        /// </summary>
        [DisplayName("标记位+时间（6位）+两位流水")]
        public String SystmBatchNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialNo")]
        public String MaterialNo
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
        /// CSEmployeeNum
        /// </summary>
        [DisplayName("员工工号")]
        public String CSEmployeeNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// CSEmployeeName
        /// </summary>
        [DisplayName("员工姓名")]
        public String CSEmployeeName
        {
            get ;
            set ;
        }
        /// <summary>
        /// NSEmployeeNum
        /// </summary>
        [DisplayName("员工工号")]
        public String NSEmployeeNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// NSEmployeeName
        /// </summary>
        [DisplayName("员工姓名")]
        public String NSEmployeeName
        {
            get ;
            set ;
        }
        /// <summary>
        /// StorageInfoId
        /// </summary>
        [DisplayName("垛位ID")]
        public String StorageInfoId
        {
            get ;
            set ;
        }
}
}
