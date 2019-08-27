using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chloe.DbExpressions;

namespace Chloe.SqlServer.MethodHandlers
{
    class NewGuid_Handler : IMethodHandler
    {
        public bool CanProcess(DbMethodCallExpression exp)
        {
            if (exp.Method != UtilConstants.MethodInfo_Guid_NewGuid)
                return false;

            return true;
        }
        public void Process(DbMethodCallExpression exp, SqlGenerator generator)
        {
            generator.SqlBuilder.Append("NEWID()");
        }
    }
}
