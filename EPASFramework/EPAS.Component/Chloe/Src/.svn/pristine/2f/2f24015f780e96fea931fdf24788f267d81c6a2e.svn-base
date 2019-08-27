using Chloe.Core;
using Chloe.Core.Emit;
using Chloe.Query.Mapping;
using Chloe.InternalExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Chloe.Infrastructure;
using Chloe.Utility;

namespace Chloe.Mapper
{
    public class EntityMemberMapper
    {
        Dictionary<MemberInfo, IMRM> _mappingMemberMappers;
        Dictionary<MemberInfo, Action<object, object>> _complexMemberSetters;

        EntityMemberMapper(Type t)
        {
            this.Type = t;
            this.Init();
        }

        void Init()
        {
            Type t = this.Type;
            var members = t.GetMembers(BindingFlags.Public | BindingFlags.Instance);

            Dictionary<MemberInfo, IMRM> mappingMemberMappers = new Dictionary<MemberInfo, IMRM>();
            Dictionary<MemberInfo, Action<object, object>> complexMemberSetters = new Dictionary<MemberInfo, Action<object, object>>();

            foreach (var member in members)
            {
                if (!member.HasPublicSetter())
                {
                    continue;
                }

                //只支持公共属性和字段
                Type memberType = member.GetMemberType();

                MappingTypeInfo mappingTypeInfo;
                if (MappingTypeSystem.IsMappingType(memberType, out mappingTypeInfo))
                {
                    IMRM mrm = MRMHelper.CreateMRM(member, mappingTypeInfo);
                    mappingMemberMappers.Add(member, mrm);
                }
                else
                {
                    Action<object, object> valueSetter = DelegateGenerator.CreateValueSetter(member);
                    complexMemberSetters.Add(member, valueSetter);
                }
            }

            this._mappingMemberMappers = PublicHelper.Clone(mappingMemberMappers);
            this._complexMemberSetters = PublicHelper.Clone(complexMemberSetters);
        }

        public Type Type { get; private set; }

        public IMRM TryGetMappingMemberMapper(MemberInfo memberInfo)
        {
            memberInfo = memberInfo.AsReflectedMemberOf(this.Type);
            IMRM mapper = null;
            this._mappingMemberMappers.TryGetValue(memberInfo, out mapper);
            return mapper;
        }
        public Action<object, object> TryGetComplexMemberSetter(MemberInfo memberInfo)
        {
            memberInfo = memberInfo.AsReflectedMemberOf(this.Type);
            Action<object, object> valueSetter = null;
            this._complexMemberSetters.TryGetValue(memberInfo, out valueSetter);
            return valueSetter;
        }

        static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, EntityMemberMapper> InstanceCache = new System.Collections.Concurrent.ConcurrentDictionary<Type, EntityMemberMapper>();

        public static EntityMemberMapper GetInstance(Type type)
        {
            EntityMemberMapper instance;
            if (!InstanceCache.TryGetValue(type, out instance))
            {
                lock (type)
                {
                    if (!InstanceCache.TryGetValue(type, out instance))
                    {
                        instance = new EntityMemberMapper(type);
                        InstanceCache.GetOrAdd(type, instance);
                    }
                }
            }

            return instance;
        }
    }
}
