using Chloe.Descriptors;
using Chloe.Entity;
using Chloe.SqlServer.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chloe.SqlServer
{
    public static class EntityPropertyBuilderExtension
    {
        /// <summary>
        /// 标识字段为 timestamp 类型
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entityPropertyBuilder"></param>
        /// <returns></returns>
        public static IEntityPropertyBuilder<TProperty> IsTimestamp<TProperty>(this IEntityPropertyBuilder<TProperty> entityPropertyBuilder)
        {
            return entityPropertyBuilder.HasAnnotation(new TimestampAttribute());
        }
        /// <summary>
        /// 标识字段为 timestamp 类型
        /// </summary>
        /// <param name="entityPropertyBuilder"></param>
        /// <returns></returns>
        public static IEntityPropertyBuilder IsTimestamp(this IEntityPropertyBuilder entityPropertyBuilder)
        {
            return entityPropertyBuilder.HasAnnotation(new TimestampAttribute());
        }
    }

    public static class PropertyDescriptorExtension
    {
        /// <summary>
        /// 判断字段是否为 timestamp 类型
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <returns></returns>
        public static bool IsTimestamp(this PropertyDescriptor propertyDescriptor)
        {
            return propertyDescriptor.HasAnnotation(UtilConstants.TypeOfTimestamp);
        }
    }
}
