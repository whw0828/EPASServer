using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3543
    /// Description:员工能力矩阵
    /// </summary>
    public class EmployeeCapabilityMatrix:BaseEntity
    {
        public EmployeeCapabilityMatrix()
        {
        }

        /// <summary>
        /// 表名: "EmployeeCapabilityMatrix"
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EmployeeCapabilityMatrixId")]
        public String EmployeeCapabilityMatrixId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("TrainingBarcode")]
        public String TrainingBarcode
        {
            get ;
            set ;
        }
        /// <summary>
        /// EmployeeNum
        /// </summary>
        [DisplayName("员工工号")]
        public String EmployeeNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// EmployeeName
        /// </summary>
        [DisplayName("员工姓名")]
        public String EmployeeName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialNumberId")]
        public String MaterialNumberId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialNo")]
        public String MaterialNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// MachineId
        /// </summary>
        [DisplayName("机械ID")]
        public String MachineId
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Training")]
        public String Training
        {
            get ;
            set ;
        }
        /// <summary>
        /// TrainingNum
        /// </summary>
        [DisplayName("员工工号")]
        public String TrainingNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// TrainingResult
        /// </summary>
        [DisplayName("是否培训")]
        public int? TrainingResult
        {
            get ;
            set ;
        }
        /// <summary>
        /// TrainingPerson
        /// </summary>
        [DisplayName("培训员")]
        public String TrainingPerson
        {
            get ;
            set ;
        }
        /// <summary>
        /// TrainingDate
        /// </summary>
        [DisplayName("培训时间")]
        public DateTime? TrainingDate
        {
            get ;
            set ;
        }
        /// <summary>
        /// Auditor
        /// </summary>
        [DisplayName("培训员")]
        public String Auditor
        {
            get ;
            set ;
        }
        /// <summary>
        /// AuditorDate
        /// </summary>
        [DisplayName("培训时间")]
        public DateTime? AuditorDate
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("IsAuthorized")]
        public int? IsAuthorized
        {
            get ;
            set ;
        }
}
}
