using Chloe.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChloeDemo
{
    class MySqlDemo
    {
        public static void Test()
        {
            MySqlContext context = new MySqlContext(new MySqlConnectionFactory(""));

            var q = context.Query<User>();

            q.Take(10).ToList();
        }
    }
}
