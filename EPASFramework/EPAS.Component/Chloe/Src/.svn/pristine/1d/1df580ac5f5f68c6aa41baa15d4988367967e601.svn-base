using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chloe.DbExpressions;

namespace Chloe.PostgreSQL.MethodHandlers
{
    class DiffSeconds_Handler : IMethodHandler
    {
        public bool CanProcess(DbMethodCallExpression exp)
        {
            if (exp.Method.DeclaringType != UtilConstants.TypeOfSql)
                return false;

            return false;
        }
        public void Process(DbMethodCallExpression exp, SqlGenerator generator)
        {
            throw UtilExceptions.NotSupportedMethod(exp.Method);
        }
    }
}
