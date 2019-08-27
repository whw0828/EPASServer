using Chloe.DbExpressions;
using Chloe.InternalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chloe.SqlServer
{
    internal static class Utils
    {
        static readonly Dictionary<Type, Type> ToStringableNumericTypes;

        static Utils()
        {
            List<Type> toStringableNumericTypes = new List<Type>();
            toStringableNumericTypes.Add(typeof(byte));
            toStringableNumericTypes.Add(typeof(sbyte));
            toStringableNumericTypes.Add(typeof(short));
            toStringableNumericTypes.Add(typeof(ushort));
            toStringableNumericTypes.Add(typeof(int));
            toStringableNumericTypes.Add(typeof(uint));
            toStringableNumericTypes.Add(typeof(long));
            toStringableNumericTypes.Add(typeof(ulong));
            ToStringableNumericTypes = toStringableNumericTypes.ToDictionary(a => a, a => a);
        }

        public static string QuoteName(string name)
        {
            return string.Concat("[", name, "]");
        }

        public static bool IsToStringableNumericType(Type type)
        {
            type = ReflectionExtension.GetUnderlyingType(type);
            return ToStringableNumericTypes.ContainsKey(type);
        }
    }
}
