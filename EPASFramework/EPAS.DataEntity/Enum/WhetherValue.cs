using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //选择是/否 业务值
    public enum WhetherValue
    {
     
        [DescriptionAttribute("zh-CN,是;en-US,Yes;")]
        Yes = 1,
        [DescriptionAttribute("zh-CN,否;en-US,No;")]
        No = 0

     
    }
}
