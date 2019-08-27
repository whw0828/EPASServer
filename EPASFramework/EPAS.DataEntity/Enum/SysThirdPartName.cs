using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Enum
{
    /// <summary>
    /// 系统支持的第三方系统名称
    /// </summary>
    public enum SysThirdPartName
    {
        [DescriptionAttribute("zh-CN,钉钉;en-US,DingTalk;")]
        DingTalk = 1,
        [DescriptionAttribute("zh-CN,微信;en-US,Weixin;")]
        Weixin = 2,
    }
}
