using Chloe;
using Chloe.Infrastructure;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChloeDemo
{
    class String_MappingType : MappingType<string>, IMappingType
    {
        public String_MappingType() : base(DbType.String)
        {
        }
        public override IDbDataParameter CreateDataParameter(IDbCommand cmd, DbParam param)
        {
            DbType jsonDbType = (DbType)100;

            IDbDataParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = param.Name;

            if (param.Value == null || param.Value == DBNull.Value)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = param.Value;
            }

            if (parameter is NpgsqlParameter)
            {
                NpgsqlParameter pgsqlParameter = (NpgsqlParameter)parameter;

                if (param.DbType == jsonDbType)
                {
                    parameter.DbType = DbType.String;
                    pgsqlParameter.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Json;
                }
                else if (param.DbType != null)
                    parameter.DbType = param.DbType.Value;
            }
            else if (param.DbType != null)
                parameter.DbType = param.DbType.Value;

            if (param.Precision != null)
                parameter.Precision = param.Precision.Value;

            if (param.Scale != null)
                parameter.Scale = param.Scale.Value;

            if (param.Size != null)
                parameter.Size = param.Size.Value;



            const int defaultSizeOfStringOutputParameter = 4000;/* 当一个 string 类型输出参数未显示指定 Size 时使用的默认大小。如果有需要更大或者该值不足以满足需求，需显示指定 DbParam.Size 值 */

            if (param.Direction == ParamDirection.Input)
            {
                parameter.Direction = ParameterDirection.Input;
            }
            else if (param.Direction == ParamDirection.Output)
            {
                parameter.Direction = ParameterDirection.Output;
                param.Value = null;
                if (param.Size == null && param.Type == typeof(string))
                {
                    parameter.Size = defaultSizeOfStringOutputParameter;
                }
            }
            else if (param.Direction == ParamDirection.InputOutput)
            {
                parameter.Direction = ParameterDirection.InputOutput;
                if (param.Size == null && param.Type == typeof(string))
                {
                    parameter.Size = defaultSizeOfStringOutputParameter;
                }
            }
            else
                throw new NotSupportedException(string.Format("ParamDirection '{0}' is not supported.", param.Direction));

            return parameter;
        }
    }
}
