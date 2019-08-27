
using EPAS.DataEntity.Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Interface.BusinessInterface
{
    /// <summary>
    /// 标准查询接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IQueryData<T>: ICustomQueryData<T>
    {

        /// <summary>
        /// 根据实体类 主键 查询实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        T GetEntityById(string id);

        /// <summary>
        /// 返回所有实体信息
        /// </summary>
        /// <returns></returns>
        List<T> GetAllEntitys();

      

    }
}
