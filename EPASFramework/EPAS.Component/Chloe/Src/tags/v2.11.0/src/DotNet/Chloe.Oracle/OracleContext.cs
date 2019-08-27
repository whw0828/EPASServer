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

namespace Chloe.Oracle
{
    public class OracleContext : DbContext
    {
        DbContextServiceProvider _dbContextServiceProvider;
        bool _convertToUppercase = true;
        public OracleContext(IDbConnectionFactory dbConnectionFactory)
        {
            Utils.CheckNull(dbConnectionFactory);

            this._dbContextServiceProvider = new DbContextServiceProvider(dbConnectionFactory, this);
        }

        /// <summary>
        /// 是否将 sql 中的表名/字段名转成大写。默认为 true。
        /// </summary>
        public bool ConvertToUppercase { get { return this._convertToUppercase; } set { this._convertToUppercase = value; } }
        public override IDbContextServiceProvider DbContextServiceProvider
        {
            get { return this._dbContextServiceProvider; }
        }

        public override TEntity Insert<TEntity>(TEntity entity, string table)
        {
            Utils.CheckNull(entity);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(entity.GetType());

            MappingMemberDescriptor keyMemberDescriptor = typeDescriptor.PrimaryKey;
            MemberInfo keyMember = typeDescriptor.PrimaryKey.MemberInfo;

            object keyValue = null;

            string sequenceName;
            object sequenceValue = null;
            MappingMemberDescriptor defineSequenceMemberDescriptor = GetDefineSequenceMemberDescriptor(typeDescriptor, out sequenceName);

            if (defineSequenceMemberDescriptor != null)
            {
                sequenceValue = ConvertIdentityType(this.GetSequenceNextValue(sequenceName), defineSequenceMemberDescriptor.MemberInfoType);
            }

            Dictionary<MappingMemberDescriptor, DbExpression> insertColumns = new Dictionary<MappingMemberDescriptor, DbExpression>();
            foreach (var kv in typeDescriptor.MappingMemberDescriptors)
            {
                MemberInfo member = kv.Key;
                MappingMemberDescriptor memberDescriptor = kv.Value;

                object val = null;
                if (defineSequenceMemberDescriptor != null && memberDescriptor == defineSequenceMemberDescriptor)
                {
                    val = sequenceValue;
                }
                else
                    val = memberDescriptor.GetValue(entity);

                if (memberDescriptor == keyMemberDescriptor)
                {
                    keyValue = val;
                }

                DbExpression valExp = DbExpression.Parameter(val, memberDescriptor.MemberInfoType);
                insertColumns.Add(memberDescriptor, valExp);
            }

            if (keyMemberDescriptor != null && keyValue == null)
            {
                throw new ChloeException(string.Format("The primary key '{0}' could not be null.", keyMemberDescriptor.MemberInfo.Name));
            }

            DbTable dbTable = table == null ? typeDescriptor.Table : new DbTable(table, typeDescriptor.Table.Schema);
            DbInsertExpression e = new DbInsertExpression(dbTable);

            foreach (var kv in insertColumns)
            {
                e.InsertColumns.Add(kv.Key.Column, kv.Value);
            }

            this.ExecuteSqlCommand(e);

            if (defineSequenceMemberDescriptor != null)
                defineSequenceMemberDescriptor.SetValue(entity, sequenceValue);

            return entity;
        }
        public override object Insert<TEntity>(Expression<Func<TEntity>> content, string table)
        {
            Utils.CheckNull(content);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(typeof(TEntity));

            MappingMemberDescriptor keyMemberDescriptor = typeDescriptor.PrimaryKey;

            string sequenceName;
            object sequenceValue = null;
            MappingMemberDescriptor defineSequenceMemberDescriptor = GetDefineSequenceMemberDescriptor(typeDescriptor, out sequenceName);

            Dictionary<MemberInfo, Expression> insertColumns = InitMemberExtractor.Extract(content);

            DbTable explicitDbTable = null;
            if (table != null)
                explicitDbTable = new DbTable(table, typeDescriptor.Table.Schema);
            DefaultExpressionParser expressionParser = typeDescriptor.GetExpressionParser(explicitDbTable);
            DbInsertExpression e = new DbInsertExpression(explicitDbTable ?? typeDescriptor.Table);

            object keyVal = null;

            foreach (var kv in insertColumns)
            {
                MemberInfo key = kv.Key;
                MappingMemberDescriptor memberDescriptor = typeDescriptor.TryGetMappingMemberDescriptor(key);

                if (memberDescriptor == null)
                    throw new ChloeException(string.Format("The member '{0}' does not map any column.", key.Name));

                if (memberDescriptor == defineSequenceMemberDescriptor)
                    throw new ChloeException(string.Format("Can not insert value into the column '{0}', because it's mapping member has define a sequence.", memberDescriptor.Column.Name));

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

                e.InsertColumns.Add(memberDescriptor.Column, expressionParser.Parse(kv.Value));
            }

            if (keyMemberDescriptor == defineSequenceMemberDescriptor)
            {
                sequenceValue = ConvertIdentityType(this.GetSequenceNextValue(sequenceName), defineSequenceMemberDescriptor.MemberInfoType);

                keyVal = sequenceValue;
                e.InsertColumns.Add(keyMemberDescriptor.Column, DbExpression.Parameter(keyVal));
            }

            if (keyMemberDescriptor != null && keyVal == null)
            {
                throw new ChloeException(string.Format("The primary key '{0}' could not be null.", keyMemberDescriptor.MemberInfo.Name));
            }

            this.ExecuteSqlCommand(e);
            return keyVal; /* It will return null if an entity does not define primary key. */
        }

