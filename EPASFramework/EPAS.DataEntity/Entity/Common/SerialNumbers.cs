using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.4420
    /// Description:
    /// </summary>
    public class SerialNumbers:BaseEntity
    {
        public SerialNumbers()
        {
        }

        /// <summary>
        /// 表名: "SerialNumbers"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SerialNumberId")]
        public String SerialNumberId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SerialType")]
        public String SerialType
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SerialValue")]
        public int? SerialValue
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaxValue")]
        public int? MaxValue
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Comments")]
        public String Comments
        {
            get ;
            set ;
        }
    }
}
