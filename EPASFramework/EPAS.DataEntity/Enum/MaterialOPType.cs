using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Enum
{
    /// <summary>
    /// 物料的操作方式 
    /// </summary>
    public enum MaterialOPType
    {
        [DescriptionAttribute("空操作")]
        NULL = 0,
        [DescriptionAttribute("生产领料")]
        Receive = 1,  
        [DescriptionAttribute("生产投料")]
        Feed = 2, 
        [DescriptionAttribute("生产退料")]
        Return =3,
        [DescriptionAttribute("生产报工")]
        Report = 4,
        [DescriptionAttribute("生产入库")]
        WMI = 5,
        [DescriptionAttribute("生产出库")]
        WMO = 6,
        [DescriptionAttribute("生产取样")]
        QCS = 7,
        [DescriptionAttribute("可疑品申报")]
        DSG = 8
    }
}