        public override int Update<TEntity>(TEntity entity, string table)
        {
            Utils.CheckNull(entity);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(entity.GetType());
            EnsureMappingTypeHasPrimaryKey(typeDescriptor);

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

                SequenceAttribute attr = (SequenceAttribute)memberDescriptor.GetCustomAttribute(typeof(SequenceAttribute));
                if (attr != null)
                    continue;

                object val = memberDescriptor.GetValue(entity);

                if (entityState != null && !entityState.HasChanged(memberDescriptor, val))
                    continue;

                DbExpression valExp = DbExpression.Parameter(val, memberDescriptor.MemberInfoType);
                updateColumns.Add(memberDescriptor, valExp);
            }

            if (keyVal == null)
                throw new ChloeException(string.Format("The primary key '{0}' could not be null.", keyMember.Name));

            if (updateColumns.Count == 0)
                return 0;

            DbTable dbTable = table == null ? typeDescriptor.Table : new DbTable(table, typeDescriptor.Table.Schema);
            DbExpression left = new DbColumnAccessExpression(dbTable, keyMemberDescriptor.Column);
            DbExpression right = DbExpression.Parameter(keyVal, keyMemberDescriptor.MemberInfoType);
            DbExpression conditionExp = new DbEqualExpression(left, right);

            DbUpdateExpression e = new DbUpdateExpression(dbTable, conditionExp);

            foreach (var item in updateColumns)
            {
                e.UpdateColumns.Add(item.Key.Column, item.Value);
            }

