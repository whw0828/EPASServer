using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //质量在工单上的最新处理标记

    public enum QCProcessFlag
    {

        [DescriptionAttribute("zh-CN,未检验;en-US,Untested;")]
        Untested = 0,
        [DescriptionAttribute("zh-CN,合格;en-US,Qualified;")]
        Qualified = 1,
        [DescriptionAttribute("zh-CN,不合格;en-US,Unqualified;")]
        Unqualified = 2,
        [DescriptionAttribute("zh-CN,返工;en-US,Rework;")]
        Rework = 3,
        [DescriptionAttribute("zh-CN,返修;en-US,Repair;")]
        Repair = 4,
        [DescriptionAttribute("zh-CN,让步;en-US,Compromise;")]
        Compromise = 5,
        [DescriptionAttribute("zh-CN,报废;en-US,Scrap;")]
        Scrap = 6,
    }

    /// <summary>
    /// 工序质检方式
    /// </summary>
    public enum QCProcessType
    {

        [DescriptionAttribute("zh-CN,抽检;en-US,SpotChecks;")]
        SpotChecks = 0,
        [DescriptionAttribute("zh-CN,全检;en-US,All checks;")]
        AllChecks = 1,
        [DescriptionAttribute("zh-CN,免检;en-US,Exemption;")]
        Exemption = 2,
    }
}


