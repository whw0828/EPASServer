using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.2798
    /// Description:
    /// </summary>
    public class AttendanceInfo:BaseEntity
    {
        public AttendanceInfo()
        {
        }

        /// <summary>
        /// 表名: "AttendanceInfo"
        /// </summary>

        /// <summary>
        /// AttendanceInfoId
        /// </summary>
        [DisplayName("出勤模式ID")]
        public String AttendanceInfoId
        {
            get ;
            set ;
        }
        /// <summary>
        /// AttendanceName
        /// </summary>
        [DisplayName("出勤名称")]
        public String AttendanceName
        {
            get ;
            set ;
        }
        /// <summary>
        /// Mode
        /// </summary>
        [DisplayName("模式")]
        public String Mode
        {
            get ;
            set ;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("备注")]
        public String Remark
        {
            get ;
            set ;
        }
}
}
