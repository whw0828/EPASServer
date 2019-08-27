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
    class GroupKeySelectorParser : ExpressionVisitor<DbExpression[]>
    {
        ScopeParameterDictionary _scopeParameters;
        KeyDictionary<string> _scopeTables;
        public GroupKeySelectorParser(ScopeParameterDictionary scopeParameters, KeyDictionary<string> scopeTables)
        {
            this._scopeParameters = scopeParameters;
            this._scopeTables = scopeTables;
        }

        public static DbExpression[] Parse(Expression keySelector, ScopeParameterDictionary scopeParameters, KeyDictionary<string> scopeTables)
        {
            return new GroupKeySelectorParser(scopeParameters, scopeTables).Visit(keySelector);
        }

        public override DbExpression[] Visit(Expression exp)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.Lambda:
                    return this.VisitLambda((LambdaExpression)exp);
                case ExpressionType.New:
                    return this.VisitNew((NewExpression)exp);
                default:
                    {
                        var dbExp = GeneralExpressionParser.Parse(exp, this._scopeParameters, this._scopeTables);
                        return new DbExpression[1] { dbExp };
                    }
            }
        }

        protected override DbExpression[] VisitLambda(LambdaExpression exp)
        {
            return this.Visit(exp.Body);
        }
        protected override DbExpression[] VisitNew(NewExpression exp)
        {
            DbExpression[] ret = new DbExpression[exp.Arguments.Count];
            for (int i = 0; i < exp.Arguments.Count; i++)
            {
                var dbExp = GeneralExpressionParser.Parse(exp.Arguments[i], this._scopeParameters, this._scopeTables);
                ret[i] = dbExp;
            }

            return ret;
        }
    }
}
