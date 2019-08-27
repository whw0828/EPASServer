using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //锁定状态



    public enum MaterialStatus
    {
        [DescriptionAttribute("zh-CN,可用;en-US,enable;")]
        Enable = 0,
        [DescriptionAttribute("zh-CN,不可用;en-US,Disenable;")]
        DisEnable = 1,
        [DescriptionAttribute("zh-CN,高级;en-US,Senior;")]
        Senior = 2

    }


}
