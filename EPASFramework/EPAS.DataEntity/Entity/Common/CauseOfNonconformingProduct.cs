using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3333
    /// Description:理论要求 缺陷原因需要根据物料编码分类，实际实施时 可以不填写物料编码！
  
    /// </summary>
    public class CauseOfNonconformingProduct:BaseEntity
    {
        public CauseOfNonconformingProduct()
        {
        }

        /// <summary>
        /// 表名: "CauseOfNonconformingProduct"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CauseOfNonconformingProductId")]
        public String CauseOfNonconformingProductId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ConpPointNo")]
        public String ConpPointNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ConpPoint")]
        public String ConpPoint
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialNo")]
        public String MaterialNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ConpDes")]
        public String ConpDes
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ConpSolution")]
        public String ConpSolution
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TechnicalStandard")]
        public String TechnicalStandard
        {
            get ;
            set ;
        }
}
}
