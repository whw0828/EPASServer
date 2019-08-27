﻿using Chloe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChloeDemo
{
    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        string _connString = null;
        public MySqlConnectionFactory(string connString)
        {
            this._connString = connString;
        }
        public IDbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }
    }
}
