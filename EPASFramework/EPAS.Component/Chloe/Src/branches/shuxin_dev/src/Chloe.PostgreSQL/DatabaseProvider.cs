using Chloe.Core.Visitors;
using Chloe.Infrastructure;
using Chloe.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Chloe.PostgreSQL
{
    class DatabaseProvider : IDatabaseProvider
    {
        IDbConnectionFactory _dbConnectionFactory;
        PostgreSQLContext _postgreSQLContext;

        public string DatabaseType { get { return "PostgreSQL"; } }

        public DatabaseProvider(IDbConnectionFactory dbConnectionFactory, PostgreSQLContext postgreSQLContext)
        {
            this._dbConnectionFactory = dbConnectionFactory;
            this._postgreSQLContext = postgreSQLContext;
        }
        public IDbConnection CreateConnection()
        {
            return this._dbConnectionFactory.CreateConnection();
        }
        public IDbExpressionTranslator CreateDbExpressionTranslator()
        {
            if (this._postgreSQLContext.ConvertToLowercase == true)
            {
                return DbExpressionTranslator_ConvertToLowercase.Instance;
            }
            else
            {
                return DbExpressionTranslator.Instance;
            }
        }
        public string CreateParameterName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (name[0] == UtilConstants.ParameterNamePlaceholer[0])
            {
                return name;
            }

            return UtilConstants.ParameterNamePlaceholer + name;
        }
    }
}
