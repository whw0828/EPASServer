using Chloe.Annotations;
using Chloe.Core.Visitors;
using Chloe.DbExpressions;
using Chloe.InternalExtensions;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chloe.Oracle
{
    class EvaluableDbExpressionTransformer : EvaluableDbExpressionTransformerBase
    {
        static EvaluableDbExpressionTransformer _transformer = new EvaluableDbExpressionTransformer();

        static KeyDictionary<MemberInfo> _toTranslateMembers = new KeyDictionary<MemberInfo>();
        static EvaluableDbExpressionTransformer()
        {
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_String_Length);

            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_Now);
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_UtcNow);
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_Today);
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_Date);

            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_Year);
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_Month);
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_Day);
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_Hour);
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_Minute);
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_Second);
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_Millisecond);
            _toTranslateMembers.Add(UtilConstants.PropertyInfo_DateTime_DayOfWeek);

            _toTranslateMembers.Add(OracleSemantics.PropertyInfo_ROWNUM);

            _toTranslateMembers = _toTranslateMembers.Clone();
        }

        public static DbExpression Transform(DbExpression exp)
        {
            return exp.Accept(_transformer);
        }

        public override bool CanTranslateToSql(DbMemberExpression exp)
        {
            return _toTranslateMembers.Exists(exp.Member);
        }
        public override bool CanTranslateToSql(DbMethodCallExpression exp)
        {
            IMethodHandler methodHandler;
            if (SqlGenerator.MethodHandlers.TryGetValue(exp.Method.Name, out methodHandler))
            {
                if (methodHandler.CanProcess(exp))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
