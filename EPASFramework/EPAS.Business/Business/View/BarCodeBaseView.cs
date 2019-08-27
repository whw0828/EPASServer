using EPAS.DataEntity.Enum;
using EPAS.DataEntity.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Business.View
{
    /// <summary>
    /// 条码业务基类  
    /// </summary>
   public class BarCodeBaseView: MaterialBaseView
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("条码号")]
        public String BarCode
        {
            get;
            set;
        }

        /// <summary>
        /// 条码依赖的条码
        /// </summary>
        [DisplayName("主条码号")]
        public String MainBarCode
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("工单号")]
        public String WorkPlanNo
        {
            get;
            set;
        }

        [DisplayName("业务操作类型")]
        public MaterialOPType BOPtype
        {
            get;
            set;
        }
        


    }
}
