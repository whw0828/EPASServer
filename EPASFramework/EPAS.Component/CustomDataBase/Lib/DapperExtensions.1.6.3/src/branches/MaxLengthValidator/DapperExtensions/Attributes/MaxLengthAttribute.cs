using System;

namespace DapperExtensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaxLengthAttribute : Attribute
    {
        public int MaxLength { get; set; }

        public MaxLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }
    }
}