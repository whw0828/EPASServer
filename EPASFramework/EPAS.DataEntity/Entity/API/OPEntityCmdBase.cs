using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.API
{


    public class OPEntityCmdBase
    { 
        public OPEntityCmdBase()
        {
        }

        public void InitCmd<T>()
        {
            Type type = typeof(T);

            this.Namespace = type.Namespace;
            this.EntityTypeName = type.Name;
        }

        public String EntityJson
        {
            get;
            set;
        }

        public String Tag
        {
            get;
            set;
        }
        public String timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// 操作员工号
        /// </summary>
        public String CEmployeeNum
        {
            get;
            set;
        }
        /// <summary>
        /// 操作姓名
        /// </summary>
        public String CEmployeeName
        {
            get;
            set;
        }

        #region Namespace 和 EntityTypeName 是连动变量 即二者都为空或二者都不为空
        /// <summary>
        /// 命名空间名称
        /// </summary>
        public String Namespace
        {
            get;
            set;
        }

        /// <summary>
        /// 实体名称
        /// </summary>
        public String EntityTypeName
        {
            get;
            set;
        }
        #endregion


    }
}
