﻿using Chloe.Infrastructure.Interception;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChloeDemo
{
    class DbCommandInterceptor : IDbCommandInterceptor
    {
        public void ReaderExecuting(IDbCommand command, DbCommandInterceptionContext<IDataReader> interceptionContext)
        {
            //interceptionContext.DataBag.Add("startTime", DateTime.Now);
            Debug.WriteLine(AppendDbCommandInfo(command));
            Console.WriteLine(command.CommandText);
            AmendParameter(command);
        }
        public void ReaderExecuted(IDbCommand command, DbCommandInterceptionContext<IDataReader> interceptionContext)
        {
            //DateTime startTime = (DateTime)(interceptionContext.DataBag["startTime"]);
            //Console.WriteLine(DateTime.Now.Subtract(startTime).TotalMilliseconds);
            if (interceptionContext.Exception == null)
                Console.WriteLine(interceptionContext.Result.FieldCount);
        }

        public void NonQueryExecuting(IDbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Debug.WriteLine(AppendDbCommandInfo(command));
            Console.WriteLine(command.CommandText);
            AmendParameter(command);
        }
        public void NonQueryExecuted(IDbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (interceptionContext.Exception == null)
                Console.WriteLine(interceptionContext.Result);
        }

        public void ScalarExecuting(IDbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            //interceptionContext.DataBag.Add("startTime", DateTime.Now);
            Debug.WriteLine(AppendDbCommandInfo(command));
            Console.WriteLine(command.CommandText);
            AmendParameter(command);
        }
        public void ScalarExecuted(IDbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            //DateTime startTime = (DateTime)(interceptionContext.DataBag["startTime"]);
            //Console.WriteLine(DateTime.Now.Subtract(startTime).TotalMilliseconds);
            if (interceptionContext.Exception == null)
                Console.WriteLine(interceptionContext.Result);
        }


        static void AmendParameter(IDbCommand command)
        {
            foreach (var parameter in command.Parameters)
            {
                if (parameter is OracleParameter)
                {
                    OracleParameter oracleParameter = (OracleParameter)parameter;
                    if (oracleParameter.Value is string)
                    {
                        /* 针对 oracle 长文本做处理 */
                        string value = (string)oracleParameter.Value;
                        if (value != null && value.Length > 4000)
                        {
                            oracleParameter.OracleDbType = OracleDbType.NClob;
                        }
                    }
                }
            }
        }

        public static string AppendDbCommandInfo(IDbCommand command)
        {
            StringBuilder sb = new StringBuilder();

            foreach (IDbDataParameter param in command.Parameters)
            {
                if (param == null)
                    continue;

                object value = null;
                if (param.Value == null || param.Value == DBNull.Value)
                {
                    value = "NULL";
                }
                else
                {
                    value = param.Value;

                    if (param.DbType == DbType.String || param.DbType == DbType.AnsiString || param.DbType == DbType.DateTime)
                        value = "'" + value + "'";
                }

                sb.AppendFormat("{3} {0} {1} = {2};", Enum.GetName(typeof(DbType), param.DbType), param.ParameterName, value, Enum.GetName(typeof(ParameterDirection), param.Direction));
                sb.AppendLine();
            }

            sb.AppendLine(command.CommandText);

            return sb.ToString();
        }
    }
}
