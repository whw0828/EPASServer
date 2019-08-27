using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Core.Emit
{
    public static class EmitHelper
    {
        public static void SetValueIL(ILGenerator il, MemberInfo member)
        {
            MemberTypes memberType = member.MemberType;
            if (memberType == MemberTypes.Property)
            {
                MethodInfo setter = ((PropertyInfo)member).GetSetMethod();
                il.EmitCall(OpCodes.Callvirt, setter, null);//给属性赋值
            }
            else if (memberType == MemberTypes.Field)
            {
                il.Emit(OpCodes.Stfld, ((FieldInfo)member));//给字段赋值
            }
            else
                throw new NotSupportedException();
        }
        public static void SetValueIL(ILGenerator il, PropertyInfo property)
        {
            MethodInfo setter = property.GetSetMethod();
            il.EmitCall(OpCodes.Callvirt, setter, null);//给属性赋值
        }
        public static void SetValueIL(ILGenerator il, FieldInfo field)
        {
            il.Emit(OpCodes.Stfld, field);//给字段赋值
        }

    }
}
