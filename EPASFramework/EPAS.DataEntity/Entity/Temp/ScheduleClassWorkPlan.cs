using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3606
    /// Description:生产进度
   
   
   
   
   
    /// </summary>
    public class ScheduleClassWorkPlan : ClassWorkPlan
    {
        public ScheduleClassWorkPlan()
        {
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
       

    }
}
