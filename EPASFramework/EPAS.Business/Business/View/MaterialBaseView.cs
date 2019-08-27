using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Business.View
{
    /// <summary>
    /// 物料信息基类
    /// </summary>
   public class MaterialBaseView: OperatorBaseView
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("物料编码")]
        public String MaterialNum
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("物料名称")]
        public String MaterialName
        {
            get;
            set;
        }


        // <summary>
        // 
        // </summary>
        [DisplayName("物料批次")]
        public String BatchNumber
        {
            get;
            set;
        }

        // <summary>
        // 
        // </summary>
        [DisplayName("物料单位")]
        public String UnitCode
        {
            get;
            set;
        }

        // <summary>
        // 
        // </summary>
        [DisplayName("物料数量")]
        public decimal Qty
        {
            get;
            set;
        }
    }
}
