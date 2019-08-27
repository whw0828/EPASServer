using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3413
    /// Description:
    /// </summary>
    public class Customer:BaseEntity
    {
        public Customer()
        {
        }

        /// <summary>
        /// 表名: "Customer"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerId")]
        public String CustomerId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerNum")]
        public String CustomerNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerName")]
        public String CustomerName
        {
            get ;
            set ;
        }
}
}
