using Chloe.DbExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chloe.MySql
{
    class MySqlDbDeleteExpression : DbDeleteExpression
    {
        public MySqlDbDeleteExpression(DbTable table)
          : this(table, null)
        {
        }
        public MySqlDbDeleteExpression(DbTable table, DbExpression condition)
            : base(table, condition)
        {
        }

        public int? Limits { get; set; }
    }
}
