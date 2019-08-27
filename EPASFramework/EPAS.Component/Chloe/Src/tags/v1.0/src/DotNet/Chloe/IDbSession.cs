using System;
using System.Data;

namespace Chloe
{
    public interface IDbSession : IDisposable
    {
        IDbContext DbContext { get; }
        bool IsInTransaction { get; }
        int ExecuteNonQuery(string sql, params DbParam[] parameters);
        object ExecuteScalar(string sql, params DbParam[] parameters);
        IDataReader ExecuteReader(string sql, params DbParam[] parameters);

        void BeginTransaction();
        void BeginTransaction(IsolationLevel il);
        void CommitTransaction();
        void RollbackTransaction();

    }
}
