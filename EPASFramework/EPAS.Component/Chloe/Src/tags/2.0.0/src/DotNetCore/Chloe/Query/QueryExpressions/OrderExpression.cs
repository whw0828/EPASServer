using Chloe.Query.QueryState;
using System;
using System.Linq.Expressions;

namespace Chloe.Query.QueryExpressions
{
    class OrderExpression : QueryExpression
    {
        LambdaExpression _expression;
        public OrderExpression(QueryExpressionType expressionType, Type elementType, QueryExpression prevExpression, LambdaExpression predicate)
            : base(expressionType, elementType, prevExpression)
        {
            this._expression = predicate;
        }
        public LambdaExpression Expression
        {
            get { return this._expression; }
        }

        public override T Accept<T>(QueryExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

}
