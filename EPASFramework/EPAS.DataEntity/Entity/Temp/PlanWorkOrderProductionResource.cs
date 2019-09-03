using EPAS.DataEntity.Entity.MES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Entity.Temp
{
    public class PlanWorkOrderProductionResource: WorkOrderProductionResource
    {

        /// <summary>
        /// 设备状态
        /// </summary>
        [DisplayName("ProductionResourceStatus")]
        public int ProductionResourceStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 设备作业数量
        /// </summary>
        [DisplayName("Qty")]
        public decimal Qty
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("Comment")]
        public String Comment
        {
            get;
            set;
        }
    }
}
