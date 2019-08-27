using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Utilities.SQLServer
{


    public class MSSQL
    {

        [DisplayName("主表名称")]
        public  String rootTable
        {
            get;
            set;
        }

        /// <summary>
        /// 例如 "DISTINCT","Top(100)"等
        /// </summary>
        [DisplayName("headCmd")]
        public String headCmd
        {
            get;
            set;
        }
        [DisplayName("视图显示的字段")]
        public String displayItem
        {
            get;
            set;
        }

        [DisplayName("Where 条件")]
        public string whereSql
        {
            get;
            set;
        }


        
    }
}
