using Chloe.Core;
using Chloe.Core.Visitors;
using Chloe.DbExpressions;
using Chloe.Descriptors;
using Chloe.Entity;
using Chloe.Exceptions;
using Chloe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chloe.SQLite
{
    public class SQLiteContext : DbContext
    {
        DbContextServiceProvider _dbContextServiceProvider;
        public SQLiteContext(IDbConnectionFactory dbConnectionFactory)
            : this(dbConnectionFactory, true)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="concurrencyMode">是否支持读写并发安全</param>
        public SQLiteContext(IDbConnectionFactory dbConnectionFactory, bool concurrencyMode)
        {
            Utils.CheckNull(dbConnectionFactory);

            if (concurrencyMode == true)
                dbConnectionFactory = new ConcurrentDbConnectionFactory(dbConnectionFactory);

            this._dbContextServiceProvider = new DbContextServiceProvider(dbConnectionFactory);
        }

        public override IDbContextServiceProvider DbContextServiceProvider
        {
            get { return this._dbContextServiceProvider; }
        }
        protected override string GetSelectLastInsertIdClause()
        {
            return "SELECT LAST_INSERT_ROWID()";
        }
    }

    class ConcurrentDbConnectionFactory : IDbConnectionFactory
    {
        IDbConnectionFactory _dbConnectionFactory;
        public ConcurrentDbConnectionFactory(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }
        public IDbConnection CreateConnection()
        {
            IDbConnection conn = new ChloeSQLiteConcurrentConnection(this._dbConnectionFactory.CreateConnection());
            return conn;
        }
    }

}
