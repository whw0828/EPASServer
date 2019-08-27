
namespace Chloe.DbExpressions
{
    public class DbOrdering
    {
        OrderType _orderType;
        DbExpression _expression;
        public DbOrdering(DbExpression expression, OrderType orderType)
        {
            this._expression = expression;
            this._orderType = orderType;
        }
        public DbExpression Expression { get { return this._expression; } }
        public OrderType OrderType { get { return this._orderType; } }
    }
}
