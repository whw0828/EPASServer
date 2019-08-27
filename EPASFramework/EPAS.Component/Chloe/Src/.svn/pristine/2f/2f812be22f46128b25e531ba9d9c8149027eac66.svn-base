using Chloe.Core;
using Chloe.DbExpressions;
using Chloe.InternalExtensions;
using Chloe.PostgreSQL.MethodHandlers;
using Chloe.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chloe.PostgreSQL
{
    partial class SqlGenerator : DbExpressionVisitor<DbExpression>
    {
        static Dictionary<string, IMethodHandler> GetMethodHandlers()
        {
            var methodHandlers = new Dictionary<string, IMethodHandler>();

            var methodHandlerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(a => a.IsClass && !a.IsAbstract && typeof(IMethodHandler).IsAssignableFrom(a) && a.Name.EndsWith("_Handler") && a.GetConstructor(Type.EmptyTypes) != null);

            foreach (Type methodHandlerType in methodHandlerTypes)
            {
                string handleMethodName = methodHandlerType.Name.Substring(0, methodHandlerType.Name.Length - "_Handler".Length);
                methodHandlers.Add(handleMethodName, (IMethodHandler)Activator.CreateInstance(methodHandlerType));
            }

            var ret = PublicHelper.Clone(methodHandlers);
            return ret;
        }
    }
}
