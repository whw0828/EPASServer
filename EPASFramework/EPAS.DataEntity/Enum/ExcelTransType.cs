using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //换算类型
    public enum ExcelTransType
    {
        [DescriptionAttribute("无换算")]
        NullTrans = -1,
        [DescriptionAttribute("枚举换算")]
        EnumTrans = 0,
        [DescriptionAttribute("实体字段")]
        EntityTrans = 1
        
    }
}
