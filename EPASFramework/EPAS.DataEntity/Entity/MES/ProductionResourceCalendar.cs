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
    public class ProductionResourceCalendar: BaseEntity
    {
        public ProductionResourceCalendar()
        {
        }

        /// <summary>
        /// 表名: "ProductionResourceCalendar"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionResourceCalendarId")]
        public String ProductionResourceCalendarId
        {
            get ;
            set ;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CalendarYear")]
        public int CalendarYear
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ResourceCode")]
        public String ResourceCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ResourceName")]
        public String ResourceName
        {
            get ;
            set ;
        }

        /// <summary>
        /// 正常作业星期一-星期五
        /// </summary>
        [DisplayName("WorkWeekName")]
        public String WorkWeekName
        {
            get;
            set;
        }

        /// <summary>
        /// 正常作业1-5
        /// </summary>
        [DisplayName("WorkWeekValue")]
        public String WorkWeekValue
        {
            get;
            set;
        }

        /// <summary>
        /// 加班日期
        /// </summary>
        [DisplayName("Overtime")]
        public String Overtime
        {
            get;
            set;
        }
        /// <summary>
        /// 节假日期
        /// </summary>
        [DisplayName("NotWorkDates")]
        public String NotWorkDates
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Remark")]
        public String Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ResourceType")]
        public int ResourceType
        {
            get;
            set;
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        [DisplayName("OperationTime")]
        public DateTime OperationTime
        {
            get;
            set;
        }
        //操作人工号
        [DisplayName("EmployeeNum")]
        public String EmployeeNum
        {
            get;
            set;
        }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        [DisplayName("EmployeeName")]
        public String EmployeeName
        {
            get;
            set;
        }

        /// <summary>
        /// AttendanceName
        /// </summary>
        [DisplayName("出勤名称")]
        public String AttendanceName
        {
            get;
            set;
        }
        /// <summary>
        /// Mode
        /// </summary>
        [DisplayName("模式")]
        public String Mode
        {
            get;
            set;
        }
    }
}
