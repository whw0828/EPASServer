using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3672
    /// Description:
    /// </summary>
    public class MaterialNumber:BaseEntity
    {
        public MaterialNumber()
        {

        }

        /// <summary>
        /// 表名: "MaterialNumber"
        /// </summary>

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
        [DisplayName("MaterialNum")]
        public String MaterialNum
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialName")]
        public String MaterialName
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("GraphicNo")]
        public String GraphicNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Type")]
        public String Type
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Format")]
        public String Format
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("UnitCode")]
        public String UnitCode
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("IOStrategy")]
        public int IOStrategy
        {
            get ;
            set ;
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("MaterialType")]
        public int MaterialType
        {
            get ;
            set ;
        }

        [DisplayName("物料状态")]
        public int MaterialStatus
        {
            get;
            set;
        }


        [DisplayName("活跃开始时间")]
        public DateTime? ValidFrom
        {
            get;
            set;
        }

        [DisplayName("活跃结束时间")]
        public DateTime? ValidTo
        {
            get;
            set;
        }


        [DisplayName("不活跃开始时间")]
        public DateTime? FrozenFrom
        {
            get;
            set;
        }

        [DisplayName("不活跃结束时间")]
        public DateTime? FrozenTo
        {
            get;
            set;
        }
    }
}
