using System.Collections.Generic;

namespace Chloe.DbExpressions
{
    public class DbJoinTableExpression : DbExpression
    {
        JoinType _joinType;
        DbTableSegment _table;
        DbExpression _condition;
        List<DbJoinTableExpression> _joinTables;
        public DbJoinTableExpression(JoinType joinType, DbTableSegment table, DbExpression condition)
            : base(DbExpressionType.JoinTable)
        {
            this._joinType = joinType;
            this._table = table;
            this._condition = condition;
            this._joinTables = new List<DbJoinTableExpression>();
        }

        public DbTableSegment Table { get { return this._table; } }
        public JoinType JoinType { get { return this._joinType; } }
        public DbExpression Condition { get { return this._condition; } }
        public List<DbJoinTableExpression> JoinTables { get { return this._joinTables; } }
        public override T Accept<T>(DbExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
