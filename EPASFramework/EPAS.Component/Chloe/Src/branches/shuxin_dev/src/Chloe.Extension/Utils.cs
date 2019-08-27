using Chloe.Infrastructure;
using Chloe.InternalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Extension
{
    static class Utils
    {
        public static Task<T> MakeTask<T>(Func<T> func)
        {
            var task = new Task<T>(func);
            task.Start();
            return task;
        }

        public static DbParam[] BuildParams(IDbContext dbContext, object parameter)
        {
            if (parameter == null)
                return new DbParam[0];

            if (parameter is IEnumerable<DbParam>)
            {
                return ((IEnumerable<DbParam>)parameter).ToArray();
            }

            IDatabaseProvider databaseProvider = ((DbContext)dbContext).DatabaseProvider;

            List<DbParam> parameters = new List<DbParam>();
            Type parameterType = parameter.GetType();
            var props = parameterType.GetProperties();
            foreach (var prop in props)
            {
                if (prop.GetGetMethod() == null)
                {
                    continue;
                }

                object value = ReflectionExtension.GetMemberValue(prop, parameter);

                string paramName = databaseProvider.CreateParameterName(prop.Name);

                DbParam p = new DbParam(paramName, value, prop.PropertyType);
                parameters.Add(p);
            }

            return parameters.ToArray();
        }
    }
}
