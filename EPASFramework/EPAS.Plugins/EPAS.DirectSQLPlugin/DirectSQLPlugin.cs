
using Dapper;
using DapperExtensions;
using EPAS.Component;
using EPAS.Component.Json;
using EPAS.DataEntity.Entity.Common;
using EPAS.Interface.PluginInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EPAS.Data.DirectSQLPlugin
{
    /// <summary>
    /// DirectSQLPlugin 是EPAS自带的轻量级数据插件 ORM:Dapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DirectSQLPlugin<T> : DapperDbOperationBase<T>, IDataPlugin<T> where T:class
    {


        public DirectSQLPlugin()
        {
            //配置插件的数据库连接地址
            //DbConnectionHelper.LicenseConnectStr = "Data Source=mes.zhongkehuazhi.com;Initial Catalog=EPAS;User Id=sa;Password=whw135063;";

            //DbConnectionHelper.LicenseConnectStr = "Data Source=192.168.1.228;Initial Catalog=EPAS;User Id=sa;Password=whw135063;";


            //DbConnectionHelper.LicenseConnectStr = "Data Source=www.wuke.org.cn,1443;Initial Catalog=EPAS;User Id=sa;Password=Zkhz!@#;";
            
            //DBConnectInfo.txt 文件在EPAS.API工程中 
            DbConnectionHelper.LicenseConnectStr = File.ReadAllText("DBConnectInfo.txt");

            //EPAS.DirectSQLPlugin.Settings.Default
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
                var itemList = this.GetEntity(id);
                if (itemList != null)
                {
                    return JsonUtil.toJson(new { Msg = true, Content = JsonUtil.toJson(itemList) });
                }
                else
                {
                    return JsonUtil.toJson(new { Msg = false });
                }
            }
            catch(Exception ex)
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
                var itemList = this.GetList();
                if (itemList != null)
                {
                    return JsonUtil.toJson(new { Msg = true, Content = JsonUtil.toJson(itemList) });
                }
                else
                {
                    return JsonUtil.toJson(new { Msg = false });
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return JsonUtil.toJson(new { Msg = false });
            }
      
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
                using (var cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    var item = cn.Execute(sql, param);//返回命令影响的行数
                    cn.Close();
               
                   return JsonUtil.toJson(new { Msg = true, Content = item });

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return JsonUtil.toJson(new { Msg = false });
            }
        }

        static object _lock = new object();
        public string GetNewSerialNumber(string serialtype)
        {
            try
            {
                using (var cn = DbConnectionHelper.CreateConnection())
                {
                    lock (_lock)
                    {
                        var para = new DynamicParameters();
                        para.Add("@SerialType", serialtype);                     
                        para.Add("@SerialNumber", string.Empty, DbType.String, ParameterDirection.Output);

                        var res1 = cn.Query("proc_GetSerialNumber", para, null, true, null, CommandType.StoredProcedure);  //0

                        return para.Get<string>("@SerialNumber");
                    }
                }
            }
            catch
            {
                return null;
            }
        }


   
        public string GetEntityViewJson<TV>(string sql, object param = null)
        {
            try
            {
                using (var cn = DbConnectionHelper.CreateConnection())
                {
                    cn.Open();
                    var itemList = cn.Query<TV>(sql, param).ToList();
                    cn.Close();
                    if (itemList != null)
                    {
                        return JsonUtil.toJson(new { Msg = true, Content = JsonUtil.toJson(itemList) });
                    }
                    else
                    {
                        return JsonUtil.toJson(new { Msg = false });
                    }
                }
                
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return  JsonUtil.toJson(new { Msg = false });
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
                    if (this.Inserts(entitys) >= 0)
                    {
                        result = true;
                    }
                }
                else if (optype == EOPType.Update)
                {
                    if (this.Updates(entitys) >= 0)
                    {
                        result = true;
                    }
                }
                else if (optype == EOPType.Delete)
                {
                    if (this.Deletes(entitys) >= 0)
                    {
                        result = true;
                    }
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
           

            return result;
        }

        #region 事务处理
        public bool TransactionOPEntitysAdd<TE> (IDbConnection cn, IDbTransaction transaction, EOPType eop, List<TE> itemList) where TE : class
        {
            int count = 0;

            try
            {
                #region 插入事务
                if (eop == EOPType.Insert)
                {

                    cn.Insert<TE>(itemList, transaction);


                }
                #endregion

                #region 更新事务
                if (eop == EOPType.Update)
                {

                    foreach (var item in itemList)
                    {

                        if (cn.Update<TE>(item, transaction))
                        {
                            count++;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
                #endregion

                #region 删除事务
                if (eop == EOPType.Delete)
                {

                    foreach (var item in itemList)
                    {
                        if (!cn.Delete<TE>(item, transaction))
                        {
                            transaction.Rollback();
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



        public bool TransactionOPEntitysCommit(Func<IDbConnection, IDbTransaction,bool> fun) 
        {
            try
            {
                using (var cn = DbConnectionHelper.CreateConnection())
                {
                    
                    cn.Open();
                    
                   
                    IDbTransaction transaction = cn.BeginTransaction();

                    if(fun(cn, transaction))
                    {
                        transaction.Commit();
                    }
                
                    cn.Close();


                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }


            return true;
        }

        #endregion
    }
}
