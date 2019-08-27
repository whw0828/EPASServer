using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.API
{


    public class OPEntityCmd: OPEntityCmdBase
    { 
        public OPEntityCmd()
        {
        }

        /// <summary>
        /// 实体操作方式
        /// </summary>
        public int optype
        {
            get;
            set;
        }

    }
}
