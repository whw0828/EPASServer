using Chloe.DbExpressions;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chloe.Query
{
    public class ResultElement
    {
        public ResultElement()
        {
            this.Orderings = new List<DbOrdering>();
            this.GroupSegments = new List<DbExpression>();
        }

        public IMappingObjectExpression MappingObjectExpression { get; set; }

        public bool OrderingsComeFromSubQuery { get; set; }

        public List<DbOrdering> Orderings { get; private set; }
        public List<DbExpression> GroupSegments { get; private set; }

        /// <summary>
        /// 如 takequery 了以后，则 table 的 Expression 类似 (select T.Id.. from User as T),Alias 则为新生成的
        /// </summary>
        public DbFromTableExpression FromTable { get; set; }
        public DbExpression Condition { get; set; }
        public DbExpression HavingCondition { get; set; }

        public void AppendCondition(DbExpression condition)
        {
            if (this.Condition == null)
                this.Condition = condition;
            else
                this.Condition = new DbAndAlsoExpression(this.Condition, condition);
        }
        public void AppendHavingCondition(DbExpression condition)
        {
            if (this.HavingCondition == null)
                this.HavingCondition = condition;
            else
                this.HavingCondition = new DbAndAlsoExpression(this.HavingCondition, condition);
        }


        public string GenerateUniqueTableAlias(string prefix = UtilConstants.DefaultTableAlias)
        {
            if (this.FromTable == null)
                return prefix;

            string alias = prefix;
            int i = 0;
            DbFromTableExpression fromTable = this.FromTable;
            while (ExistTableAlias(fromTable, alias))
            {
                alias = prefix + i.ToString();
                i++;
            }

            return alias;
        }

        static bool ExistTableAlias(DbFromTableExpression fromTable, string alias)
        {
            if (string.Equals(fromTable.Table.Alias, alias, StringComparison.OrdinalIgnoreCase))
                return true;

            foreach (var item in fromTable.JoinTables)
            {
                if (ExistTableAlias(item, alias))
                    return true;
            }

            return false;
        }
        static bool ExistTableAlias(DbJoinTableExpression joinTable, string alias)
        {
            if (string.Equals(joinTable.Table.Alias, alias, StringComparison.OrdinalIgnoreCase))
                return true;

            foreach (var item in joinTable.JoinTables)
            {
                if (ExistTableAlias(item, alias))
                    return true;
            }

            return false;
        }
    }
}
