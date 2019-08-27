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
    public class BusinessNotificationRecord : BaseEntity
    {
        public BusinessNotificationRecord()
        {
        }

        /// <summary>
        /// 表名: "BusinessNotificationRecord"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BusinessNotificationRecordId")]
        public String BusinessNotificationRecordId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BusinessNotificationRecordNo")]
        public String BusinessNotificationRecordNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SendEmployeeNum")]
        public String SendEmployeeNum
        {
            get ;
            set ;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SendEmployeeName")]
        public String SendEmployeeName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RecvEmployeeNum")]
        public String RecvEmployeeNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RecvEmployeeName")]
        public String RecvEmployeeName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("NoticeTitle")]
        public String NoticeTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("NoticeContent")]
        public String NoticeContent
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("NoticeTime")]
        public DateTime NoticeTime
        {
            get;
            set;
        }

        /// <summary>
        /// 附件
        /// </summary>
        [DisplayName("Attachment")]
        public Byte[] Attachment
        {
            get;
            set;
        }
    }
}
