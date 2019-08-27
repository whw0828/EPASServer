using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chloe.Extensions
{
    internal static class ReflectionExtensions
    {
        public static Type GetMemberInfoType(this MemberInfo member)
        {
            if (member == null)
                throw new ArgumentNullException("member");

            if (member.MemberType == MemberTypes.Property)
                return ((PropertyInfo)member).PropertyType;
            if (member.MemberType == MemberTypes.Field)
                return ((FieldInfo)member).FieldType;
            if (member is MethodInfo)
                return ((MethodInfo)member).ReturnType;
            if (member is ConstructorInfo)
                return ((ConstructorInfo)member).ReflectedType;

            return null;
        }

        public static Type GetPropertyOrFieldType(this MemberInfo propertyOrField)
        {
            if (propertyOrField.MemberType == MemberTypes.Property)
                return ((PropertyInfo)propertyOrField).PropertyType;
            if (propertyOrField.MemberType == MemberTypes.Field)
                return ((FieldInfo)propertyOrField).FieldType;

            throw new NotSupportedException();
        }

        public static void SetPropertyOrFieldValue(this MemberInfo propertyOrField, object obj, object value)
        {
            if (propertyOrField.MemberType == MemberTypes.Property)
                ((PropertyInfo)propertyOrField).SetValue(obj, value, null);
            else if (propertyOrField.MemberType == MemberTypes.Field)
                ((FieldInfo)propertyOrField).SetValue(obj, value);

            throw new ArgumentException();
        }

        public static object GetPropertyOrFieldValue(this MemberInfo propertyOrField, object obj)
        {
            if (propertyOrField.MemberType == MemberTypes.Property)
                return ((PropertyInfo)propertyOrField).GetValue(obj, null);
            else if (propertyOrField.MemberType == MemberTypes.Field)
                return ((FieldInfo)propertyOrField).GetValue(obj);

            throw new ArgumentException();
        }
    }
}
