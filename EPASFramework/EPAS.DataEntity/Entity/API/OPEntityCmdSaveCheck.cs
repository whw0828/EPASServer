using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.API
{

    /// <summary>
    /// 检查保存数据的编码和名称是否存在相同项
    /// </summary>

    public class OPEntityCmdSaveCheck : OPEntityCmdBase
    {
        public OPEntityCmdSaveCheck()
        {
        }

        /// <summary>
        /// 检查的字段，重复项校验
        /// </summary>
        public List<string> fields
        {
            get;
            set;
        }

    }

}
