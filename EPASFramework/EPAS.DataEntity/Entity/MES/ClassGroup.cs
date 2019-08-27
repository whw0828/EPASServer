using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace EPAS.DataEntity.Entity.MES
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:57:46.3287
    /// Description:
    /// </summary>
    public class ClassGroup:BaseEntity
    {
        public ClassGroup()
        {
        }

        /// <summary>
        /// 表名: "ClassGroup"
        /// </summary>

        /// <summary>
        /// ClassGroupId
        /// </summary>
        [DisplayName("班组ID")]
        public String ClassGroupId
        {
            get ;
            set ;
        }
        /// <summary>
        /// ClassGroupName
        /// </summary>
        [DisplayName("班组名称")]
        public String ClassGroupName
        {
            get ;
            set ;
        }
        /// <summary>
        /// Comments
        /// </summary>
        [DisplayName("备注")]
        public String Comments
        {
            get ;
            set ;
        }
}
}
