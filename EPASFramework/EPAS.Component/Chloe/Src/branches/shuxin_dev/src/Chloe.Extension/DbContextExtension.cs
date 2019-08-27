using Chloe.Descriptors;
using Chloe.Exceptions;
using Chloe.Extension;
using Chloe.Infrastructure;
using Chloe.InternalExtensions;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chloe
{
    public static partial class DbContextExtension
    {
        public static IQuery<T> Query<T>(this IDbContext dbContext, Expression<Func<T, bool>> predicate)
        {
            return dbContext.Query<T>().Where(predicate);
        }

        /// <summary>
        /// dbContext.Update&lt;User&gt;(user, a =&gt; a.Id == 1)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int Update<TEntity>(this IDbContext dbContext, TEntity entity, Expression<Func<TEntity, bool>> condition)
        {
            PublicHelper.CheckNull(dbContext);
            PublicHelper.CheckNull(entity);
            PublicHelper.CheckNull(condition);

            Type entityType = typeof(TEntity);
            TypeDescriptor typeDescriptor = EntityTypeContainer.GetDescriptor(entityType);

            List<MemberBinding> bindings = new List<MemberBinding>();

            ConstantExpression entityConstantExp = Expression.Constant(entity);
            foreach (PropertyDescriptor propertyDescriptor in typeDescriptor.PropertyDescriptors)
            {
                if (propertyDescriptor.IsPrimaryKey || propertyDescriptor.IsAutoIncrement)
                {
                    continue;
                }

                Expression entityMemberAccess = Expression.MakeMemberAccess(entityConstantExp, propertyDescriptor.Property);
                MemberAssignment bind = Expression.Bind(propertyDescriptor.Property, entityMemberAccess);

                bindings.Add(bind);
            }

            return UpdateOnly(dbContext, condition, bindings);
        }

        /// <summary>
        /// dbContext.UpdateOnly&lt;User&gt;(user, a =&gt; new { a.Name, a.Age })
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static int UpdateOnly<TEntity>(this IDbContext dbContext, TEntity entity, Expression<Func<TEntity, object>> fields)
        {
            PublicHelper.CheckNull(fields);

            List<string> fieldList = FieldsResolver.Resolve(fields);
            return dbContext.UpdateOnly(entity, fieldList.ToArray());
        }
        /// <summary>
        /// dbContext.UpdateOnly&lt;User&gt;(user, "Name,Age", "NickName")
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static int UpdateOnly<TEntity>(this IDbContext dbContext, TEntity entity, params string[] fields)
        {
            PublicHelper.CheckNull(dbContext);
            PublicHelper.CheckNull(entity);
            PublicHelper.CheckNull(fields);

            /* 支持 context.UpdateOnly<User>(user, "Name,Age", "NickName"); */
            fields = fields.SelectMany(a => a.Split(',')).Select(a => a.Trim()).ToArray();

            if (fields.Length == 0)
                throw new ArgumentException("fields");

            Type entityType = typeof(TEntity);
            TypeDescriptor typeDescriptor = EntityTypeContainer.GetDescriptor(entityType);

            List<MemberBinding> bindings = new List<MemberBinding>();

            ConstantExpression entityConstantExp = Expression.Constant(entity);
            foreach (string field in fields)
            {
                MemberInfo memberInfo = entityType.GetMember(field)[0];
                var propertyDescriptor = typeDescriptor.TryGetPropertyDescriptor(memberInfo);

                if (propertyDescriptor == null)
                    throw new ArgumentException(string.Format("Could not find the member '{0}' from entity.", propertyDescriptor.Column.Name));

                Expression entityMemberAccess = Expression.MakeMemberAccess(entityConstantExp, memberInfo);
                MemberAssignment bind = Expression.Bind(memberInfo, entityMemberAccess);

                bindings.Add(bind);
            }

            ParameterExpression parameter = Expression.Parameter(entityType, "a");
            Expression conditionBody = null;
            foreach (PropertyDescriptor primaryKey in typeDescriptor.PrimaryKeys)
            {
                Expression propOrField = Expression.PropertyOrField(parameter, primaryKey.Property.Name);
                Expression keyValue = Expression.MakeMemberAccess(entityConstantExp, primaryKey.Property);
                Expression e = Expression.Equal(propOrField, keyValue);
                conditionBody = conditionBody == null ? e : Expression.AndAlso(conditionBody, e);
            }

            Expression<Func<TEntity, bool>> condition = Expression.Lambda<Func<TEntity, bool>>(conditionBody, parameter);

            return UpdateOnly(dbContext, condition, bindings);
        }
        static int UpdateOnly<TEntity>(this IDbContext dbContext, Expression<Func<TEntity, bool>> condition, List<MemberBinding> bindings)
        {
            Type entityType = typeof(TEntity);
            NewExpression newExp = Expression.New(entityType);

            ParameterExpression parameter = Expression.Parameter(entityType, "a");
            Expression lambdaBody = Expression.MemberInit(newExp, bindings);

            Expression<Func<TEntity, TEntity>> lambda = Expression.Lambda<Func<TEntity, TEntity>>(lambdaBody, parameter);

            return dbContext.Update(condition, lambda);
        }

        public static void BeginTransaction(this IDbContext dbContext)
        {
            dbContext.Session.BeginTransaction();
        }
        public static void BeginTransaction(this IDbContext dbContext, IsolationLevel il)
        {
            dbContext.Session.BeginTransaction(il);
        }
        public static void CommitTransaction(this IDbContext dbContext)
        {
            dbContext.Session.CommitTransaction();
        }
        public static void RollbackTransaction(this IDbContext dbContext)
        {
            dbContext.Session.RollbackTransaction();
        }
        public static void DoWithTransaction(this IDbContext dbContext, Action action)
        {
            dbContext.UseTransaction(action);
        }
        public static void DoWithTransaction(this IDbContext dbContext, Action action, IsolationLevel il)
        {
            dbContext.UseTransaction(action, il);
        }
        public static T DoWithTransaction<T>(this IDbContext dbContext, Func<T> action)
        {
            dbContext.Session.BeginTransaction();
            return ExecuteAction(dbContext, action);
        }
        public static T DoWithTransaction<T>(this IDbContext dbContext, Func<T> action, IsolationLevel il)
        {
            dbContext.Session.BeginTransaction(il);
            return ExecuteAction(dbContext, action);
        }


        static void ExecuteAction(IDbContext dbContext, Action action)
        {
            try
            {
                action();
                dbContext.Session.CommitTransaction();
            }
            catch
            {
                if (dbContext.Session.IsInTransaction)
                    dbContext.Session.RollbackTransaction();
                throw;
            }
        }
        static T ExecuteAction<T>(IDbContext dbContext, Func<T> action)
        {
            try
            {
                T ret = action();
                dbContext.Session.CommitTransaction();
                return ret;
            }
            catch
            {
                if (dbContext.Session.IsInTransaction)
                    dbContext.Session.RollbackTransaction();
                throw;
            }
        }

        public static DbActionBag CreateActionBag(this IDbContext dbContext)
        {
            DbActionBag bag = new DbActionBag(dbContext);
            return bag;
        }


    }
}
