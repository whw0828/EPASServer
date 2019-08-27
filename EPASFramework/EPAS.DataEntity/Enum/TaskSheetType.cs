using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //工单大类型



    public enum TaskSheetType
    {

        [DescriptionAttribute("zh-CN,工序工单;en-US,PS;")]
        PS = 99,
        [DescriptionAttribute("zh-CN,日工单;en-US,DS")]
        DS = 1,
        [DescriptionAttribute("zh-CN,设备工单;en-US,MS")]
        MS = 2,
        [DescriptionAttribute("zh-CN,人员工单;en-US,ES")]
        ES = 2

    }
    //工单小类型
    public enum TaskSheetSmallType
    {

        [DescriptionAttribute("zh-CN,正常;en-US,Normal;")]
        Normal = 0,
        [DescriptionAttribute("zh-CN,延期;en-US,Delay")]
        Delay = 1,
        [DescriptionAttribute("zh-CN,返工;en-US,Rework")]
        Rework = 2

    }

    //工单业务操作
    public enum TaskSheetOPStatus
    {

        [DescriptionAttribute("zh-CN,未开始;en-US,Not Start;")]
        NotStart = 0,
        [DescriptionAttribute("zh-CN,开始;en-US,Start;")]
        Start = 1,
        [DescriptionAttribute("zh-CN,暂停;en-US,Suspend;")]
        Suspend = 2,
        [DescriptionAttribute("zh-CN,完成;en-US,Finish;")]
        Finish = 3,
        [DescriptionAttribute("zh-CN,切单退料;en-US,Shift;")]
        Shift = 4,
        [DescriptionAttribute("zh-CN,停线退料;en-US,Discontinuation;")]
        Discontinuation =5,
       [DescriptionAttribute("zh-CN,停止;en-US,Stop;")]
        Stop = 6,

    }


}
