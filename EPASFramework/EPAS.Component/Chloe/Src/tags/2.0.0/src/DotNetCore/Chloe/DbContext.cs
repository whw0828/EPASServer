using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Chloe.Query;
using Chloe.Core;
using Chloe.Utility;
using Chloe.Infrastructure;
using Chloe.Descriptors;
using Chloe.DbExpressions;
using Chloe.Query.Internals;
using Chloe.Core.Visitors;
using Chloe.Exceptions;

namespace Chloe
{
    public abstract class DbContext : IDbContext, IDisposable
    {
        bool _disposed = false;
        InternalDbSession _innerDbSession;
        DbSession _currentSession;

        Dictionary<Type, TrackEntityCollection> _trackingEntityContainer;

        Dictionary<Type, TrackEntityCollection> TrackingEntityContainer
        {
            get
            {
                if (this._trackingEntityContainer == null)
                {
                    this._trackingEntityContainer = new Dictionary<Type, TrackEntityCollection>();
                }

                return this._trackingEntityContainer;
            }
        }

        internal InternalDbSession InnerDbSession
        {
            get
            {
                this.CheckDisposed();
                if (this._innerDbSession == null)
                    this._innerDbSession = new InternalDbSession(this.DbContextServiceProvider.CreateConnection());
                return this._innerDbSession;
            }
        }
        public abstract IDbContextServiceProvider DbContextServiceProvider { get; }

        protected DbContext()
        {
            this._currentSession = new DbSession(this);
        }

        public IDbSession CurrentSession { get { return this._currentSession; } }


        public virtual IQuery<T> Query<T>() where T : new()
        {
            return new Query<T>(this);
        }

        public virtual IEnumerable<T> SqlQuery<T>(string sql, params DbParam[] parameters) where T : new()
        {
            Utils.CheckNull(sql, "sql");

            return new InternalSqlQuery<T>(this, sql, parameters);
        }

