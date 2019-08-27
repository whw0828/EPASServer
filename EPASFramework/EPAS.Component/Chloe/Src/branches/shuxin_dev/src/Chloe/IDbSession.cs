using Chloe.Infrastructure.Interception;
using System;
using System.Data;

namespace Chloe
{
    public interface IDbSession : IDisposable
    {
        IDbContext DbContext { get; }
        IDbConnection CurrentConnection { get; }
        /// <summary>
        /// 如果未开启事务，则返回 null
        /// </summary>
        IDbTransaction CurrentTransaction { get; }
        bool IsInTransaction { get; }
        int CommandTimeout { get; set; }

        int ExecuteNonQuery(string cmdText, params DbParam[] parameters);
        int ExecuteNonQuery(string cmdText, CommandType cmdType, params DbParam[] parameters);
        int ExecuteNonQuery(string cmdText, object parameter);
        /// <summary>
        /// dbSession.ExecuteNonQuery("update Users set Age=18 where Id=@Id", CommandType.Text, new { Id = 1 })
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string cmdText, CommandType cmdType, object parameter);

        object ExecuteScalar(string cmdText, params DbParam[] parameters);
        object ExecuteScalar(string cmdText, CommandType cmdType, params DbParam[] parameters);
        object ExecuteScalar(string cmdText, object parameter);
        /// <summary>
        /// dbSession.ExecuteScalar("select Age from Users where Id=@Id", CommandType.Text, new { Id = 1 })
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        object ExecuteScalar(string cmdText, CommandType cmdType, object parameter);

        IDataReader ExecuteReader(string cmdText, params DbParam[] parameters);
        IDataReader ExecuteReader(string cmdText, CommandType cmdType, params DbParam[] parameters);
        IDataReader ExecuteReader(string cmdText, object parameter);
        /// <summary>
        /// dbSession.ExecuteReader("select * from Users where Id=@Id", CommandType.Text, new { Id = 1 })
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string cmdText, CommandType cmdType, object parameter);

        void BeginTransaction();
        void BeginTransaction(IsolationLevel il);
        void CommitTransaction();
        void RollbackTransaction();

        void AddInterceptor(IDbCommandInterceptor interceptor);
        void RemoveInterceptor(IDbCommandInterceptor interceptor);
    }
}
