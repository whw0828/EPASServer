using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Business.View
{
    /// <summary>
    /// 业务操作员基类
    /// </summary>
   public class OperatorBaseView
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("操作员工号")]
        public String OperatorCode
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("操作员姓名")]
        public String OperatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("操作时间")]
        public DateTime OperatorTime
        {
            get;
            set;
        }
     
    }
}
