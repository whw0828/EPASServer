using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3672
    /// Description:
    /// </summary>
    public class PurchasedMaterialsGuidance : BaseEntity
    {
        public PurchasedMaterialsGuidance()
        {

        }

        /// <summary>
        /// 表名: "PurchasedMaterialsGuidance"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("PurchasedMaterialsGuidanceId")]
        public String PurchasedMaterialsGuidanceId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialNum")]
        public String MaterialNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialName")]
        public String MaterialName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("GraphicNo")]
        public String GraphicNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Type")]
        public String Type
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Format")]
        public String Format
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("DocNum")]
        public String DocNum
        {
            get ;
            set ;
        }
    
    }
}
