using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //质量追溯类型



    public enum QCTraceType
    {

        [DescriptionAttribute("zh-CN,批量;en-US,Batch;")]
        Batch = 0,
        [DescriptionAttribute("zh-CN,产品码;en-US,Unique code;")]
        UniqueCode = 1,
        [DescriptionAttribute("zh-CN,辅码;en-US,Secondary code;")]
        SecondaryCode = 2

    }


}
