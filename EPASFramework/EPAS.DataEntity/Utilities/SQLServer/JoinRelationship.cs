using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Utilities.SQLServer
{

    public class JoinRelationship
    {


        [DisplayName("Join表名称")]
        public string JoinTableName
        {
            get;
            set;
        }
        [DisplayName("Join表别名")]
        public string JoinTableAsName
        {
            get;
            set;
        }
        [DisplayName("Join 条件")]
        public string OnCondition
        {
            get;
            set;
        }

        [DisplayName("Join类型")]
        public JoinType joinType
        {
            get;
            set;
        }


    }
}
