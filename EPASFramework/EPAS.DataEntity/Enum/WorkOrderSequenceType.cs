using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //工序间接序时间类型



    public enum WorkOrderSequenceType
    {

        [DescriptionAttribute("zh-CN,ES;en-US,ES;")]
        ES = 0,
        [DescriptionAttribute("zh-CN,SS;en-US,SS")]
        SS = 1,
        [DescriptionAttribute("zh-CN,SSEE;en-US,SSEE")]
        SSEE = 2

    }


}
