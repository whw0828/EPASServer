﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Chloe.DbExpressions;

namespace Chloe.SqlServer.MethodHandlers
{
    class In_Handler : IMethodHandler
    {
        public bool CanProcess(DbMethodCallExpression exp)
        {
            MethodInfo method = exp.Method;
            /* public static bool In<T>(this T obj, IEnumerable<T> source) */
            if (method.IsGenericMethod && method.ReturnType == UtilConstants.TypeOfBoolean)
            {
                Type[] genericArguments = method.GetGenericArguments();
                ParameterInfo[] parameters = method.GetParameters();
                Type genericType = genericArguments[0];
                if (genericArguments.Length == 1 && parameters.Length == 2 && parameters[0].ParameterType == genericType)
                {
                    Type secondParameterType = parameters[1].ParameterType;
                    if (typeof(IEnumerable<>).MakeGenericType(genericType).IsAssignableFrom(secondParameterType))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public void Process(DbMethodCallExpression exp, SqlGenerator generator)
        {
            MethodInfo method = exp.Method;
            /* public static bool In<T>(this T obj, IEnumerable<T> source) */

            Type[] genericArguments = method.GetGenericArguments();
            Type genericType = genericArguments[0];

            MethodInfo method_Contains = UtilConstants.MethodInfo_Enumerable_Contains.MakeGenericMethod(genericType);
            List<DbExpression> arguments = new List<DbExpression>(2) { exp.Arguments[1], exp.Arguments[0] };
            DbMethodCallExpression newExp = new DbMethodCallExpression(null, method_Contains, arguments);
            newExp.Accept(generator);
        }
    }
}
