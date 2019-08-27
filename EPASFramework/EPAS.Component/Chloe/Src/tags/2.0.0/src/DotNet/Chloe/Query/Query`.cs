using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Chloe.Core;
using Chloe.Query.QueryExpressions;
using Chloe.Infrastructure;
using Chloe.Query.Internals;
using System.Diagnostics;
using Chloe.Utility;
using System.Reflection;
using Chloe.DbExpressions;

namespace Chloe.Query
{
    class Query<T> : QueryBase, IQuery<T>
    {
        static readonly List<Expression> EmptyParameterList = new List<Expression>(0);

        DbContext _dbContext;
        QueryExpression _expression;

        internal bool _trackEntity = false;
        public DbContext DbContext { get { return this._dbContext; } }

        public Query(DbContext dbContext)
            : this(dbContext, new RootQueryExpression(typeof(T)), false)
        {

        }
        public Query(DbContext dbContext, QueryExpression exp)
            : this(dbContext, exp, false)
        {
        }
        public Query(DbContext dbContext, QueryExpression exp, bool trackEntity)
        {
            this._dbContext = dbContext;
            this._expression = exp;
            this._trackEntity = trackEntity;
        }

        public IQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            Utils.CheckNull(selector);
            SelectExpression e = new SelectExpression(typeof(TResult), _expression, selector);
            return new Query<TResult>(this._dbContext, e, this._trackEntity);
        }

        public IQuery<T> Where(Expression<Func<T, bool>> predicate)
        {
            Utils.CheckNull(predicate);
            WhereExpression e = new WhereExpression(_expression, typeof(T), predicate);
            return new Query<T>(this._dbContext, e, this._trackEntity);
        }
        public IOrderedQuery<T> OrderBy<K>(Expression<Func<T, K>> predicate)
        {
            Utils.CheckNull(predicate);
            OrderExpression e = new OrderExpression(QueryExpressionType.OrderBy, typeof(T), this._expression, predicate);
            return new OrderedQuery<T>(this._dbContext, e, this._trackEntity);
        }
        public IOrderedQuery<T> OrderByDesc<K>(Expression<Func<T, K>> predicate)
        {
            Utils.CheckNull(predicate);
            OrderExpression e = new OrderExpression(QueryExpressionType.OrderByDesc, typeof(T), this._expression, predicate);
            return new OrderedQuery<T>(this._dbContext, e, this._trackEntity);
        }
        public IQuery<T> Skip(int count)
        {
            SkipExpression e = new SkipExpression(typeof(T), this._expression, count);
            return new Query<T>(this._dbContext, e, this._trackEntity);
        }
        public IQuery<T> Take(int count)
        {
            TakeExpression e = new TakeExpression(typeof(T), this._expression, count);
            return new Query<T>(this._dbContext, e, this._trackEntity);
        }

        public IGroupingQuery<T> GroupBy<K>(Expression<Func<T, K>> predicate)
        {
            Utils.CheckNull(predicate);
            return new GroupingQuery<T>(this, predicate);
        }

        public IJoiningQuery<T, TSource> InnerJoin<TSource>(IQuery<TSource> q, Expression<Func<T, TSource, bool>> on)
        {
            Utils.CheckNull(q);
            Utils.CheckNull(on);
            return new JoiningQuery<T, TSource>(this, (Query<TSource>)q, JoinType.InnerJoin, on);
        }
        public IJoiningQuery<T, TSource> LeftJoin<TSource>(IQuery<TSource> q, Expression<Func<T, TSource, bool>> on)
        {
            Utils.CheckNull(q);
            Utils.CheckNull(on);
            return new JoiningQuery<T, TSource>(this, (Query<TSource>)q, JoinType.LeftJoin, on);
        }
        public IJoiningQuery<T, TSource> RightJoin<TSource>(IQuery<TSource> q, Expression<Func<T, TSource, bool>> on)
        {
            Utils.CheckNull(q);
            Utils.CheckNull(on);
            return new JoiningQuery<T, TSource>(this, (Query<TSource>)q, JoinType.RightJoin, on);
        }
        public IJoiningQuery<T, TSource> FullJoin<TSource>(IQuery<TSource> q, Expression<Func<T, TSource, bool>> on)
        {
            Utils.CheckNull(q);
            Utils.CheckNull(on);
            return new JoiningQuery<T, TSource>(this, (Query<TSource>)q, JoinType.FullJoin, on);
        }

        public T First()
        {
            var q = (Query<T>)this.Take(1);
            IEnumerable<T> iterator = q.GenerateIterator();
            return iterator.First();
        }
        public T First(Expression<Func<T, bool>> predicate)
        {
            return this.Where(predicate).First();
        }
        public T FirstOrDefault()
        {
            var q = (Query<T>)this.Take(1);
            IEnumerable<T> iterator = q.GenerateIterator();
            return iterator.FirstOrDefault();
        }
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this.Where(predicate).FirstOrDefault();
        }
        public List<T> ToList()
        {
            IEnumerable<T> iterator = this.GenerateIterator();
            return iterator.ToList();
        }

        public bool Any()
        {
            var q = (Query<string>)this.Select(a => "1").Take(1);
            return q.GenerateIterator().Any();
        }
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return this.Where(predicate).Any();
        }

        public int Count()
        {
            return this.ExecuteAggregateQuery<int>(GetCalledMethod(() => default(IQuery<T>).Count()), null, false);
        }
        public long LongCount()
        {
            return this.ExecuteAggregateQuery<long>(GetCalledMethod(() => default(IQuery<T>).LongCount()), null, false);
        }

        public TResult Max<TResult>(Expression<Func<T, TResult>> selector)
        {
            return this.ExecuteAggregateQuery<TResult>(GetCalledMethod(() => default(IQuery<T>).Max(default(Expression<Func<T, TResult>>))), selector);
        }
        public TResult Min<TResult>(Expression<Func<T, TResult>> selector)
        {
            return this.ExecuteAggregateQuery<TResult>(GetCalledMethod(() => default(IQuery<T>).Min(default(Expression<Func<T, TResult>>))), selector);
        }

        public int Sum(Expression<Func<T, int>> selector)
        {
            return this.ExecuteAggregateQuery<int>(GetCalledMethod(() => default(IQuery<T>).Sum(default(Expression<Func<T, int>>))), selector);
        }
        public int? Sum(Expression<Func<T, int?>> selector)
        {
            return this.ExecuteAggregateQuery<int?>(GetCalledMethod(() => default(IQuery<T>).Sum(default(Expression<Func<T, int?>>))), selector);
        }
        public long Sum(Expression<Func<T, long>> selector)
        {
            return this.ExecuteAggregateQuery<long>(GetCalledMethod(() => default(IQuery<T>).Sum(default(Expression<Func<T, long>>))), selector);
        }
        public long? Sum(Expression<Func<T, long?>> selector)
        {
            return this.ExecuteAggregateQuery<long?>(GetCalledMethod(() => default(IQuery<T>).Sum(default(Expression<Func<T, long?>>))), selector);
        }
        public decimal Sum(Expression<Func<T, decimal>> selector)
        {
            return this.ExecuteAggregateQuery<decimal>(GetCalledMethod(() => default(IQuery<T>).Sum(default(Expression<Func<T, decimal>>))), selector);
        }
        public decimal? Sum(Expression<Func<T, decimal?>> selector)
        {
            return this.ExecuteAggregateQuery<decimal?>(GetCalledMethod(() => default(IQuery<T>).Sum(default(Expression<Func<T, decimal?>>))), selector);
        }
        public double Sum(Expression<Func<T, double>> selector)
        {
            return this.ExecuteAggregateQuery<double>(GetCalledMethod(() => default(IQuery<T>).Sum(default(Expression<Func<T, double>>))), selector);
        }
        public double? Sum(Expression<Func<T, double?>> selector)
        {
            return this.ExecuteAggregateQuery<double?>(GetCalledMethod(() => default(IQuery<T>).Sum(default(Expression<Func<T, double?>>))), selector);
        }
        public float Sum(Expression<Func<T, float>> selector)
        {
            return this.ExecuteAggregateQuery<float>(GetCalledMethod(() => default(IQuery<T>).Sum(default(Expression<Func<T, float>>))), selector);
        }
        public float? Sum(Expression<Func<T, float?>> selector)
        {
            return this.ExecuteAggregateQuery<float?>(GetCalledMethod(() => default(IQuery<T>).Sum(default(Expression<Func<T, float?>>))), selector);
        }

        public double Average(Expression<Func<T, int>> selector)
        {
            return this.ExecuteAggregateQuery<double>(GetCalledMethod(() => default(IQuery<T>).Average(default(Expression<Func<T, int>>))), selector);
        }
        public double? Average(Expression<Func<T, int?>> selector)
        {
            return this.ExecuteAggregateQuery<double>(GetCalledMethod(() => default(IQuery<T>).Average(default(Expression<Func<T, int?>>))), selector);
        }
        public double Average(Expression<Func<T, long>> selector)
        {
            return this.ExecuteAggregateQuery<double>(GetCalledMethod(() => default(IQuery<T>).Average(default(Expression<Func<T, long>>))), selector);
        }
        public double? Average(Expression<Func<T, long?>> selector)
        {
            return this.ExecuteAggregateQuery<double?>(GetCalledMethod(() => default(IQuery<T>).Average(default(Expression<Func<T, long?>>))), selector);
        }
        public decimal Average(Expression<Func<T, decimal>> selector)
        {
            return this.ExecuteAggregateQuery<decimal>(GetCalledMethod(() => default(IQuery<T>).Average(default(Expression<Func<T, decimal>>))), selector);
        }
        public decimal? Average(Expression<Func<T, decimal?>> selector)
        {
            return this.ExecuteAggregateQuery<decimal?>(GetCalledMethod(() => default(IQuery<T>).Average(default(Expression<Func<T, decimal?>>))), selector);
        }
        public double Average(Expression<Func<T, double>> selector)
        {
            return this.ExecuteAggregateQuery<double>(GetCalledMethod(() => default(IQuery<T>).Average(default(Expression<Func<T, double>>))), selector);
        }
        public double? Average(Expression<Func<T, double?>> selector)
        {
            return this.ExecuteAggregateQuery<double?>(GetCalledMethod(() => default(IQuery<T>).Average(default(Expression<Func<T, double?>>))), selector);
        }
        public float Average(Expression<Func<T, float>> selector)
        {
            return this.ExecuteAggregateQuery<float>(GetCalledMethod(() => default(IQuery<T>).Average(default(Expression<Func<T, float>>))), selector);
        }
        public float? Average(Expression<Func<T, float?>> selector)
        {
            return this.ExecuteAggregateQuery<float?>(GetCalledMethod(() => default(IQuery<T>).Average(default(Expression<Func<T, float?>>))), selector);
        }

        public override QueryExpression QueryExpression { get { return this._expression; } }
        public override bool TrackEntity { get { return this._trackEntity; } }

        public IQuery<T> AsTracking()
        {
            return new Query<T>(this._dbContext, this.QueryExpression, true);
        }
        public IEnumerable<T> AsEnumerable()
        {
            return this.GenerateIterator();
        }

        InternalQuery<T> GenerateIterator()
        {
            InternalQuery<T> internalQuery = new InternalQuery<T>(this);
            return internalQuery;
        }


        TResult ExecuteAggregateQuery<TResult>(MethodInfo method, Expression parameter, bool checkParameter = true)
        {
            if (checkParameter)
                Utils.CheckNull(parameter);

            List<Expression> parameters = parameter == null ? EmptyParameterList : new List<Expression>(1) { parameter };

            IEnumerable<TResult> iterator = this.CreateAggregateQuery<TResult>(method, parameters);
            return iterator.Single();
        }
        InternalQuery<TResult> CreateAggregateQuery<TResult>(MethodInfo method, List<Expression> parameters)
        {
            AggregateQueryExpression e = new AggregateQueryExpression(this._expression, method, parameters);
            var q = new Query<TResult>(this._dbContext, e, false);
            InternalQuery<TResult> iterator = q.GenerateIterator();
            return iterator;
        }
        MethodInfo GetCalledMethod<TResult>(Expression<Func<TResult>> exp)
        {
            var body = (MethodCallExpression)exp.Body;
            return body.Method;
        }


        public override string ToString()
        {
            InternalQuery<T> internalQuery = this.GenerateIterator();
            return internalQuery.ToString();
        }
    }
}
