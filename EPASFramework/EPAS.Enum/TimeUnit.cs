using System.ComponentModel;

namespace EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// 时间单位
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>
        /// 秒
        /// </summary>
        [DescriptionAttribute("zh-CN,秒;en-US,Second;")]
        Second = 1,

        /// <summary>
        /// 分
        /// </summary>
        [DescriptionAttribute("zh-CN,分;en-US,Minute;")]
        Minute = 60 * Second,

        /// <summary>
        /// 小时
        /// </summary>
        [DescriptionAttribute("zh-CN,时;en-US,Hour;")]
        Hour = 60 * Minute

    }
}
