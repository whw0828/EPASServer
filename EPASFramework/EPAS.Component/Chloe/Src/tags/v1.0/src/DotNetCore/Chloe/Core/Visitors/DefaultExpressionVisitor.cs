using Chloe.Core.Visitors;
using Chloe.DbExpressions;
using Chloe.Descriptors;
using Chloe.Exceptions;
using Chloe.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chloe.Core.Visitors
{
    public class DefaultExpressionVisitor : ExpressionVisitorBase
    {
        TypeDescriptor _typeDescriptor;

        public DefaultExpressionVisitor(TypeDescriptor typeDescriptor)
        {
            this._typeDescriptor = typeDescriptor;
        }

        public DbExpression VisitFilterPredicate(LambdaExpression lambda)
        {
            lambda = ExpressionVisitorBase.ReBuildFilterPredicate(lambda);
            return this.Visit(lambda);
        }

        protected override DbExpression VisitMemberAccess(MemberExpression exp)
        {
            if (ExpressionExtensions.IsDerivedFromParameter(exp))
            {
                Stack<MemberExpression> reversedExps = ExpressionExtensions.Reverse(exp);

                Dictionary<MemberInfo, DbColumnAccessExpression> memberColumnMap = this._typeDescriptor.MemberColumnMap;

                DbExpression dbExp = null;
                bool first = true;
                foreach (var me in reversedExps)
                {
                    if (first)
                    {
                        DbColumnAccessExpression dbColumnAccessExpression;
                        if (!memberColumnMap.TryGetValue(me.Member, out dbColumnAccessExpression))
                        {
                            throw new ChloeException(string.Format("The member '{0}' does not map any column.", me.Member.Name));
                        }

                        dbExp = dbColumnAccessExpression;
                        first = false;
                    }
                    else
                    {
                        DbMemberExpression dbMe = new DbMemberExpression(me.Member, dbExp);
                        dbExp = dbMe;
                    }
                }

                if (dbExp != null)
                {
                    return dbExp;
                }
                else
                    throw new Exception();
            }
            else
            {
                return base.VisitMemberAccess(exp);
            }
        }

        protected override DbExpression VisitParameter(ParameterExpression exp)
        {
            throw new NotSupportedException(exp.ToString());
        }
    }
}
