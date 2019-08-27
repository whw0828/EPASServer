using System;

namespace Chloe.Entity
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    class AssociationAttribute : Attribute
    {
        /// <summary>
        /// 假设实体 Order 内有个导航属性 User， Order.UserId=User.Id，则 ThisKey 为 Order.UserId，AssociatingKey 为 User.Id
        /// </summary>
        /// <param name="thisKey">定义导航属性实体相关的属性或字段名称</param>
        /// <param name="associatingKey">关联导航属性实体类型的属性或字段名称</param>
        public AssociationAttribute(string thisKey, string associatingKey)
        {
            this.ThisKey = thisKey;
            this.AssociatingKey = associatingKey;
        }

        /// <summary>
        /// 假设实体 Order 内有个导航属性 User， Order.UserId=User.Id，则 ThisKey 为 Order.UserId，AssociatingKey 为 User.Id
        /// </summary>
        public string ThisKey { get; set; }
        public string AssociatingKey { get; set; }

    }
}
