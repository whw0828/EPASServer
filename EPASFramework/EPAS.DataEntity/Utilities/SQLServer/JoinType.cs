using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Utilities.SQLServer
{
   public  enum JoinType
    {
        [DescriptionAttribute("Left Join")]
        LeftJoin = 1,
        [DescriptionAttribute("Right Join")]
        RightJoin = 2,
        [DescriptionAttribute("Inner Join")]
        InnerJoin = 3
    }
}
