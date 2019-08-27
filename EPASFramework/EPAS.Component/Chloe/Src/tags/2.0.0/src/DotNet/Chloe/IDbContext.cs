using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Chloe
{
    public interface IDbContext : IDisposable
    {
        IDbSession CurrentSession { get; }

        IQuery<T> Query<T>() where T : new();
        IEnumerable<T> SqlQuery<T>(string sql, params DbParam[] parameters) where T : new();

        T Insert<T>(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="body"></param>
        /// <returns>PrimaryKey</returns>
        object Insert<T>(Expression<Func<T>> body);

        int Update<T>(T entity);
        int Update<T>(Expression<Func<T, T>> body, Expression<Func<T, bool>> condition);

        int Delete<T>(T entity);
        int Delete<T>(Expression<Func<T, bool>> condition);

        void TrackEntity(object entity);
    }
}
