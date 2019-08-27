using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4275
    /// Description:该条码 含 工序中间品的条码 和 产品条码（最后一道工序）
   
   
  // 条码类型（1-投料码转报工码  2-  非转序报工码）	
   
  // 1、投料码转报工码  的业务场景：成品的条码以投料的方式进入工序生产
   //2、工序新建报工码的业务场景：工序自己打印的新报工条码
   
   
    /// </summary>
    public class ProductionMakeMaterialBarcode:BaseEntity
    {
        public ProductionMakeMaterialBarcode()
        {
        }

        /// <summary>
        /// 表名: "ProductionMakeMaterialBarcode"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionMakeMaterialBarcodeId")]
        public String ProductionMakeMaterialBarcodeId
        {
            get ;
            set ;
        }
        /// <summary>
        /// ClassWorkPlanId
        /// </summary>
        [DisplayName("生产工单ID")]
        public String ClassWorkPlanId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionBarCode")]
        public String ProductionBarCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialNumberId")]
        public String MaterialNumberId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Qty")]
        public Decimal Qty
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Weight")]
        public Decimal Weight
        {
            get ;
            set ;
        }
        /// <summary>
        /// BatchNumber
        /// </summary>
        [DisplayName("作业票号")]
        public String BatchNumber
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TemplateId")]
        public String TemplateId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionBarCodeType")]
        public int ProductionBarCodeType
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("YetPrintCount")]
        public int YetPrintCount
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("QCProcessFlag")]
        public int QCProcessFlag
        {
            get ;
            set ;
        }
}
}
