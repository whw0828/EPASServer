using System;
using System.ComponentModel;

namespace EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// 时间单位
    /// </summary>
    public enum Weeks
    {
     
        /// <summary>
        /// 星期一
        /// </summary>
        [DescriptionAttribute("zh-CN,星期一;en-US,MO;")]
        MO = DayOfWeek.Monday,

        /// <summary>
        /// 星期二
        /// </summary>
        [DescriptionAttribute("zh-CN,星期二;en-US,TU;")]
        TU = DayOfWeek.Tuesday,

        /// <summary>
        /// 星期三
        /// </summary>
        [DescriptionAttribute("zh-CN,星期三;en-US,WE;")]
        WE = DayOfWeek.Wednesday,
        /// <summary>
        /// 星期四
        /// </summary>
        [DescriptionAttribute("zh-CN,星期四;en-US,TH;")]
        TH = DayOfWeek.Thursday,

        /// <summary>
        /// 星期五
        /// </summary>
        [DescriptionAttribute("zh-CN,星期五;en-US,FR;")]
        FR = DayOfWeek.Friday,

        /// <summary>
        /// 星期六
        /// </summary>
        [DescriptionAttribute("zh-CN,星期六;en-US,SA;")]
        SA = DayOfWeek.Saturday,
        /// <summary>
        /// 星期天
        /// </summary>
        [DescriptionAttribute("zh-CN,星期天;en-US,SU;")]
        SU = DayOfWeek.Sunday,

    }
}
