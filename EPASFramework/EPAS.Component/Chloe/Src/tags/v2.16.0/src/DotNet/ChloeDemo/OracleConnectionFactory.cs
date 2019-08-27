﻿using Chloe.Infrastructure;
using Chloe.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe
{
    public static class OracleRefCursorParameterHelper
    {
        public static DbParam AsRefCursorParam(this DbParam param, string refCursorName = "p_cursor")
        {
            OracleParameter p_cur = new OracleParameter();
            p_cur.ParameterName = refCursorName;
            p_cur.OracleDbType = OracleDbType.RefCursor;
            p_cur.Direction = ParameterDirection.Output;

            param.ExplicitParameter = p_cur;

            return param;
        }
    }
}

namespace ChloeDemo
{
    public class OracleConnectionFactory : IDbConnectionFactory
    {
        string _connString = null;
        public OracleConnectionFactory(string connString)
        {
            this._connString = connString;
        }
        public IDbConnection CreateConnection()
        {
            OracleConnection oracleConnection = new OracleConnection(this._connString);
            OracleConnectionDecorator conn = new OracleConnectionDecorator(oracleConnection);
            return conn;
        }
    }

    class OracleConnectionDecorator : IDbConnection, IDisposable
    {
        private OracleConnection _oracleConnection;
        public OracleConnectionDecorator(OracleConnection oracleConnection)
        {
            if (oracleConnection == null)
                throw new Exception("Please call 911.");
            _oracleConnection = oracleConnection;
        }

        public string ConnectionString
        {
            get { return _oracleConnection.ConnectionString; }
            set { _oracleConnection.ConnectionString = value; }
        }
        public int ConnectionTimeout
        {
            get { return _oracleConnection.ConnectionTimeout; }
        }
        public string Database
        {
            get { return _oracleConnection.Database; }
        }
        public ConnectionState State
        {
            get { return _oracleConnection.State; }
        }

        public IDbTransaction BeginTransaction()
        {
            return _oracleConnection.BeginTransaction();
        }
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return _oracleConnection.BeginTransaction(il);
        }
        public void ChangeDatabase(string databaseName)
        {
            _oracleConnection.ChangeDatabase(databaseName);
        }
        public void Close()
        {
            _oracleConnection.Close();
        }
        /// <summary>
        /// 驱动默认 BindByName 为 false，我们在这将它设置为 true
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateCommand()
        {
            var cmd = _oracleConnection.CreateCommand();
            cmd.BindByName = true;
            return cmd;
        }
        public void Open()
        {
            _oracleConnection.Open();
        }

        public void Dispose()
        {
            _oracleConnection.Dispose();
        }
    }
}
