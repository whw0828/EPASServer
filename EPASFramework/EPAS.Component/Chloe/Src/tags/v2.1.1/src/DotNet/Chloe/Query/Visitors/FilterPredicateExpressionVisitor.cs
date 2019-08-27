using Chloe.Core.Visitors;
using Chloe.DbExpressions;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Chloe.Query.Visitors
{
    class FilterPredicateExpressionVisitor : ExpressionVisitor<DbExpression>
    {
        public static DbExpression VisitFilterPredicate(LambdaExpression lambda, List<IMappingObjectExpression> moeList)
        {
            return GeneralExpressionVisitor.VisitPredicate(ExpressionVisitorBase.ReBuildFilterPredicate(lambda), moeList);
        }
    }
}
