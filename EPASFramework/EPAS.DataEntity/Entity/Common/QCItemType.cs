using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4111
    /// Description:
    /// </summary>
    public class QCItemType:BaseEntity
    {
        public QCItemType()
        {
        }

        /// <summary>
        /// 表名: "QCItemType"
        /// </summary>

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
        [DisplayName("QCItemTypeCode")]
        public String QCItemTypeCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCItemTypeName")]
        public String QCItemTypeName
        {
            get ;
            set ;
        }
}
}
