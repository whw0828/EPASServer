using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //生产资源（设备）状态



    public enum ProductionResourceStatus
    {

        [DescriptionAttribute("zh-CN,空闲;en-US,Normal;")]
        Default = 0,
        [DescriptionAttribute("zh-CN,生产中;en-US,Working;")]
        Working = 1,
        [DescriptionAttribute("zh-CN,维修;en-US,Repair;")]
        Repair = 2,
        [DescriptionAttribute("zh-CN,保养;en-US,Maintain;")]
        Maintain = 3,
        [DescriptionAttribute("zh-CN,试机;en-US,Test;")]
        Test = 4,

    }


}
