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
        int _commandTimeout = 30;/* seconds */
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
        public bool IsInTransaction { get { return this._isInTransaction; } }
        public int CommandTimeout { get { return this._commandTimeout; } set { this._commandTimeout = value; } }

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
            this.EndTransaction();
        }
        public void RollbackTransaction()
        {
            if (!this._isInTransaction)
            {
                throw new ChloeException("Current session does not open a transaction.");
            }
            this._dbTransaction.Rollback();
            this.EndTransaction();
        }

        public IDataReader ExecuteReader(string cmdText, DbParam[] parameters, CommandType cmdType)
        {
            return this.ExecuteReader(cmdText, parameters, cmdType, CommandBehavior.Default);
        }
        public IDataReader ExecuteReader(string cmdText, DbParam[] parameters, CommandType cmdType, CommandBehavior behavior)
        {
            this.CheckDisposed();

#if DEBUG
            System.Diagnostics.Debug.WriteLine(AppendDbCommandInfo(cmdText, parameters));
#endif

            IDbCommand cmd = this.DbCommand;

            List<OutputParameter> outputParameters = this.PrepareCommand(cmd, cmdText, parameters, cmdType);

            this.Activate();

            IDataReader reader = new InternalDataReader(this, cmd.ExecuteReader(behavior), cmd, outputParameters);
            return reader;
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

                List<OutputParameter> outputParameters = this.PrepareCommand(cmd, cmdText, parameters, cmdType);

                this.Activate();
                int r = cmd.ExecuteNonQuery();
                OutputParameter.CallMapValue(outputParameters);
                cmd.Parameters.Clear();
                return r;
            }
            finally
            {
                this.Complete();
            }
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

                List<OutputParameter> outputParameters = this.PrepareCommand(cmd, cmdText, parameters, cmdType);

                this.Activate();
                object r = cmd.ExecuteScalar();
                OutputParameter.CallMapValue(outputParameters);
                cmd.Parameters.Clear();
                return r;
            }
            finally
            {
                this.Complete();
            }
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

        List<OutputParameter> PrepareCommand(IDbCommand cmd, string cmdText, DbParam[] parameters, CommandType cmdType)
        {
            List<OutputParameter> outputParameters = null;

            if (cmd.Parameters.Count > 0)
            {
                cmd.Parameters.Clear();/* 目前的设计 IDbCommand 在当前 InternalDbSession 中是单例可重用的，所以，每次装载 IDbCommand 得保证清掉上次执行的 Parameters（主要防止以下“特俗”情况：当执行 sql 过程出现异常，会来不及调用 cmd.Parameters.Clear()，就会出现残留的 Parameters，为了保证每次使用 IDbCommand 不受上次使用结果的影响，所以得 Clear 下，否则会成为bug！）  */
            }

            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = this._commandTimeout;
            cmd.Transaction = this.IsInTransaction ? this._dbTransaction : null;

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    var param = parameters[i];
                    if (param == null)
                        continue;

                    if (param.ExplicitParameter != null)/* 如果存在创建好了的 IDbDataParameter，则直接用它。同时也忽视了 DbParam 的其他属性 */
                    {
                        cmd.Parameters.Add(param.ExplicitParameter);
                        continue;
                    }

                    IDbDataParameter parameter = cmd.CreateParameter();
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

                    if (param.Precision != null)
                        parameter.Precision = param.Precision.Value;

                    if (param.Scale != null)
                        parameter.Scale = param.Scale.Value;

                    if (param.Size != null)
                        parameter.Size = param.Size.Value;

                    DbType? dbType = Utils.TryGetDbType(parameterType);
                    if (dbType != null)
                        parameter.DbType = dbType.Value;

                    const int defaultSizeOfStringOutputParameter = 8000;/* 当一个 string 类型输出参数未显示指定 Size 时使用的默认大小。如果有需要更大或者该值不足以满足需求，需显示指定 DbParam.Size 值 */

                    OutputParameter outputParameter = null;
                    if (param.Direction == ParamDirection.Input)
                    {
                        parameter.Direction = ParameterDirection.Input;
                    }
                    else if (param.Direction == ParamDirection.Output)
                    {
                        parameter.Direction = ParameterDirection.Output;
                        param.Value = null;
                        if (param.Size == null && param.Type == UtilConstants.TypeOfString)
                        {
                            parameter.Size = defaultSizeOfStringOutputParameter;
                        }
                        outputParameter = new OutputParameter(param, parameter);
                    }
                    else if (param.Direction == ParamDirection.InputOutput)
                    {
                        parameter.Direction = ParameterDirection.InputOutput;
                        if (param.Size == null && param.Type == UtilConstants.TypeOfString)
                        {
                            parameter.Size = defaultSizeOfStringOutputParameter;
                        }
                        outputParameter = new OutputParameter(param, parameter);
                    }
                    else
                        throw new NotSupportedException(string.Format("ParamDirection '{0}' is not supported.", param.Direction));

                    cmd.Parameters.Add(parameter);

                    if (outputParameter != null)
                    {
                        if (outputParameters == null)
                            outputParameters = new List<OutputParameter>();
                        outputParameters.Add(outputParameter);
                    }
                }
            }

            return outputParameters;
        }
        void EndTransaction()
        {
            this._dbTransaction.Dispose();
            if (this._dbCommand != null)
                this._dbCommand.Transaction = null;
            this._isInTransaction = false;
            this.Complete();
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
