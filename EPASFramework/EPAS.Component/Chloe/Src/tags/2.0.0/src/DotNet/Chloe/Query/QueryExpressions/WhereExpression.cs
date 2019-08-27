using Chloe.Query.QueryState;
using System;
using System.Linq.Expressions;

namespace Chloe.Query.QueryExpressions
{
    class WhereExpression : QueryExpression
    {
        LambdaExpression _expression;
        public WhereExpression(QueryExpression prevExpression, Type elementType, LambdaExpression predicate)
            : base(QueryExpressionType.Where, elementType, prevExpression)
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
