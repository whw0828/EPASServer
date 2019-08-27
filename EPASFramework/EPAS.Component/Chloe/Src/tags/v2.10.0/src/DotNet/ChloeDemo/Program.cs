using Chloe.Infrastructure.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChloeDemo
{
    public class Program
    {
        /* documentation：http://www.52chloe.com/Wiki/Document */
        public static void Main(string[] args)
        {
            IDbCommandInterceptor interceptor = new DbCommandInterceptor();
            DbInterception.Add(interceptor);

            SQLiteDemo.Test();
            MsSqlDemo.Test();
            MySqlDemo.Test();
            OracleDemo.Test();

            RegisterMappingTypeDemo.RunDemo();
        }
    }
}
