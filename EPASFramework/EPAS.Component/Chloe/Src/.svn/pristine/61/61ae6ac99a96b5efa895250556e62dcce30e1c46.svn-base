using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chloe.Extensions
{
    internal static class ExpressionExtensions
    {
        public static BinaryExpression Assign(MemberInfo propertyOrField, Expression instance, Expression value)
        {
            PropertyInfo propertyInfo = propertyOrField as PropertyInfo;
            if (propertyInfo != null)
            {
                var pro = Expression.Property(instance, propertyInfo);
                var setValue = Expression.Assign(pro, value);
                return setValue;
            }

            FieldInfo fieldInfo = propertyOrField as FieldInfo;
            if (fieldInfo != null)
            {
                var field = Expression.Field(instance, fieldInfo);
                var setValue = Expression.Assign(field, value);
                return setValue;
            }

            throw new ArgumentException();
        }

        public static bool IsDerivedFromParameter(this MemberExpression exp)
        {
            ParameterExpression p;
            return IsDerivedFromParameter(exp, out p);
        }

        public static bool IsDerivedFromParameter(this MemberExpression exp, out ParameterExpression p)
        {
            Expression prevExp = exp.Expression;
            MemberExpression memberExp = prevExp as MemberExpression;
            while (memberExp != null)
            {
                prevExp = memberExp.Expression;
                memberExp = prevExp as MemberExpression;
            }

            p = prevExp as ParameterExpression;

            if (p != null)
                return true;
            else
                return false;
        }

        public static Expression StripQuotes(this Expression exp)
        {
            while (exp.NodeType == ExpressionType.Quote)
            {
                exp = ((UnaryExpression)exp).Operand;
            }
            return exp;
        }

        public static Stack<MemberExpression> Reverse(this MemberExpression exp)
        {
            var stack = new Stack<MemberExpression>();
            stack.Push(exp);
            while ((exp = exp.Expression as MemberExpression) != null)
            {
                stack.Push(exp);
            }
            return stack;
        }
    }
}
