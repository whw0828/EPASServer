using System;
using System.Collections.Generic;
using System.Data;
using Chloe.Core;
using Chloe.InternalExtensions;
using Chloe.Utility;

namespace Chloe.Infrastructure
{
    public static class MappingTypeSystem
    {
        static readonly object _lockObj = new object();

        static readonly Dictionary<Type, MappingTypeInfo> _defaultTypeInfos;
        static readonly Dictionary<Type, MappingTypeInfo> _typeInfos;

        static MappingTypeSystem()
        {
            Dictionary<Type, MappingTypeInfo> defaultTypeInfos = new Dictionary<Type, MappingTypeInfo>();
            SetItem(defaultTypeInfos, typeof(byte), DbType.Byte);
            SetItem(defaultTypeInfos, typeof(sbyte), DbType.SByte);
            SetItem(defaultTypeInfos, typeof(short), DbType.Int16);
            SetItem(defaultTypeInfos, typeof(ushort), DbType.UInt16);
            SetItem(defaultTypeInfos, typeof(int), DbType.Int32);
            SetItem(defaultTypeInfos, typeof(uint), DbType.UInt32);
            SetItem(defaultTypeInfos, typeof(long), DbType.Int64);
            SetItem(defaultTypeInfos, typeof(ulong), DbType.UInt64);
            SetItem(defaultTypeInfos, typeof(float), DbType.Single);
            SetItem(defaultTypeInfos, typeof(double), DbType.Double);
            SetItem(defaultTypeInfos, typeof(decimal), DbType.Decimal);
            SetItem(defaultTypeInfos, typeof(bool), DbType.Boolean);
            SetItem(defaultTypeInfos, typeof(string), DbType.String);
            SetItem(defaultTypeInfos, typeof(Guid), DbType.Guid);
            SetItem(defaultTypeInfos, typeof(DateTime), DbType.DateTime);
            SetItem(defaultTypeInfos, typeof(DateTimeOffset), DbType.DateTimeOffset);
            SetItem(defaultTypeInfos, typeof(TimeSpan), DbType.Time);
            SetItem(defaultTypeInfos, typeof(byte[]), DbType.Binary);

            _typeInfos = PublicHelper.Clone(defaultTypeInfos);
            _defaultTypeInfos = PublicHelper.Clone(defaultTypeInfos);
        }

        static void SetItem(Dictionary<Type, MappingTypeInfo> map, Type type, DbType mapDbType, IMappingType mappingType = null)
        {
            map[type] = new MappingTypeInfo(type, mapDbType, mappingType);
        }

        /// <summary>
        /// 配置映射的类型。
        /// </summary>
        /// <param name="mappingType"></param>
        public static void Configure(IMappingType mappingType)
        {
            Utils.CheckNull(mappingType);

            lock (_lockObj)
            {
                SetItem(_typeInfos, mappingType.Type, mappingType.DbType, mappingType);
            }
        }

        public static DbType? GetDbType(Type type)
        {
            if (type == null)
                return null;

            Type underlyingType = type.GetUnderlyingType();
            if (underlyingType.IsEnum)
                underlyingType = Enum.GetUnderlyingType(underlyingType);

            MappingTypeInfo mappingTypeInfo;
            if (_typeInfos.TryGetValue(underlyingType, out mappingTypeInfo))
                return mappingTypeInfo.MapDbType;

            return null;
        }
        public static MappingTypeInfo GetMappingTypeInfo(Type type)
        {
            MappingTypeInfo mappingTypeInfo;
            IsMappingType(type, out mappingTypeInfo);
            return mappingTypeInfo;
        }
        public static bool IsMappingType(Type type)
        {
            Type underlyingType = type.GetUnderlyingType();
            if (underlyingType.IsEnum)
                return true;

            return _typeInfos.ContainsKey(underlyingType);
        }
        public static bool IsMappingType(Type type, out MappingTypeInfo mappingTypeInfo)
        {
            Type underlyingType = type.GetUnderlyingType();
            if (underlyingType.IsEnum)
                underlyingType = Enum.GetUnderlyingType(underlyingType);

            return _typeInfos.TryGetValue(underlyingType, out mappingTypeInfo);
        }
    }

    public class MappingTypeInfo
    {
        public MappingTypeInfo(Type type, DbType mapDbType, IMappingType mappingType)
        {
            this.Type = type;
            this.MapDbType = mapDbType;
            this.MappingType = mappingType;
        }
        public Type Type { get; private set; }
        public DbType MapDbType { get; private set; }
        public IMappingType MappingType { get; private set; }
    }
}
