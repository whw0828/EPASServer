using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //出入库策略



    public enum IOStrategy
    {

        [DescriptionAttribute("zh-CN,无策略;en-US,NoStrategy;")]
        NoStrategy = 0,
        [DescriptionAttribute("zh-CN,先进先出;en-US,FIFO;")]
        FIFO = 1
        
    }


}
