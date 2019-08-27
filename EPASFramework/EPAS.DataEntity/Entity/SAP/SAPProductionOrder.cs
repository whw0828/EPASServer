using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Entity.MES;

namespace  EPAS.DataEntity.Entity.SAP
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3672
    /// Description:对接SAP中生产订单的实体
    /// 实体有效版本  SAP  Business One 9.2
    /// </summary>
    public class SAPProductionOrder : ProductionOrder
    {
        public SAPProductionOrder()
        {

        }

        /// <summary>
        /// 表名: "SAPMaterialNumber"
        /// </summary>

    

        /// <summary>
        ///P-已计划 R-已下达 L-已结算 已取消
        /// </summary>
        [DisplayName("订单状态")]
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        ///订单开始时间
        /// </summary>
        [DisplayName("订单开始时间")]
        public DateTime ProductionOrderStartDate
        {
            get;
            set;
        }


        



    }
}
