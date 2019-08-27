using Chloe.Core;
using Chloe.Exceptions;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Chloe.Core
{
    class InternalDbSession : IDisposable
    {
        IDbConnection _dbConnection;
        IDbTransaction _dbTransaction;
        IDbCommand _dbCommand;
        bool _isInTransaction;
        bool _disposed = false;

        public InternalDbSession(IDbConnection conn)
        {
            this._dbConnection = conn;
        }

        IDbCommand DbCommand
        {
            get
            {
                this.CheckDisposed();
                if (this._dbCommand == null)
                    this._dbCommand = this._dbConnection.CreateCommand();
                return this._dbCommand;
            }
        }
        public bool IsInTransaction
        {
            get { return this._isInTransaction; }
        }

        void Activate()
        {
            this.CheckDisposed();

            if (this._dbConnection.State == ConnectionState.Broken)
            {
                this._dbConnection.Close();
            }

            if (this._dbConnection.State == ConnectionState.Closed)
            {
                this._dbConnection.Open();
            }
        }
        /// <summary>
        /// 表示一次查询完成。在事务中的话不关闭连接，交给 CommitTransaction() 或者 RollbackTransaction() 控制，否则调用 IDbConnection.Close() 关闭连接
        /// </summary>
        public void Complete()
        {
            //在事务中的话不关闭连接  交给CommitTransaction()或者RollbackTransaction()
            if (!this._isInTransaction)
            {
                if (this._dbConnection.State == ConnectionState.Open)
                {
                    this._dbConnection.Close();
                }
            }
        }

        public void BeginTransaction()
        {
            this.Activate();
            this._dbTransaction = _dbConnection.BeginTransaction();
            this._isInTransaction = true;
        }
        public void BeginTransaction(IsolationLevel il)
        {
            this.Activate();
            this._dbTransaction = this._dbConnection.BeginTransaction(il);
            this._isInTransaction = true;
        }
        public void CommitTransaction()
        {
            if (!this._isInTransaction)
            {
                throw new ChloeException("Current session does not open a transaction.");
            }
            this._dbTransaction.Commit();
            this._dbTransaction.Dispose();
            this._isInTransaction = false;
            this.Complete();
        }
        public void RollbackTransaction()
        {
            if (!this._isInTransaction)
            {
                throw new ChloeException("Current session does not open a transaction.");
            }
            this._dbTransaction.Rollback();
            this._dbTransaction.Dispose();
            this._isInTransaction = false;
            this.Complete();
        }

        public IDataReader ExecuteReader(string cmdText, DbParam[] parameters)
        {
            return this.ExecuteReader(cmdText, parameters, CommandBehavior.Default, CommandType.Text);
        }
        public IDataReader ExecuteReader(string cmdText, DbParam[] parameters, CommandType cmdType)
        {
            return this.ExecuteReader(cmdText, parameters, CommandBehavior.Default, cmdType);
        }
        public IDataReader ExecuteReader(string cmdText, DbParam[] parameters, CommandBehavior behavior)
        {
            return this.ExecuteReader(cmdText, parameters, behavior, CommandType.Text);
        }
        public IDataReader ExecuteReader(string cmdText, DbParam[] parameters, CommandBehavior behavior, CommandType cmdType)
        {
            this.CheckDisposed();

#if DEBUG
            System.Diagnostics.Debug.WriteLine(AppendDbCommandInfo(cmdText, parameters));
#endif

            IDbCommand cmd = this.DbCommand;

            this.PrepareCommand(cmd, cmdText, parameters, cmdType);

            this.Activate();

            IDataReader reader = cmd.ExecuteReader(behavior);
            cmd.Parameters.Clear();
            return reader;
        }

        public int ExecuteNonQuery(string cmdText, DbParam[] parameters)
        {
            return this.ExecuteNonQuery(cmdText, parameters, CommandType.Text);
        }
        public int ExecuteNonQuery(string cmdText, DbParam[] parameters, CommandType cmdType)
        {
            this.CheckDisposed();

#if DEBUG
            System.Diagnostics.Debug.WriteLine(AppendDbCommandInfo(cmdText, parameters));
#endif

            try
            {
                IDbCommand cmd = this.DbCommand;

                this.PrepareCommand(cmd, cmdText, parameters, cmdType);

                this.Activate();
                int r = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return r;
            }
            finally
            {
                this.Complete();
            }
        }

        public object ExecuteScalar(string cmdText, DbParam[] parameters)
        {
            return this.ExecuteScalar(cmdText, parameters, CommandType.Text);
        }
        public object ExecuteScalar(string cmdText, DbParam[] parameters, CommandType cmdType)
        {
            this.CheckDisposed();

#if DEBUG
            System.Diagnostics.Debug.WriteLine(AppendDbCommandInfo(cmdText, parameters));
#endif

            try
            {
                IDbCommand cmd = this.DbCommand;

                this.PrepareCommand(cmd, cmdText, parameters, cmdType);

                this.Activate();
                object r = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return r;
            }
            finally
            {
                this.Complete();
            }
        }

        internal InternalDataReader ExecuteInternalReader(string cmdText, DbParam[] parameters, CommandType cmdType)
        {
            IDataReader reader = this.ExecuteReader(cmdText, parameters, cmdType);
            return new InternalDataReader(this, reader);
        }
        public void Dispose()
        {
            if (this._disposed)
                return;

            if (this._dbTransaction != null)
            {
                if (this._isInTransaction)
                {
                    try
                    {
                        this._dbTransaction.Rollback();
                    }
                    catch
                    {
                    }
                }

                this._dbTransaction.Dispose();
                this._dbTransaction = null;
                this._isInTransaction = false;
            }

            if (this._dbCommand != null)
            {
                this._dbCommand.Dispose();
                this._dbCommand = null;
            }

            if (this._dbConnection != null)
            {
                this._dbConnection.Dispose();
            }

            this._disposed = true;
        }

        void PrepareCommand(IDbCommand cmd, string cmdText, DbParam[] parameters, CommandType cmdType)
        {
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            if (this.IsInTransaction)
                cmd.Transaction = this._dbTransaction;

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    var param = parameters[i];
                    if (param == null)
                        continue;

                    var parameter = cmd.CreateParameter();
                    parameter.ParameterName = param.Name;

                    Type parameterType;
                    if (param.Value == null || param.Value == DBNull.Value)
                    {
                        parameter.Value = DBNull.Value;
                        parameterType = param.Type;
                    }
                    else
                    {
                        parameter.Value = param.Value;
                        parameterType = param.Value.GetType();
                    }

                    DbType? dbType = Utils.TryGetDbType(parameterType);
                    if (dbType != null)
                        parameter.DbType = dbType.Value;

                    cmd.Parameters.Add(parameter);
                }
            }
        }

        void CheckDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }


        public static string AppendDbCommandInfo(string cmdText, DbParam[] parameters)
        {
            StringBuilder sb = new StringBuilder();
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    if (param == null)
                        continue;

                    string typeName = null;
                    object value = null;
                    Type parameterType;
                    if (param.Value == null || param.Value == DBNull.Value)
                    {
                        parameterType = param.Type;
                        value = "NULL";
                    }
                    else
                    {
                        value = param.Value;
                        parameterType = param.Value.GetType();

                        if (parameterType == typeof(string) || parameterType == typeof(DateTime))
                            value = "'" + value + "'";
                    }

                    if (parameterType != null)
                        typeName = GetTypeName(parameterType);

                    sb.AppendFormat("{0} {1} = {2};", typeName, param.Name, value);
                    sb.AppendLine();
                }
            }

            sb.AppendLine(cmdText);

            return sb.ToString();
        }
        static string GetTypeName(Type type)
        {
            Type unType;
            if (Utils.IsNullable(type, out unType))
            {
                return string.Format("Nullable<{0}>", GetTypeName(unType));
            }

            return type.Name;
        }
    }
}