            int ret = this.ExecuteSqlCommand(e);
            if (entityState != null)
                entityState.Refresh();
            return ret;
        }
        public override int Update<TEntity>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TEntity>> content, string table)
        {
            Utils.CheckNull(condition);
            Utils.CheckNull(content);

            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(typeof(TEntity));

            Dictionary<MemberInfo, Expression> updateColumns = InitMemberExtractor.Extract(content);

            DbTable explicitDbTable = null;
            if (table != null)
                explicitDbTable = new DbTable(table, typeDescriptor.Table.Schema);
            DefaultExpressionParser expressionParser = typeDescriptor.GetExpressionParser(explicitDbTable);

            DbExpression conditionExp = expressionParser.ParseFilterPredicate(condition);

            DbUpdateExpression e = new DbUpdateExpression(explicitDbTable ?? typeDescriptor.Table, conditionExp);

            foreach (var kv in updateColumns)
            {
                MemberInfo key = kv.Key;
                MappingMemberDescriptor memberDescriptor = typeDescriptor.TryGetMappingMemberDescriptor(key);

                if (memberDescriptor == null)
                    throw new ChloeException(string.Format("The member '{0}' does not map any column.", key.Name));

                if (memberDescriptor.IsPrimaryKey)
                    throw new ChloeException(string.Format("Could not update the primary key '{0}'.", memberDescriptor.Column.Name));

                SequenceAttribute attr = (SequenceAttribute)memberDescriptor.GetCustomAttribute(typeof(SequenceAttribute));
                if (attr != null)
                    throw new ChloeException(string.Format("Could not update the column '{0}',because it's mapping member has define a sequence.", memberDescriptor.Column.Name));

                e.UpdateColumns.Add(memberDescriptor.Column, expressionParser.Parse(kv.Value));
            }

            if (e.UpdateColumns.Count == 0)
                return 0;

            return this.ExecuteSqlCommand(e);
        }

        int ExecuteSqlCommand(DbExpression e)
        {
            IDbExpressionTranslator translator = this.DbContextServiceProvider.CreateDbExpressionTranslator();
            List<DbParam> parameters;
            string cmdText = translator.Translate(e, out parameters);

            int r = this.Session.ExecuteNonQuery(cmdText, parameters.ToArray());
            return r;
        }
        object GetSequenceNextValue(string sequenceName)
        {
            if (this.ConvertToUppercase)
                sequenceName = sequenceName.ToUpper();

            object ret = this.Session.ExecuteScalar(string.Concat("SELECT \"", sequenceName, "\".\"NEXTVAL\" FROM \"DUAL\""));

            if (ret == null || ret == DBNull.Value)
            {
                throw new ChloeException(string.Format("Unable to get the sequence '{0}' next value.", sequenceName));
            }

            return ret;
        }

        static MappingMemberDescriptor GetDefineSequenceMemberDescriptor(TypeDescriptor typeDescriptor, out string sequenceName)
        {
            sequenceName = null;
            MappingMemberDescriptor defineSequenceMemberDescriptor = null;

            foreach (MappingMemberDescriptor memberDescriptor in typeDescriptor.MappingMemberDescriptors.Values)
            {
                SequenceAttribute attr = (SequenceAttribute)memberDescriptor.GetCustomAttribute(typeof(SequenceAttribute));
                if (attr != null)
                {
                    if (defineSequenceMemberDescriptor != null)
                        throw new ChloeException(string.Format("Mapping type '{0}' can not define multiple identity members.", typeDescriptor.EntityType.FullName));

                    if (string.IsNullOrEmpty(attr.Name))
                        throw new ChloeException("Sequence name can not be empty.");

                    sequenceName = attr.Name;
                    defineSequenceMemberDescriptor = memberDescriptor;
                }
            }

            if (defineSequenceMemberDescriptor != null)
                EnsureDefineSequenceMemberType(defineSequenceMemberDescriptor);

            return defineSequenceMemberDescriptor;
        }
        static void EnsureDefineSequenceMemberType(MappingMemberDescriptor defineSequenceMemberDescriptor)
        {
            if (defineSequenceMemberDescriptor.MemberInfoType != UtilConstants.TypeOfInt16 && defineSequenceMemberDescriptor.MemberInfoType != UtilConstants.TypeOfInt32 && defineSequenceMemberDescriptor.MemberInfoType != UtilConstants.TypeOfInt64)
            {
                throw new ChloeException("Identity type must be Int16,Int32 or Int64.");
            }
        }
        static void EnsureMappingTypeHasPrimaryKey(TypeDescriptor typeDescriptor)
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
    }
}
