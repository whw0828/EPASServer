using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EPAS.Component
{
    /// <summary>
    /// 数据库操作基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class DapperDbOperationBase<TEntity> where TEntity : class
    {

     
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetEntity(object id) 
        {
            try
            { 
                using (IDbConnection cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    TEntity tEntity = cn.Get<TEntity>(id);
                    cn.Close();
                    return tEntity;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public object Insert(TEntity entity) 
        {
            try
            {
                using (IDbConnection cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    var multiKey = cn.Insert(entity);
                    cn.Close();

                    return multiKey;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        public int Inserts(IEnumerable<TEntity> entities) 
        {
            int result = 0;
            try
            {
                using (IDbConnection cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    cn.Insert(entities);
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                result = -1;
            }

            return result;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public bool Update(TEntity entity) 
        {
            try
            {
                using (IDbConnection cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    bool isSuccess = cn.Update(entity);
                    cn.Close();

                    return isSuccess;
                }
            }
            catch
            {
                return false;
            }
        }

        public int Updates(IEnumerable<TEntity> entitys)
        {
            try
            {
                int count = 0;
                using (IDbConnection cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    IDbTransaction transaction = cn.BeginTransaction();
                    foreach (var vItem in entitys)
                    {
                        if (cn.Update(vItem, transaction))
                        {
                            count++;
                        }
                        else
                        {
                            transaction.Rollback();
                            return -1;
                        }
                    }

                    transaction.Commit();
                    cn.Close();
                    return count;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(TEntity entity) 
        {
            bool result = false;
            try
            {
                using (IDbConnection cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    result = cn.Delete(entity);
                    cn.Close();

              
                }
            }
            catch(Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int Deletes(IEnumerable<TEntity> entitys)
        {
            try
            {
                int count = 0;
                using (IDbConnection cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    IDbTransaction transaction = cn.BeginTransaction();
                    foreach (var vItem in entitys)
                    {
                        if (cn.Delete(vItem, transaction))
                        {
                            count++;
                        }
                        else
                        {
                            transaction.Rollback();
                            return -1;
                        }
                    }

                    transaction.Commit();
                    cn.Close();
                    return count;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 批量查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList(object predicate = null, IList<ISort> sort = null) 
        {
            try
            {
                
                using (IDbConnection cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    
                    IEnumerable<TEntity> list = cn.GetList<TEntity>(predicate);

                   
                    cn.Close();
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPage(object predicate, IList<ISort> sort,int page,int resultsPerPage)
        {
            try
            {
                using (IDbConnection cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    //var predicate = Predicates.Field<Person>(f => f.Active, Operator.Eq, true);
                    IEnumerable<TEntity> list = cn.GetPage<TEntity>(predicate, sort, page, resultsPerPage, null, null, true);

                    cn.Close();
                    return list;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 个数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(object predicate)
        {
            try
            {
                using (IDbConnection cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    //var predicate = Predicates.Field<Person>(f => f.Active, Operator.Eq, true);
                    int count = cn.Count<TEntity>(predicate);
                    cn.Close();
                    return count;
                }
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 查询服务器时间
        /// </summary>
        /// <returns></returns>
        public  DateTime GetServerTime()
        {
            return DbConnectionHelper.GetServerTime();
        }
    }
}
