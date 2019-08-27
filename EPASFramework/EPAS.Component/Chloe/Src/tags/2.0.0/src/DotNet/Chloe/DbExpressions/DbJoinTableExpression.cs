using System.Collections.Generic;

namespace Chloe.DbExpressions
{
    public class DbJoinTableExpression : DbMainTableExpression
    {
        JoinType _joinType;
        DbExpression _condition;
        public DbJoinTableExpression(JoinType joinType, DbTableSegment table, DbExpression condition)
            : base(DbExpressionType.JoinTable, table)
        {
            this._joinType = joinType;
            this._condition = condition;
        }

        public JoinType JoinType { get { return this._joinType; } }
        public DbExpression Condition { get { return this._condition; } }
        public override T Accept<T>(DbExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
