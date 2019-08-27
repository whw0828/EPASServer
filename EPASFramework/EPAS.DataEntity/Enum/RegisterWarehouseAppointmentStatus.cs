using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //生产物料预约状态



    public enum RegisterWarehouseAppointmentStatus
    {
        [DescriptionAttribute("zh-CN,成功;en-US,Success;")]
        Success = 1,
        [DescriptionAttribute("zh-CN,失败;en-US,Fail")]
        Fail = 2,
        [DescriptionAttribute("zh-CN,取消;en-US,Cancel")]
        Cancel = 3

    }


}
