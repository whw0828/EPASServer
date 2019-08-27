using Chloe.InternalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chloe.DbExpressions;
using System.Reflection;

namespace Chloe.Oracle.MethodHandlers
{
    class MethodHandlerHelper
    {
        public static void EnsureTrimCharArgumentIsSpaces(DbExpression exp)
        {
            if (!exp.IsEvaluable())
                throw new NotSupportedException();

            var arg = exp.Evaluate();
            if (arg == null)
                throw new ArgumentNullException();

            var chars = arg as char[];
            if (chars.Length != 1 || chars[0] != ' ')
            {
                throw new NotSupportedException();
            }
        }

        public static string AppendNotSupportedDbFunctionsMsg(MethodInfo method, string insteadProperty)
        {
            return string.Format("'{0}' is not supported. Instead of using '{1}.{2}'.", Utils.ToMethodString(method), Utils.ToMethodString(UtilConstants.MethodInfo_DateTime_Subtract_DateTime), insteadProperty);
        }
    }
}
