using EPAS.DataEntity.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Entity.Common
{
    public class BaseEntity
    {
        /// <summary>
        /// 录入人工号
        /// </summary>
        [DisplayName("CEmployeeNum")]
        public String CEmployeeNum
        {
            get;
            set;
        }
        /// <summary>
        /// 录入人姓名
        /// </summary>
        [DisplayName("CEmployeeName")]
        public String CEmployeeName
        {
            get;
            set;
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        [DisplayName("CreateTime")]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 修改人工号
        /// </summary>
        [DisplayName("UEmployeeNum")]
        public String UEmployeeNum
        {
            get;
            set;
        }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        [DisplayName("UEmployeeName")]
        public String UEmployeeName
        {
            get;
            set;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        [DisplayName("UpdateTime")]
        public DateTime? UpdateTime
        {
            get;
            set;
        }
        //    /// <summary>
        //    /// 创建用户
        //    /// </summary>
        //    [DisplayName("创建用户")]
        //    public string CUserCode
        //    {
        //        get;
        //        set;
        //    }
        //    /// <summary>
        //    /// 创建时间
        //    /// </summary>
        //    [DisplayName("创建时间")]
        //    public DateTime? CDate
        //    {
        //        get;
        //        set;
        //    }
        //    /// <summary>
        //    /// 修改用户
        //    /// </summary>
        //    [DisplayName("修改用户")]
        //    public string MUserCode
        //    {
        //        get;
        //        set;
        //    }
        //    /// <summary>
        //    /// 修改时间 
        //    /// </summary>
        //    [DisplayName("修改时间")]
        //    public DateTime? MDate
        //    {
        //        get;
        //        set;
        //    }
    }
}
