using Chloe.DbExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chloe.SQLite
{
    interface IMethodHandler
    {
        bool CanProcess(DbMethodCallExpression exp);
        void Process(DbMethodCallExpression exp, SqlGenerator generator);
    }
}
