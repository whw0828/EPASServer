using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5900
    /// Description:
    /// </summary>
    public class QCSuspiciousProductRecord:BaseEntity
    {
        public QCSuspiciousProductRecord()
        {
        }

        /// <summary>
        /// 表名: "QCSuspiciousProductRecord"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCSuspiciousProductRecordId")]
        public String QCSuspiciousProductRecordId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ScrapOrderId")]
        public String ScrapOrderId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ScrapOrderNum")]
        public String ScrapOrderNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ClassWorkPlanId")]
        public String ClassWorkPlanId
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
        /// 
        /// </summary>
        [DisplayName("MaterialNumberId")]
        public String MaterialNumberId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SuspiciousQty")]
        public Decimal SuspiciousQty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCItemTypeId")]
        public String QCItemTypeId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Operator")]
        public String Operator
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("OperationDate")]
        public DateTime OperationDate
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SuspiciousDesc")]
        public String SuspiciousDesc
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCProcessFlag")]
        public int? QCProcessFlag
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCOperator")]
        public String QCOperator
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCOperationDate")]
        public DateTime QCOperationDate
        {
            get ;
            set ;
        }
}
}
