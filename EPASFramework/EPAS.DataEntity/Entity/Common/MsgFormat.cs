using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3832
    /// Description:
    /// </summary>
    public class MsgChannel : BaseEntity
    {
        public MsgChannel()
        {
        }

        /// <summary>
        /// 表名: "MsgFormat"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Id")]
        public String Id
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ChannelName")]
        public String ChannelName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MsgFormat")]
        public String MsgFormat
        {
            get ;
            set ;
        }
}
}
