using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// 数据插件的名称
    /// </summary>
    public enum DataPluginName
    {
        [DescriptionAttribute("EPAS.DirectSQLPlugin")]
        DefaultDataPlugin = 1,  
        [DescriptionAttribute("EPAS.ChloePlugin")]
        ChloeDataPlugin = 2, 

    }
}
