using Chloe.Core;
using Chloe.Core.Visitors;
using Chloe.DbExpressions;
using Chloe.Descriptors;
using Chloe.Entity;
using Chloe.Exceptions;
using Chloe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chloe.SQLite
{
    public class SQLiteContext : DbContext
    {
        DbContextServiceProvider _dbContextServiceProvider;
        public SQLiteContext(IDbConnectionFactory dbConnectionFactory)
        {
            Utils.CheckNull(dbConnectionFactory);

            this._dbContextServiceProvider = new DbContextServiceProvider(dbConnectionFactory);
        }

        public override IDbContextServiceProvider DbContextServiceProvider
        {
            get { return this._dbContextServiceProvider; }
        }

        public override T Insert<T>(T entity)
        {
            Utils.CheckNull(entity);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(entity.GetType());
            EnsureMappingTypeHasPrimaryKey(typeDescriptor);

            MappingMemberDescriptor keyMemberDescriptor = typeDescriptor.PrimaryKey;
            MemberInfo keyMember = typeDescriptor.PrimaryKey.MemberInfo;

            object keyValue = null;

            MappingMemberDescriptor autoIncrementMemberDescriptor = GetAutoIncrementMemberDescriptor(typeDescriptor);

            Dictionary<MappingMemberDescriptor, DbExpression> insertColumns = new Dictionary<MappingMemberDescriptor, DbExpression>();
            foreach (var kv in typeDescriptor.MappingMemberDescriptors)
            {
                MemberInfo member = kv.Key;
                MappingMemberDescriptor memberDescriptor = kv.Value;

                if (memberDescriptor == autoIncrementMemberDescriptor)
                    continue;

                var val = memberDescriptor.GetValue(entity);

                if (memberDescriptor == keyMemberDescriptor)
                {
                    keyValue = val;
                }

                DbExpression valExp = DbExpression.Parameter(val, memberDescriptor.MemberInfoType);
                insertColumns.Add(memberDescriptor, valExp);
            }

            //主键为空并且主键又不是自增列
            if (keyValue == null && keyMemberDescriptor != autoIncrementMemberDescriptor)
            {
                throw new ChloeException(string.Format("The primary key '{0}' could not be null.", keyMemberDescriptor.MemberInfo.Name));
            }

            DbInsertExpression e = new DbInsertExpression(typeDescriptor.Table);

            foreach (var kv in insertColumns)
            {
                e.InsertColumns.Add(kv.Key.Column, kv.Value);
            }

            if (autoIncrementMemberDescriptor == null)
            {
                this.ExecuteSqlCommand(e);
                return entity;
            }

            IDbExpressionTranslator translator = this.DbContextServiceProvider.CreateDbExpressionTranslator();
            List<DbParam> parameters;
            string sql = translator.Translate(e, out parameters);

            sql += ";SELECT LAST_INSERT_ROWID()";

            //SELECT @@IDENTITY 返回的是 decimal 类型
            object retIdentity = this.CurrentSession.ExecuteScalar(sql, parameters.ToArray());

            if (retIdentity == null || retIdentity == DBNull.Value)
            {
                throw new ChloeException("Unable to get the identity value.");
            }

            retIdentity = ConvertIdentityType(retIdentity, autoIncrementMemberDescriptor.MemberInfoType);
            autoIncrementMemberDescriptor.SetValue(entity, retIdentity);
            return entity;
        }
        public override object Insert<T>(Expression<Func<T>> body)
        {
            Utils.CheckNull(body);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(typeof(T));
            EnsureMappingTypeHasPrimaryKey(typeDescriptor);

            MappingMemberDescriptor keyMemberDescriptor = typeDescriptor.PrimaryKey;
            MappingMemberDescriptor autoIncrementMemberDescriptor = GetAutoIncrementMemberDescriptor(typeDescriptor);

            Dictionary<MemberInfo, Expression> insertColumns = InitMemberExtractor.Extract(body);

            DbInsertExpression e = new DbInsertExpression(typeDescriptor.Table);

            object keyVal = null;

            foreach (var kv in insertColumns)
            {
                MemberInfo key = kv.Key;
                MappingMemberDescriptor memberDescriptor = typeDescriptor.TryGetMappingMemberDescriptor(key);

                if (memberDescriptor == null)
                    throw new ChloeException(string.Format("The member '{0}' does not map any column.", key.Name));

                if (memberDescriptor == autoIncrementMemberDescriptor)
                    throw new ChloeException(string.Format("Could not insert value into the identity column '{0}'.", memberDescriptor.Column.Name));

                if (memberDescriptor.IsPrimaryKey)
                {
                    object val = ExpressionEvaluator.Evaluate(kv.Value);
                    if (val == null)
                        throw new ChloeException(string.Format("The primary key '{0}' could not be null.", memberDescriptor.MemberInfo.Name));
                    else
                    {
                        keyVal = val;
                        e.InsertColumns.Add(memberDescriptor.Column, DbExpression.Parameter(keyVal));
                        continue;
                    }
                }

                e.InsertColumns.Add(memberDescriptor.Column, typeDescriptor.Visitor.Visit(kv.Value));
            }

            //主键为空并且主键又不是自增列
            if (keyVal == null && keyMemberDescriptor != autoIncrementMemberDescriptor)
            {
                throw new ChloeException(string.Format("The primary key '{0}' could not be null.", keyMemberDescriptor.MemberInfo.Name));
            }

            if (autoIncrementMemberDescriptor == null)
            {
                this.ExecuteSqlCommand(e);
                return keyVal;
            }

            IDbExpressionTranslator translator = this.DbContextServiceProvider.CreateDbExpressionTranslator();
            List<DbParam> parameters;
            string sql = translator.Translate(e, out parameters);
            sql += ";SELECT LAST_INSERT_ROWID()";

            //SELECT @@IDENTITY 返回的是 decimal 类型
            object retIdentity = this.CurrentSession.ExecuteScalar(sql, parameters.ToArray());

            if (retIdentity == null || retIdentity == DBNull.Value)
            {
                throw new ChloeException("Unable to get the identity value.");
            }

            retIdentity = ConvertIdentityType(retIdentity, autoIncrementMemberDescriptor.MemberInfoType);
            return retIdentity;
        }

        public override int Update<T>(T entity)
        {
            Utils.CheckNull(entity);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(entity.GetType());
            EnsureMappingTypeHasPrimaryKey(typeDescriptor);

            object keyVal = null;
            MappingMemberDescriptor keyMemberDescriptor = typeDescriptor.PrimaryKey;
            MemberInfo keyMember = keyMemberDescriptor.MemberInfo;

            IEntityState entityState = this.TryGetTrackedEntityState(entity);
            Dictionary<MappingMemberDescriptor, DbExpression> updateColumns = new Dictionary<MappingMemberDescriptor, DbExpression>();
            foreach (var kv in typeDescriptor.MappingMemberDescriptors)
            {
                MemberInfo member = kv.Key;
                MappingMemberDescriptor memberDescriptor = kv.Value;

                if (member == keyMember)
                {
                    keyVal = memberDescriptor.GetValue(entity);
                    keyMemberDescriptor = memberDescriptor;
                    continue;
                }

                AutoIncrementAttribute attr = (AutoIncrementAttribute)memberDescriptor.GetCustomAttribute(typeof(AutoIncrementAttribute));
                if (attr != null)
                    continue;

                var val = memberDescriptor.GetValue(entity);

                if (entityState != null && !entityState.HasChanged(memberDescriptor, val))
                    continue;

                DbExpression valExp = DbExpression.Parameter(val, memberDescriptor.MemberInfoType);
                updateColumns.Add(memberDescriptor, valExp);
            }

            if (keyVal == null)
                throw new ChloeException(string.Format("The primary key '{0}' could not be null.", keyMember.Name));

            if (updateColumns.Count == 0)
                return 0;

            DbExpression left = new DbColumnAccessExpression(typeDescriptor.Table, keyMemberDescriptor.Column);
            DbExpression right = DbExpression.Parameter(keyVal, keyMemberDescriptor.MemberInfoType);
            DbExpression conditionExp = new DbEqualExpression(left, right);

            DbUpdateExpression e = new DbUpdateExpression(typeDescriptor.Table, conditionExp);

            foreach (var item in updateColumns)
            {
                e.UpdateColumns.Add(item.Key.Column, item.Value);
            }

            int ret = this.ExecuteSqlCommand(e);
            if (entityState != null)
                entityState.Refresh();
            return ret;
        }
        public override int Update<T>(Expression<Func<T, T>> body, Expression<Func<T, bool>> condition)
        {
            Utils.CheckNull(body);
            Utils.CheckNull(condition);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(typeof(T));

            Dictionary<MemberInfo, Expression> updateColumns = InitMemberExtractor.Extract(body);
            DbExpression conditionExp = typeDescriptor.Visitor.VisitFilterPredicate(condition);

            DbUpdateExpression e = new DbUpdateExpression(typeDescriptor.Table, conditionExp);

            foreach (var kv in updateColumns)
            {
                MemberInfo key = kv.Key;
                MappingMemberDescriptor memberDescriptor = typeDescriptor.TryGetMappingMemberDescriptor(key);

                if (memberDescriptor == null)
                    throw new ChloeException(string.Format("The member '{0}' does not map any column.", key.Name));

                if (memberDescriptor.IsPrimaryKey)
                    throw new ChloeException(string.Format("Could not update the primary key '{0}'.", memberDescriptor.Column.Name));

                AutoIncrementAttribute attr = (AutoIncrementAttribute)memberDescriptor.GetCustomAttribute(typeof(AutoIncrementAttribute));
                if (attr != null)
                    throw new ChloeException(string.Format("Could not update the identity column '{0}'.", memberDescriptor.Column.Name));

                e.UpdateColumns.Add(memberDescriptor.Column, typeDescriptor.Visitor.Visit(kv.Value));
            }

            return this.ExecuteSqlCommand(e);
        }

        int ExecuteSqlCommand(DbExpression e)
        {
            IDbExpressionTranslator translator = this.DbContextServiceProvider.CreateDbExpressionTranslator();
            List<DbParam> parameters;
            string sql = translator.Translate(e, out parameters);

            int r = this.CurrentSession.ExecuteNonQuery(sql, parameters.ToArray());
            return r;
        }

        static void EnsureMappingTypeHasPrimaryKey(TypeDescriptor typeDescriptor)
        {
            if (!typeDescriptor.HasPrimaryKey())
                throw new ChloeException(string.Format("Mapping type '{0}' does not define a primary key.", typeDescriptor.EntityType.FullName));
        }

        static MappingMemberDescriptor GetAutoIncrementMemberDescriptor(TypeDescriptor typeDescriptor)
        {
            List<MappingMemberDescriptor> autoIncrementMemberDescriptors = typeDescriptor.MappingMemberDescriptors.Values.Where(a =>
            {
                AutoIncrementAttribute attr = (AutoIncrementAttribute)a.GetCustomAttribute(typeof(AutoIncrementAttribute));
                return attr != null;
            }).ToList();

            if (autoIncrementMemberDescriptors.Count > 1)
                throw new ChloeException(string.Format("Mapping type '{0}' can not define multiple identity members.", typeDescriptor.EntityType.FullName));

            MappingMemberDescriptor autoIncrementMemberDescriptor = autoIncrementMemberDescriptors.FirstOrDefault();

            if (autoIncrementMemberDescriptor != null)
                EnsureAutoIncrementMemberType(autoIncrementMemberDescriptor);

            return autoIncrementMemberDescriptor;
        }
        static void EnsureAutoIncrementMemberType(MappingMemberDescriptor autoIncrementMemberDescriptor)
        {
            if (autoIncrementMemberDescriptor.MemberInfoType != UtilConstants.TypeOfInt16 && autoIncrementMemberDescriptor.MemberInfoType != UtilConstants.TypeOfInt32 && autoIncrementMemberDescriptor.MemberInfoType != UtilConstants.TypeOfInt64)
            {
                throw new ChloeException("Identity type must be Int16,Int32 or Int64.");
            }
        }
        static object ConvertIdentityType(object identity, Type conversionType)
        {
            if (identity.GetType() != conversionType)
                return Convert.ChangeType(identity, conversionType);

            return identity;
        }

    }
}
