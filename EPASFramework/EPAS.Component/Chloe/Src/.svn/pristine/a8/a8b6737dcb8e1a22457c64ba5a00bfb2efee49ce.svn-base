using System;
using System.Linq.Expressions;

namespace Chloe
{
    public interface IOrderedQuery<T> : IQuery<T>
    {
        IOrderedQuery<T> ThenBy<K>(Expression<Func<T, K>> predicate);
        IOrderedQuery<T> ThenByDesc<K>(Expression<Func<T, K>> predicate);
    }
}
