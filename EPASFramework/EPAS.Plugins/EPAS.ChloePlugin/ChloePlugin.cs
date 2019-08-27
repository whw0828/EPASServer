using Chloe;
using Chloe.SqlServer;
using EPAS.Component;
using EPAS.Component.Json;
using EPAS.Component.Utilities;
using EPAS.DataEntity.Entity.Common;
using EPAS.Interface.PluginInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Data.ChloePlugin
{
    /// <summary>
    /// ChloePlugin 是EPAS支持的轻量级数据插件 ORM:Chloe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ChloePlugin<T> : IDataPlugin<T> where T : class
    {
        public IDbContext DbContext;
   

        public ChloePlugin()
        {
            //配置插件的数据库连接地址
            //DbConnectionHelper.LicenseConnectStr = "Data Source=mes.zhongkehuazhi.com;Initial Catalog=EPAS;User Id=sa;Password=whw135063;";

            //DbConnectionHelper.LicenseConnectStr = "Data Source=192.168.1.228;Initial Catalog=EPAS;User Id=sa;Password=whw135063;";

            DbContext= new MsSqlContext(DbConnectionHelper.LicenseConnectStr);
        }

        //List<T> itemList = new List<T>();//测试
        /// <summary>
        /// 根据实体类 主键 查询实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public string GetEntityById(string id)
        {
            try
            {
                string tableName = ObjectHelper.GetEntityName<T>();

                string fieldName = tableName + "Id";


                string sql = @"select * from " + " " + tableName + " " + " where " + " " + fieldName + "= @fieldValue";

                var itemList = DbContext.SqlQuery<T>(sql, new { fieldValue = id });
                if (itemList != null)
                {
                    T item = itemList.FirstOrDefault();
                    return JsonUtil.toJson(new { Msg = true, Content = JsonUtil.toJson(item) });
                }
                else
                {
                    return JsonUtil.toJson(new { Msg = false });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return JsonUtil.toJson(new { Msg = false });
            }

        }

        /// <summary>
        /// 返回所有实体信息
        /// </summary>
        /// <returns></returns>
        public string GetAllEntitys()
        {
            try
            {
                var itemList = DbContext.Query<T>().ToList();
                if (itemList != null)
                {
                    return JsonUtil.toJson(new { Msg = true, Content = JsonUtil.toJson(itemList) });
                }
                else
                {
                    return JsonUtil.toJson(new { Msg = false });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return JsonUtil.toJson(new { Msg = false });
            }

        }

        public string GetNewSerialNumber(string serialtype)
        {
            return "";
        }
        /// <summary>
        /// 根据日期 返回实体类信息
        /// </summary>
        /// <returns></returns>
        public string GetEntitysByDate(DateTime sdt, DateTime edt)
        {
            return "";
        }

        public string Execute(string sql, object param = null)
        {
            try
            {
                //using (var cn = DbConnectionHelper.CreateConnection())
                //{
                //    cn.Open();
                //    var item = cn.Execute(sql, param);//返回命令影响的行数
                //    cn.Close();

                //    return JsonUtil.toJson(new { Msg = true, Content = item });

                //}

               var itemList = DbContext.SqlQuery<T>(sql, param);

                if(itemList!= null)
                {
                    return JsonUtil.toJson(new { Msg = true, Content = JsonUtil.toJson(itemList) });
                }else
                {
                    return JsonUtil.toJson(new { Msg = false, Content = "" });
                }
               


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return JsonUtil.toJson(new { Msg = false });
            }
        }

        public string GetEntityViewJson<TV>(string sql, object param = null)
        {
            try
            {

                var itemList = DbContext.SqlQuery<TV>(sql, param);

                if (itemList != null)
                {
                    return JsonUtil.toJson(new { Msg = true, Content = JsonUtil.toJson(itemList) });
                }
                else
                {
                    return JsonUtil.toJson(new { Msg = false, Content = "" });
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return JsonUtil.toJson(new { Msg = false });
            }

        }




        /// <summary>
        /// 实体类操作 （增删改）
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="optype"></param>
        /// <returns></returns>
        public bool NoTransactionOPEntitys(List<T> entitys, EOPType optype)
        {
            bool result = false;

            try
            {
                //itemList.AddRange(entitys);
                if (optype == EOPType.Insert)
                {
                    DbContext.InsertRange(entitys);                    
                    result = true;
                    
                }
                else if (optype == EOPType.Update)
                {
                    //由于需要在实体中强制指定主键： [ColumnAttribute(IsPrimaryKey = true)]
                    //不是主流标准，目前EPAS中实体不支持
                    if (DbContext.Update(entitys) >= 0)
                    {
                        result = true;
                    }
                }
                else if (optype == EOPType.Delete)
                {
                    //由于需要在实体中强制指定主键： [ColumnAttribute(IsPrimaryKey = true)]
                    //不是主流标准，目前EPAS中实体不支持
                    if (DbContext.Delete(entitys) >= 0)
                    {
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            return result;
        }

        #region 事务处理

        /// <summary>
        /// 未发布方法 无法使用 2019-6-27
        /// </summary>
        /// <typeparam name="TE"></typeparam>
        /// <param name="cn"></param>
        /// <param name="transaction"></param>
        /// <param name="eop"></param>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public bool TransactionOPEntitysAdd<TE>(IDbConnection cn, IDbTransaction transaction, EOPType eop, List<TE> itemList) where TE : class
        {
            int count = 0;

            try
            {
                #region 插入事务
                if (eop == EOPType.Insert)
                {

                    DbContext.InsertRange(itemList);


                }
                #endregion

                #region 更新事务
                if (eop == EOPType.Update)
                {

                    foreach (TE item in itemList)
                    {

                        if (DbContext.Update<TE>(item)>=0)
                        {
                            count++;
                        }
                        else
                        {
                            if (DbContext.Session.IsInTransaction)
                                DbContext.Session.RollbackTransaction();
                            return false;
                        }
                    }
                }
                #endregion

                #region 删除事务
                if (eop == EOPType.Delete)
                {

                    foreach (TE item in itemList)
                    {
                        if (DbContext.Delete<TE>(item)>=0)
                        {
                            if (DbContext.Session.IsInTransaction)
                                DbContext.Session.RollbackTransaction();
                            return false;

                        }
                        else
                        {
                            //删除成功
                            count++;
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            return true;
        }


        /// <summary>
        /// 未发布方法 无法使用 2019-6-27
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public bool TransactionOPEntitysCommit(Func<IDbConnection, IDbTransaction, bool> fun)
        {
            try
            {
                var cn = DbContext.Session.CurrentConnection;
                
                  

                IDbTransaction transaction = null;

                DbContext.Session.BeginTransaction();

                if (fun(cn, transaction))
                {
                    DbContext.Session.CommitTransaction();
                }      
              
            }
            catch (Exception ex)
            {
                if (DbContext.Session.IsInTransaction)
                    DbContext.Session.RollbackTransaction();
                Debug.WriteLine(ex.Message);
                return false;
            }


            return true;
        }

        #endregion
    }
}
