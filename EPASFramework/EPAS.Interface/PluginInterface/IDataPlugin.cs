
using EPAS.DataEntity.Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Interface.PluginInterface
{
    /// <summary>
    /// 数据插件接口，业务数据插件必须实现该接口后才能可以被系统调用！
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataPlugin<TEntity>
    {
 
        /// <summary>
        /// 根据实体类 主键 查询实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        string GetEntityById(string id);

        /// <summary>
        /// 返回所有实体信息
        /// </summary>
        /// <returns></returns>
        string GetAllEntitys();

        string GetNewSerialNumber(string serialtype);
        /// <summary>
        /// 执行SQL 命令 只返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        string Execute(string sql, object param = null);
        /// <summary>
        /// 根据SQL语句 返回JSon格式视图
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        string GetEntityViewJson<TV>(string sql, object param = null);

        /// <summary>
        /// 获取视图  待定
        /// </summary>
        /// <typeparam name="TV"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        //string GetEntitysView<TV>(string sql, object param = null);

        /// <summary>
        /// 单实体类操作 （增删改） 非事务
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="optype"></param>
        /// <returns></returns>
        bool NoTransactionOPEntitys(List<TEntity> entitys, EOPType optype);


        /// <summary>
        /// 添加事务处理的实体信息
        /// </summary>
        /// <typeparam name="TE"></typeparam>
        /// <param name="cn"></param>
        /// <param name="transaction"></param>
        /// <param name="eop"></param>
        /// <param name="itemList"></param>
        /// <returns></returns>
        bool TransactionOPEntitysAdd<TE>(IDbConnection cn, IDbTransaction transaction, EOPType eop, List<TE> itemList) where TE : class;

        /// <summary>
        /// 多实体类事务操作提交
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="optype"></param>
        /// <returns></returns>
        bool TransactionOPEntitysCommit(Func<IDbConnection, IDbTransaction, bool> fun);

    }
}
