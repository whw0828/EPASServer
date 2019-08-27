using System.ComponentModel;

namespace EPAS.DataEntity.Entity.Common
{

    //是否有效
    public enum CustomEnable
    {
        [DescriptionAttribute("无效")]
        DisEnable = 0,
        [DescriptionAttribute("有效")]
        Enable = 1
        
    }
}
