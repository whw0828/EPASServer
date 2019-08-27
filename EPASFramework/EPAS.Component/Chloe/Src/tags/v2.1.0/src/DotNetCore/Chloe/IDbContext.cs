using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Chloe
{
    public interface IDbContext : IDisposable
    {
        IDbSession Session { get; }
        [Obsolete("'CurrentSession' will be removed in future releases.Instead of using 'Session' property.")]
        IDbSession CurrentSession { get; }

        IQuery<TEntity> Query<TEntity>() where TEntity : new();
        IEnumerable<T> SqlQuery<T>(string sql, params DbParam[] parameters) where T : new();
        IEnumerable<T> SqlQuery<T>(string sql, CommandType cmdType, params DbParam[] parameters) where T : new();

        TEntity Insert<TEntity>(TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="body"></param>
        /// <returns>PrimaryKey</returns>
        object Insert<TEntity>(Expression<Func<TEntity>> body);

        int Update<TEntity>(TEntity entity);
        int Update<TEntity>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TEntity>> body);
        [Obsolete("Current method will be removed in future releases.Instead of using 'Update<TEntity>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TEntity>> body)'.")]
        int Update<TEntity>(Expression<Func<TEntity, TEntity>> body, Expression<Func<TEntity, bool>> condition);

        int Delete<TEntity>(TEntity entity);
        int Delete<TEntity>(Expression<Func<TEntity, bool>> condition);

        void TrackEntity(object entity);
    }
}
