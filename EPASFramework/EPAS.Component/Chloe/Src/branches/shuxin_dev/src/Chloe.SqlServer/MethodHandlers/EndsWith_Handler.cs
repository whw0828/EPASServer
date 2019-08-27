using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chloe.DbExpressions;

namespace Chloe.SqlServer.MethodHandlers
{
    class EndsWith_Handler : IMethodHandler
    {
        public bool CanProcess(DbMethodCallExpression exp)
        {
            if (exp.Method != UtilConstants.MethodInfo_String_EndsWith)
                return false;

            return true;
        }
        public void Process(DbMethodCallExpression exp, SqlGenerator generator)
        {
            exp.Object.Accept(generator);
            generator.SqlBuilder.Append(" LIKE '%' + ");
            exp.Arguments.First().Accept(generator);
        }
    }

}
