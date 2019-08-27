using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3901
    /// Description:条码来源 ：仓库、生产  
   //仓库黑名单：禁止生产备料、出库操作  
   //生产黑名单：禁止工序流转、作业报工、生产入库操作
    /// </summary>
    public class QCBarCodeLock:BaseEntity
    {
        public QCBarCodeLock()
        {
        }

        /// <summary>
        /// 表名: "QCBarCodeLock"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCBarCodeLockId")]
        public String QCBarCodeLockId
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
        [DisplayName("Lock")]
        public int Lock
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Comment")]
        public String Comment
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BatchNumber")]
        public String BatchNumber
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
        [DisplayName("WorkOrdeId")]
        public String WorkOrdeId
        {
            get ;
            set ;
        }
}
}
