using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //生产资源类型
    public enum ProductionResourcesType
    {
     
        [DescriptionAttribute("zh-CN,设备;en-US,Equipment;")]
        EquipmentResource = 1,
        [DescriptionAttribute("zh-CN,工装;en-US,Technological equipment;")]
        TEResource = 2,
        [DescriptionAttribute("zh-CN,模具;en-US,Model resources;")]
        ModelResources = 3,
        [DescriptionAttribute("zh-CN,外协资源;en-US,External resources;")]
        ExternalResources = 4,
        [DescriptionAttribute("zh-CN,资源组;en-US,Group resources;")]
        GroupResources = 5,
   

    }
}
