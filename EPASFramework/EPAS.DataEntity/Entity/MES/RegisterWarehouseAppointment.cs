using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3606
    /// Description:生产工单中间品登记
   
   
   
   
   
    /// </summary>
    public class RegisterWarehouseAppointment: BaseEntity
    {
        public RegisterWarehouseAppointment()
        {
        }

        [DisplayName("登记库存使用预约记录ID")]
        public String RegisterWarehouseAppointmentId
        {
            get;
            set;
        }

        [DisplayName("生产订单")]
        public String ProductionOrderNumbe
        {
            get;
            set;
        }

        [DisplayName("生产版本ID")]
        public String ProductionVersionId
        {
            get;
            set;
        }

        [DisplayName("生产版本工序ID")]
        public String ProductionMakeWorkOrderId
        {
            get;
            set;
        }

        [DisplayName("生产版本工序产出品ID")]
        public String WorkOrderOutProductId
        {
            get;
            set;
        }


        /// <summary>
        /// 工序编码
        /// </summary>
        [DisplayName("WorkOrderNum")]
        public String WorkOrderNum
        {
            get;
            set;
        }
        /// <summary>
        /// 工序名称
        /// </summary>
        [DisplayName("WorkOrderName")]
        public String WorkOrderName
        {
            get;
            set;
        }

        /// <summary>
        /// 在制品物料编码
        /// </summary>
        [DisplayName("MaterialNum")]
        public String MaterialNum
        {
            get;
            set;
        }
        /// <summary>
        /// 在制品物料名称
        /// </summary>
        [DisplayName("MaterialName")]
        public String MaterialName
        {
            get;
            set;
        }

        /// <summary>
        /// 工单作业计划数量
        /// </summary>
        [DisplayName("ClassPlanQty")]
        public Decimal ClassPlanQty
        {
            get ;
            set ;
        }

     
        /// <summary>
        /// 库存预约使用数量
        /// </summary>
        [DisplayName("RegisterQty")]
        public Decimal RegisterQty
        {
            get ;
            set ;
        }


        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerNum")]
        public String CustomerNum
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerName")]
        public String CustomerName
        {
            get;
            set;
        }

        /// <summary>
        ///产品单位
        /// </summary>
        [DisplayName("单位")]
        public string UnitCode
        {
            get;
            set;
        }

        /// <summary>
        ///预约人工号
        /// </summary>
        [DisplayName("EmployeeNum")]
        public string EmployeeNum
        {
            get;
            set;
        }

        /// <summary>
        ///预约人姓名
        /// </summary>
        [DisplayName("EmployeeName")]
        public string EmployeeName
        {
            get;
            set;
        }


        /// <summary>
        ///登记时间
        /// </summary>
        [DisplayName("RegisterTime")]
        public DateTime RegisterTime
        {
            get;
            set;
        }

        /// <summary>
        ///状态
        /// </summary>
        [DisplayName("Status")]
        public int Status
        {
            get;
            set;
        }


        /// <summary>
        ///经办人姓名
        /// </summary>
        [DisplayName("OperatorName")]
        public string OperatorName
        {
            get;
            set;
        }

        /// <summary>
        ///经办人工号
        /// </summary>
        [DisplayName("OperatorCode")]
        public string OperatorCode
        {
            get;
            set;
        }


        /// <summary>
        ///操作时间
        /// </summary>
        [DisplayName("OperatorTime")]
        public DateTime? OperatorTime
        {
            get;
            set;
        }

    }
}
