using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //生产订单状态



    public enum ProductionOrderStatus
    {

        [DescriptionAttribute("zh-CN,新订单;en-US,New Order;")]
        Default = 0,
        [DescriptionAttribute("zh-CN,发布;en-US,Release")]
        Release = 1,
        [DescriptionAttribute("zh-CN,完成;en-US,Complete")]
        Complete = 2,
        [DescriptionAttribute("zh-CN,停止;en-US,Stop")]
        Stop = 4,

    }


}