        public virtual T Insert<T>(T entity)
        {
            Utils.CheckNull(entity);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(entity.GetType());
            EnsureEntityHasPrimaryKey(typeDescriptor);

            MappingMemberDescriptor keyMemberDescriptor = typeDescriptor.PrimaryKey;
            MemberInfo keyMember = typeDescriptor.PrimaryKey.MemberInfo;

            object keyValue = null;

            MappingMemberDescriptor autoIncrementMemberDescriptor = typeDescriptor.AutoIncrement;

            Dictionary<MappingMemberDescriptor, DbExpression> insertColumns = new Dictionary<MappingMemberDescriptor, DbExpression>();
            foreach (var kv in typeDescriptor.MappingMemberDescriptors)
            {
                MemberInfo member = kv.Key;
                MappingMemberDescriptor memberDescriptor = kv.Value;

                if (memberDescriptor == autoIncrementMemberDescriptor)
                    continue;

                object val = memberDescriptor.GetValue(entity);

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

            sql = string.Concat(sql, ";", this.GetSelectLastInsertIdClause());

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
        public virtual object Insert<T>(Expression<Func<T>> body)
        {
            Utils.CheckNull(body);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(typeof(T));
            EnsureEntityHasPrimaryKey(typeDescriptor);

            MappingMemberDescriptor keyMemberDescriptor = typeDescriptor.PrimaryKey;
            MappingMemberDescriptor autoIncrementMemberDescriptor = typeDescriptor.AutoIncrement;

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

            if (keyMemberDescriptor != autoIncrementMemberDescriptor)
            {
                this.ExecuteSqlCommand(e);
                return keyVal;
            }

            IDbExpressionTranslator translator = this.DbContextServiceProvider.CreateDbExpressionTranslator();
            List<DbParam> parameters;
            string sql = translator.Translate(e, out parameters);
            sql = string.Concat(sql, ";", this.GetSelectLastInsertIdClause());

            //SELECT @@IDENTITY 返回的是 decimal 类型
            object retIdentity = this.CurrentSession.ExecuteScalar(sql, parameters.ToArray());

            if (retIdentity == null || retIdentity == DBNull.Value)
            {
                throw new ChloeException("Unable to get the identity value.");
            }

            retIdentity = ConvertIdentityType(retIdentity, autoIncrementMemberDescriptor.MemberInfoType);
            return retIdentity;
        }

        public virtual int Update<T>(T entity)
        {
            Utils.CheckNull(entity);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(entity.GetType());
            EnsureEntityHasPrimaryKey(typeDescriptor);

            MappingMemberDescriptor keyMemberDescriptor = typeDescriptor.PrimaryKey;
            MemberInfo keyMember = keyMemberDescriptor.MemberInfo;

            object keyVal = null;

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

                if (memberDescriptor.IsAutoIncrement)
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
        public virtual int Update<T>(Expression<Func<T, T>> body, Expression<Func<T, bool>> condition)
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

                if (memberDescriptor.IsAutoIncrement)
                    throw new ChloeException(string.Format("Could not update the identity column '{0}'.", memberDescriptor.Column.Name));

                e.UpdateColumns.Add(memberDescriptor.Column, typeDescriptor.Visitor.Visit(kv.Value));
            }

            if (e.UpdateColumns.Count == 0)
                return 0;

            return this.ExecuteSqlCommand(e);
        }

        public virtual int Delete<T>(T entity)
        {
            Utils.CheckNull(entity);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(entity.GetType());
            EnsureEntityHasPrimaryKey(typeDescriptor);

            MappingMemberDescriptor keyMemberDescriptor = typeDescriptor.PrimaryKey;
            MemberInfo keyMember = typeDescriptor.PrimaryKey.MemberInfo;

            var keyVal = keyMemberDescriptor.GetValue(entity);

            if (keyVal == null)
                throw new ChloeException(string.Format("The primary key '{0}' could not be null.", keyMember.Name));

            DbExpression left = new DbColumnAccessExpression(typeDescriptor.Table, keyMemberDescriptor.Column);
            DbExpression right = new DbParameterExpression(keyVal);
            DbExpression conditionExp = new DbEqualExpression(left, right);

            DbDeleteExpression e = new DbDeleteExpression(typeDescriptor.Table, conditionExp);
            return this.ExecuteSqlCommand(e);
        }
        public virtual int Delete<T>(Expression<Func<T, bool>> condition)
        {
            Utils.CheckNull(condition);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(typeof(T));
            DbExpression conditionExp = typeDescriptor.Visitor.VisitFilterPredicate(condition);

            DbDeleteExpression e = new DbDeleteExpression(typeDescriptor.Table, conditionExp);

            return this.ExecuteSqlCommand(e);
        }

        public virtual void TrackEntity(object entity)
        {
            Utils.CheckNull(entity);
            Type entityType = entity.GetType();

            if (Utils.IsAnonymousType(entityType))
                return;

            Dictionary<Type, TrackEntityCollection> entityContainer = this.TrackingEntityContainer;

            TrackEntityCollection collection;
            if (!entityContainer.TryGetValue(entityType, out collection))
            {
                TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(entityType);

                if (!typeDescriptor.HasPrimaryKey())
                    return;

                collection = new TrackEntityCollection(typeDescriptor);
                entityContainer.Add(entityType, collection);
            }

            collection.TryAddEntity(entity);
        }
        protected virtual string GetSelectLastInsertIdClause()
        {
            return "SELECT @@IDENTITY";
        }
        protected virtual IEntityState TryGetTrackedEntityState(object entity)
        {
            Utils.CheckNull(entity);
            Type entityType = entity.GetType();
            Dictionary<Type, TrackEntityCollection> entityContainer = this._trackingEntityContainer;

            if (entityContainer == null)
                return null;

            TrackEntityCollection collection;
            if (!entityContainer.TryGetValue(entityType, out collection))
            {
                return null;
            }

            IEntityState ret = collection.TryGetEntityState(entity);
            return ret;
        }

        public void Dispose()
        {
            if (this._disposed)
                return;

            if (this._innerDbSession != null)
                this._innerDbSession.Dispose();
            this.Dispose(true);
            this._disposed = true;
        }
        protected virtual void Dispose(bool disposing)
        {

        }
        void CheckDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }


        int ExecuteSqlCommand(DbExpression e)
        {
            IDbExpressionTranslator translator = this.DbContextServiceProvider.CreateDbExpressionTranslator();
            List<DbParam> parameters;
            string cmdText = translator.Translate(e, out parameters);

            int r = this.InnerDbSession.ExecuteNonQuery(cmdText, parameters.ToArray());
            return r;
        }

        static void EnsureEntityHasPrimaryKey(TypeDescriptor typeDescriptor)
        {
            if (!typeDescriptor.HasPrimaryKey())
                throw new ChloeException(string.Format("Mapping type '{0}' does not define a primary key.", typeDescriptor.EntityType.FullName));
        }
        static object ConvertIdentityType(object identity, Type conversionType)
        {
            if (identity.GetType() != conversionType)
                return Convert.ChangeType(identity, conversionType);

            return identity;
        }


        class TrackEntityCollection
        {
            public TrackEntityCollection(TypeDescriptor typeDescriptor)
            {
                this.TypeDescriptor = typeDescriptor;
                this.Entities = new Dictionary<object, IEntityState>(1);
            }
            public TypeDescriptor TypeDescriptor { get; private set; }
            public Dictionary<object, IEntityState> Entities { get; private set; }
            public bool TryAddEntity(object entity)
            {
                if (this.Entities.ContainsKey(entity))
                {
                    return false;
                }

                IEntityState entityState = new EntityState(this.TypeDescriptor, entity);
                this.Entities.Add(entity, entityState);

                return true;
            }
            public IEntityState TryGetEntityState(object entity)
            {
                IEntityState ret;
                if (!this.Entities.TryGetValue(entity, out ret))
                    ret = null;

                return ret;
            }
        }
    }
}
