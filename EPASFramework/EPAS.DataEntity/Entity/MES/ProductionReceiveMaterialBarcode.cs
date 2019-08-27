using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4932
    /// Description:该表中条码是唯一的
    /// </summary>
    public class ProductionReceiveMaterialBarcode:BaseEntity
    {
        public ProductionReceiveMaterialBarcode()
        {
        }

        /// <summary>
        /// 表名: "ProductionReceiveMaterialBarcode"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ProductionReceiveMaterialBarcodeId")]
        public String ProductionReceiveMaterialBarcodeId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("PurchaseOrderLineNum")]
        public String PurchaseOrderLineNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("PurchaseOrderNum")]
        public String PurchaseOrderNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("PalletBarCode")]
        public String PalletBarCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// BarCode
        /// </summary>
        [DisplayName("BarCode")]
        public String BarCode
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
        [DisplayName("MaterieName")]
        public String MaterieName
        {
            get ;
            set ;
        }
        /// <summary>
        /// SupplierCode
        /// </summary>
        [DisplayName("(SupplierCode)")]
        public String SupplierCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// SupplierName
        /// </summary>
        [DisplayName("(SupplierName)")]
        public String SupplierName
        {
            get ;
            set ;
        }
        /// <summary>
        /// BatchNumber
        /// </summary>
        [DisplayName("生产批次")]
        public String BatchNumber
        {
            get ;
            set ;
        }
        /// <summary>
        /// Qty
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
        [DisplayName("UnitCode")]
        public String UnitCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BarcodeType")]
        public int? BarcodeType
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Comments")]
        public String Comments
        {
            get ;
            set ;
        }
}
}
