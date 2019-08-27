using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4852
    /// Description:该数据是生产 产品跟仓库的数据接口
    /// </summary>
    public class ProductionPackingIntoWarhouse:BaseEntity
    {
        public ProductionPackingIntoWarhouse()
        {
        }

        /// <summary>
        /// 表名: "ProductionPackingIntoWarhouse"
        /// </summary>

        /// <summary>
        /// ProductionPackingIntoWarhouseId
        /// </summary>
        [DisplayName("理货ID")]
        public String ProductionPackingIntoWarhouseId
        {
            get ;
            set ;
        }
        /// <summary>
        /// FinishPackingId
        /// </summary>
        [DisplayName("理货ID")]
        public String FinishPackingId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("BinBarCode")]
        public String BinBarCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Quantity")]
        public Decimal? Quantity
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("WarehouseId")]
        public String WarehouseId
        {
            get ;
            set ;
        }
        /// <summary>
        /// StorageInfoId
        /// </summary>
        [DisplayName("垛位ID")]
        public String StorageInfoId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("InOutState")]
        public int? InOutState
        {
            get ;
            set ;
        }
        /// <summary>
        /// TallyPerson
        /// </summary>
        [DisplayName("审核员")]
        public String TallyPerson
        {
            get ;
            set ;
        }
        /// <summary>
        /// TallyDate
        /// </summary>
        [DisplayName("审核时间")]
        public DateTime TallyDate
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SaleReturnNumber")]
        public String SaleReturnNumber
        {
            get ;
            set ;
        }
}
}
