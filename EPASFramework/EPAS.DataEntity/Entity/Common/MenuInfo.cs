using System;
using System.ComponentModel;
using EPAS.DataEntity.Entity.Common;
namespace  EPAS.DataEntity.Entity.Common
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-03-28 09:27:59.3732
    /// Description:菜单信息
    /// </summary>
    public class MenuInfo:BaseEntity
    {
        public MenuInfo()
        {
        }

        /// <summary>
        /// 表名: "MenuInfo"
        /// </summary>

        /// <summary>
        /// MenuInfoId
        /// </summary>
        [DisplayName("菜单ID")]
        public string MenuInfoId
        {
            get ;
            set ;
        }
        /// <summary>
        /// ParentId
        /// </summary>
        [DisplayName("父节点ID")]
        public string ParentId
        {
            get ;
            set ;
        }
        /// <summary>
        /// MenuName
        /// </summary>
        [DisplayName("菜单名")]
        public string MenuName
        {
            get ;
            set ;
        }

        /// <summary>
        /// MenuPath
        /// </summary>
        [DisplayName("菜单路径")]
        public string MenuPath
        {
            get ;
            set ;
        }
        /// <summary>
        /// OrderNo
        /// </summary>
        [DisplayName("顺序号")]
        public int OrderNo
        {
            get ;
            set ;
        }
        /// <summary>
        /// FunctionType
        /// </summary>
        [DisplayName("菜单类型")]
        public int? FunctionType
        {
            get;
            set;
        }
        /// <summary>
        /// IsEnable
        /// </summary>
        [DisplayName("是否可用")]
        public int? IsEnable
        {
            get ;
            set ;
        }

        /// <summary>
        /// Comments
        /// </summary>
        [DisplayName("描述")]
        public string Comments
        {
            get ;
            set ;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ImageData")]
        public Byte[] ImageData
        {
            get;
            set;
        }

    }
}
