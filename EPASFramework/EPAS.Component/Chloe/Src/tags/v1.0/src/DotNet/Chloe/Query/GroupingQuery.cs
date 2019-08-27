using Chloe.Core;
using Chloe.Query.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Chloe.Query
{
    class GroupingQuery<T> : IGroupingQuery<T>
    {
        Query<T> _fromQuery;
        List<LambdaExpression> _groupPredicates;
        List<LambdaExpression> _havingPredicates;

        public GroupingQuery(Query<T> fromQuery, LambdaExpression predicate)
        {
            this._fromQuery = fromQuery;
            this._groupPredicates = new List<LambdaExpression>(1);
            this._havingPredicates = new List<LambdaExpression>();

            this._groupPredicates.Add(predicate);
        }
        public GroupingQuery(Query<T> fromQuery, List<LambdaExpression> groupPredicates, List<LambdaExpression> havingPredicates)
        {
            this._fromQuery = fromQuery;
            this._groupPredicates = groupPredicates;
            this._havingPredicates = havingPredicates;
        }

        public IGroupingQuery<T> ThenBy<K>(Expression<Func<T, K>> predicate)
        {
            List<LambdaExpression> groupPredicates = new List<LambdaExpression>(this._groupPredicates.Count + 1);
            groupPredicates.AddRange(this._groupPredicates);
            groupPredicates.Add(predicate);

            List<LambdaExpression> havingPredicates = new List<LambdaExpression>(this._havingPredicates.Count);
            havingPredicates.AddRange(this._havingPredicates);

            return new GroupingQuery<T>(this._fromQuery, groupPredicates, havingPredicates);
        }
        public IGroupingQuery<T> Having(Expression<Func<T, bool>> predicate)
        {
            List<LambdaExpression> groupPredicates = new List<LambdaExpression>(this._groupPredicates.Count);
            groupPredicates.AddRange(this._groupPredicates);

            List<LambdaExpression> havingPredicates = new List<LambdaExpression>(this._havingPredicates.Count + 1);
            havingPredicates.AddRange(this._havingPredicates);
            havingPredicates.Add(predicate);

            return new GroupingQuery<T>(this._fromQuery, groupPredicates, havingPredicates);
        }
        public IQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            var e = new GroupingQueryExpression(typeof(TResult), this._fromQuery.QueryExpression, selector);
            e.GroupPredicates.AddRange(this._groupPredicates);
            e.HavingPredicates.AddRange(this._havingPredicates);
            return new Query<TResult>(this._fromQuery.DbContext, e, this._fromQuery._trackEntity);
        }
    }
}
