using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chloe.DbExpressions;

namespace Chloe.Oracle.MethodHandlers
{
    class AddDays_Handler : IMethodHandler
    {
        public bool CanProcess(DbMethodCallExpression exp)
        {
            if (exp.Method.DeclaringType != UtilConstants.TypeOfDateTime)
                return false;

            return true;
        }
        public void Process(DbMethodCallExpression exp, SqlGenerator generator)
        {
            /* (systimestamp + 3) */
            generator.SqlBuilder.Append("(");
            exp.Object.Accept(generator);
            generator.SqlBuilder.Append(" + ");
            exp.Arguments[0].Accept(generator);
            generator.SqlBuilder.Append(")");
        }
    }
}
