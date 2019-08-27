using EPAS.DataEntity.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace EPAS.DataEntity.Enum
{

    //生产过程中使用的物料类型
    public enum MaterialType
    {
      

        [DescriptionAttribute("zh-CN,原材料;en-US,RawMaterial;")]
        RawMaterial = 1,
        [DescriptionAttribute("zh-CN,辅料;en-US,AuxiliaryMaterial;")]
        AuxiliaryMaterial = 2,
        [DescriptionAttribute("zh-CN,包材;en-US,PackingMaterial;")]
        PackingMaterial = 3,
        [DescriptionAttribute("zh-CN,其它;en-US,OtherMaterial;")]
        OtherMaterial = 99,
        [DescriptionAttribute("zh-CN,采购半成品;en-US,CWFMaterial;")]
        CWFMaterial = 20,
        [DescriptionAttribute("zh-CN,采购成品;en-US,CWFPMaterial;")]
        CWFPMaterial = 21,
        [DescriptionAttribute("zh-CN,自制半成品;en-US,SFP;")]
        SFP = 10,
        [DescriptionAttribute("zh-CN,工序中间品;en-US,WFP;")]
        WFP = 11,
        [DescriptionAttribute("zh-CN,成品;en-US,FP;")]
        FP = 12,

    }

}
