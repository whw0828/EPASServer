using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.API
{

    public class OPEntityCmdField : OPEntityCmdBase
    {
        public OPEntityCmdField()
        {
        }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string fieldName
        {
            get;
            set;
        }

        /// <summary>
        /// 字段值
        /// </summary>
        public string fieldValue
        {
            get;
            set;
        }

    }
   
}
