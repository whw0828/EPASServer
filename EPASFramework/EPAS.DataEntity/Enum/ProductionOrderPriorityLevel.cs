using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //生产订单优先级
    //90（含90）以上为紧急订单



    public enum ProductionOrderPriorityLevel
    {

        [DescriptionAttribute("zh-CN,默认;en-US,Default;")]
        Default = 80,
        [DescriptionAttribute("zh-CN,紧急订单;en-US,Emergency Orders;")]
        EmergencyOrders = 90,

    }


}
