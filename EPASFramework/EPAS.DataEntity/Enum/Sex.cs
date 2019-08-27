using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //性别



    public enum Sex
    {

        [DescriptionAttribute("zh-CN,保密;en-US,Secrecy;")]
        Secrecy = 0,
        [DescriptionAttribute("zh-CN,男;en-US,Male;")]
        Male = 1,
        [DescriptionAttribute("zh-CN,女;en-US,Female;")]
        Female = 1

    }


}
