using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.4364
    /// Description:生产版本中工序视图
    /// </summary>
    public class ProductionMakeWorkOrderView: ProductionMakeWorkOrder
    {
        public ProductionMakeWorkOrderView()
        {
        }

        /// <summary>
        /// 表名: "ProductionMakeWorkOrderView"
        /// </summary>

        /// <summary>
        /// 工序编码
        /// </summary>
        [DisplayName("WorkOrderNum")]
        public String WorkOrderNum
        {
            get ;
            set ;
        }

 


        /// <summary>
        /// 工序名称
        /// </summary>
        [DisplayName("WorkOrderName")]
        public String WorkOrderName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 版本名称
        /// </summary>
        [DisplayName("ProductionVersionName")]
        public String ProductionVersionName
        {
            get ;
            set ;
        }

    }
}
