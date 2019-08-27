using Chloe.Core;
using Chloe.DbExpressions;
using Chloe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.SqlServer
{
    class DbExpressionTranslator : IDbExpressionTranslator
    {
        public static readonly DbExpressionTranslator Instance = new DbExpressionTranslator();
        public virtual string Translate(DbExpression expression, out List<DbParam> parameters)
        {
            SqlGenerator generator = this.CreateSqlGenerator();
            expression = EvaluableDbExpressionTransformer.Transform(expression);
            expression.Accept(generator);

            parameters = generator.Parameters;
            string sql = generator.SqlBuilder.ToSql();

            return sql;
        }
        public virtual SqlGenerator CreateSqlGenerator()
        {
            return SqlGenerator.CreateInstance();
        }
    }

    class DbExpressionTranslator_OffsetFetch : DbExpressionTranslator
    {
        public static readonly new DbExpressionTranslator_OffsetFetch Instance = new DbExpressionTranslator_OffsetFetch();
        public override SqlGenerator CreateSqlGenerator()
        {
            return new SqlGenerator_OffsetFetch();
        }
    }
}
