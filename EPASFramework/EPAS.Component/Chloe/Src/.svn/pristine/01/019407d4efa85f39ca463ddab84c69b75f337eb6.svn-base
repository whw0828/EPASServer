using Chloe.Infrastructure;
using Chloe.Infrastructure.Interception;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ChloeDemo
{
    public class Program
    {
        /* documentation：http://www.52chloe.com/Wiki/Document */
        public static void Main(string[] args)
        {
            /* 添加拦截器，输出 sql 语句极其相应的参数 */
            IDbCommandInterceptor interceptor = new DbCommandInterceptor();
            DbConfiguration.UseInterceptors(interceptor);

            RegisterMappingType();

            /* fluent mapping */
            DbConfiguration.UseTypeBuilders(typeof(UserMap));

            SQLiteDemo.Run();
            MsSqlDemo.Run();
            MySqlDemo.Run();
            PostgreSQLDemo.Run();
            OracleDemo.Run();
        }

        /// <summary>
        /// 注册映射类型。当数据库字段类型与属性类型不一致时，映射时自动将数据类型转换成与属性类型一致的类型。
        /// </summary>
        static void RegisterMappingType()
        {
            DbConfiguration.UseMappingType(new String_MappingType());
            DbConfiguration.UseMappingType(new MappingType<int>(DbType.Int32));
            DbConfiguration.UseMappingType(new MappingType<long>(DbType.Int64));
            DbConfiguration.UseMappingType(new MappingType<Int16>(DbType.Int16));
            DbConfiguration.UseMappingType(new MappingType<byte>(DbType.Byte));
            DbConfiguration.UseMappingType(new MappingType<double>(DbType.Double));
            DbConfiguration.UseMappingType(new MappingType<float>(DbType.Single));
            DbConfiguration.UseMappingType(new MappingType<decimal>(DbType.Decimal));
        }
    }
}
