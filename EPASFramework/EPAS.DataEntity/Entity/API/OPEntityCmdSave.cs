using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.API
{

    public class OPEntityCmdSave : OPEntityCmdBase
    {
        public OPEntityCmdSave()
        {
        }
        /// <summary>
        /// 实体保存检查
        /// </summary>
        public bool SaveCheckResult
        {
            get;
            set;
        }

    }
   
}
