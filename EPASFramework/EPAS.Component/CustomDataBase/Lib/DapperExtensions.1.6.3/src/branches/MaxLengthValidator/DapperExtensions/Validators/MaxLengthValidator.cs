using System;
using System.Collections.Generic;
using System.Reflection;
using DapperExtensions.Attributes;

namespace DapperExtensions.Validators
{
    public class MaxLengthValidator
    {
        private class MaxLengthProperty
        {
            public PropertyInfo PropertyInfo { get; set; }
            public int MaxLength { get; set; }
        }

        private readonly Dictionary<Type, List<MaxLengthProperty>> PropertyCache;

        public MaxLengthValidator()
        {
            PropertyCache = new Dictionary<Type, List<MaxLengthProperty>>();
        }

        public void Validate<T>(T entity)
        {
            List<MaxLengthProperty> maxLengthProperties = null;
            if (!PropertyCache.TryGetValue(typeof(T), out maxLengthProperties))
            {
                maxLengthProperties = new List<MaxLengthProperty>();
                var properties = typeof(T).GetProperties();
                foreach (var pi in properties)
                {
                    var attributes = pi.GetCustomAttributes(typeof(MaxLengthAttribute), false);

                    if (attributes.Length == 0)
                    {
                        continue;
                    }

                    var maxLengthAttribute = attributes[0] as MaxLengthAttribute;
                    maxLengthProperties.Add(new MaxLengthProperty
                    {
                        PropertyInfo = pi,
                        MaxLength = maxLengthAttribute.MaxLength
                    });
                }
            }

            foreach (var maxLengthProperty in maxLengthProperties)
            {
                var val = maxLengthProperty.PropertyInfo.GetValue(entity, null) as string;
                if (val == null)
                {
                    continue;
                }

                if (val.Length > maxLengthProperty.MaxLength)
                {
                    var error = string.Format("Value for field {0} exceeds length {1}", 
                                               maxLengthProperty.PropertyInfo.Name,
                                               maxLengthProperty.MaxLength);
                    throw new InvalidOperationException(error);
                }
            }
        }
    }
}
