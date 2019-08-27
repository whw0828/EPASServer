using Chloe.DbExpressions;
using Chloe.Descriptors;
using Chloe.Exceptions;
using Chloe.Infrastructure;
using Chloe.InternalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chloe.Utility
{
    public class PublicHelper
    {
        public static void CheckNull(object obj, string paramName = null)
        {
            if (obj == null)
                throw new ArgumentNullException(paramName);
        }
        public static bool AreEqual(object obj1, object obj2)
        {
            if (obj1 == null && obj2 == null)
                return true;

            if (obj1 != null)
            {
                return obj1.Equals(obj2);
            }

            if (obj2 != null)
            {
                return obj2.Equals(obj1);
            }

            return object.Equals(obj1, obj2);
        }
        public static DbMethodCallExpression MakeNextValueForSequenceDbExpression(PropertyDescriptor propertyDescriptor)
        {
            return MakeNextValueForSequenceDbExpression(propertyDescriptor.PropertyType, propertyDescriptor.Definition.SequenceName);
        }
        public static DbMethodCallExpression MakeNextValueForSequenceDbExpression(Type retType, string sequenceName)
        {
            MethodInfo nextValueForSequenceMethod = UtilConstants.MethodInfo_Sql_NextValueForSequence.MakeGenericMethod(retType);
            List<DbExpression> arguments = new List<DbExpression>() { new DbConstantExpression(sequenceName) };

            DbMethodCallExpression getNextValueForSequenceExp = new DbMethodCallExpression(null, nextValueForSequenceMethod, arguments);
            return getNextValueForSequenceExp;
        }
        public static object ConvertObjType(object obj, Type conversionType)
        {
            if (obj == null)
                return null;

            conversionType = conversionType.GetUnderlyingType();
            if (obj.GetType() != conversionType)
                return Convert.ChangeType(obj, conversionType);

            return obj;
        }

        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(Dictionary<TKey, TValue> source)
        {
            Dictionary<TKey, TValue> ret = Clone<TKey, TValue>(source, source.Count);
            return ret;
        }
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(Dictionary<TKey, TValue> source, int capacity)
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(capacity);

            foreach (var kv in source)
            {
                ret.Add(kv.Key, kv.Value);
            }

            return ret;
        }

        public static void EnsureHasPrimaryKey(TypeDescriptor typeDescriptor)
        {
            if (!typeDescriptor.HasPrimaryKey())
                throw new ChloeException(string.Format("The entity type '{0}' does not define any primary key.", typeDescriptor.Definition.Type.FullName));
        }

        public static DbParam[] BuildParams(DbContext dbContext, object parameter)
        {
            if (parameter == null)
                return new DbParam[0];

            if (parameter is IEnumerable<DbParam>)
            {
                return ((IEnumerable<DbParam>)parameter).ToArray();
            }

            List<DbParam> parameters = new List<DbParam>();
            Type parameterType = parameter.GetType();
            var props = parameterType.GetProperties();
            foreach (var prop in props)
            {
                if (prop.GetGetMethod() == null || !MappingTypeSystem.IsMappingType(prop.GetMemberType()))
                {
                    continue;
                }

                object value = ReflectionExtension.GetMemberValue(prop, parameter);

                string paramName = dbContext.DatabaseProvider.CreateParameterName(prop.Name);

                DbParam p = new DbParam(paramName, value, prop.PropertyType);
                parameters.Add(p);
            }

            return parameters.ToArray();
        }

    }
}
