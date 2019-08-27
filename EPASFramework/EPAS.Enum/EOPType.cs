using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// 实体类的操作方式 
    /// </summary>
    public enum EOPType
    {
        [DescriptionAttribute("空操作")]
        NULL = 0,
        [DescriptionAttribute("插入")]
        Insert = 1,  
        [DescriptionAttribute("更新")]
        Update = 2, 
        [DescriptionAttribute("删除")]
        Delete =3, 
    }
}
