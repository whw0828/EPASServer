using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.SAP
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3672
    /// Description:对接SAP中物料主数据的实体
    /// 实体有效版本  SAP  Business One 9.2
    /// </summary>
    public class SAPMaterialNumber:MaterialNumber
    {
        public SAPMaterialNumber()
        {

        }

        /// <summary>
        /// 表名: "SAPMaterialNumber"
        /// </summary>

        /// <summary>
        ///Y-有效 N-无效
        /// </summary>
        [DisplayName("可用")]
        public string validFor
        {
            get;
            set;
        }

        /// <summary>
        /// Y-有效 N-无效
        /// </summary>
        [DisplayName("不可用")]
        public string frozenFor
        {
            get;
            set;
        }

        /// <summary>
        /// 生产-M 采购-B
        /// </summary>
        [DisplayName("生产/采购")]
        public int PrcrmntMtd
        {
            get;
            set;
        }
    }
}
