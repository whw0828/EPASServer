using System;
using DapperExtensions.Attributes;
using DapperExtensions.Test.Data;
using DapperExtensions.Validators;
using NUnit.Framework;

namespace DapperExtensions.Test.Validators
{
    [TestFixture]
    public class MaxLengthValidatorFixture
    {
        public class Place
        {
            public string Name { get; set; }

            [MaxLength(10)]
            public string Description { get; set; }
        }

        [Test]
        public void MaxLengthValidator_Respects_MaxLength_Attribute_When_Length_Greater_Than_MaxLength()
        {
            Place p = new Place();
            p.Name = "Foo";
            p.Description = new string('a', 11);

            MaxLengthValidator maxLengthValidator = new MaxLengthValidator();
            Assert.Throws<InvalidOperationException>(() => maxLengthValidator.Validate(p));
        }

        [Test]
        public void MaxLengthValidator_Respects_MaxLength_Attribute_When_Length_Equal_To_MaxLength()
        {
            Place p = new Place();
            p.Name = "Foo";
            p.Description = new string('a', 10);

            MaxLengthValidator maxLengthValidator = new MaxLengthValidator();
            maxLengthValidator.Validate(p);
        }

        [Test]
        public void MaxLengthValidator_Respects_MaxLength_Attribute_When_Value_Is_Null()
        {
            Place p = new Place();
            p.Name = "Foo";
            p.Description = null;

            MaxLengthValidator maxLengthValidator = new MaxLengthValidator();
            maxLengthValidator.Validate(p);
        }
    }
}