using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3606
    /// Description:生产日工单
   
   
   
   
   
    /// </summary>
    public class DayClassWorkPlan: ClassWorkPlan
    {
        public DayClassWorkPlan()
        {
        }

        /// <summary>
        /// 第一天
        /// </summary>
        [DisplayName("FirstDay")]
        public DateTime FirstDay
        {
            get;
            set;
        }
        /// <summary>
        /// 第二天
        /// </summary>
        [DisplayName("SecondDay")]
        public DateTime SecondDay

        {
            get;
            set;
        }

        /// <summary>
        /// 第三天
        /// </summary>
        [DisplayName("ThirdDay")]
        public DateTime ThirdDay
        {
            get;
            set;
        }

        /// <summary>
        /// 工序工单欠数
        /// </summary>
        [DisplayName("OweQty")]
        public decimal OweQty
        {
            get;
            set;
        }
        /// <summary>
        /// 第一天计划作业量
        /// </summary>
        [DisplayName("FirstQty")]
        public Decimal FirstQty
        {
            get ;
            set ;
        }

        /// <summary>
        /// 第二天计划作业量
        /// </summary>
        [DisplayName("SecondQty")]
        public Decimal SecondQty
        {
            get;
            set;
        }

        /// <summary>
        /// 第三天计划作业量
        /// </summary>
        [DisplayName("ThirdQty")]
        public Decimal ThirdQty
        {
            get;
            set;
        }

    }
}
