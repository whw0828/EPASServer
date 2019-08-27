using Chloe.Query.QueryState;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chloe.Query.QueryExpressions
{
    class AggregateQueryExpression : QueryExpression
    {
        MethodInfo _method;
        ReadOnlyCollection<Expression> _parameters;

        public AggregateQueryExpression(QueryExpression prevExpression, MethodInfo method, IList<Expression> parameters)
            : base(QueryExpressionType.Aggregate, method.ReturnType, prevExpression)
        {
            this._method = method;
            this._parameters = new ReadOnlyCollection<Expression>(parameters);
        }

        public MethodInfo Method { get { return this._method; } }
        public ReadOnlyCollection<Expression> Parameters { get { return this._parameters; } }


        public override T Accept<T>(QueryExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
