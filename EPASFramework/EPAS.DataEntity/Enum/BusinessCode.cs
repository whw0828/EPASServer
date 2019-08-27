using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //业务代码
    public enum BusinessCode
    {
     
        [DescriptionAttribute("zh-CN,系统缺料产出不一致报工!;en-US,Inconsistent input and output!;")]
        FeedOutNoMatch = 1,
        [DescriptionAttribute("zh-CN,多工单共用物料!;en-US,Multiplex Sharing Material!;")]
        MultiplexSharingMaterial = 2,
        [DescriptionAttribute("zh-CN,异常报工条码补打!;en-US,Abnormal BarCode Printing!;")]
        AbnormalBarCodePrinting = 3
     
    }
}
