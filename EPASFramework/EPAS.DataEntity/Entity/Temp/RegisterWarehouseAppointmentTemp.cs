using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.5192
    /// Description:中间计算用
    /// </summary>
    public class RegisterWarehouseAppointmentTemp : RegisterWarehouseAppointment
    {
        public RegisterWarehouseAppointmentTemp()
        {
        }
        /// <summary>
        /// 工序在制品已预约使用库存合计量
        /// </summary>
        [DisplayName("YetRegisterQty")]
        public decimal YetRegisterQty
        {
            get ;
            set ;
        }
    }
}
