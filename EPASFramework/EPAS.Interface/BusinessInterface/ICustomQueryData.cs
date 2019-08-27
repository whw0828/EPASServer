using EPAS.DataEntity.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Interface.BusinessInterface
{
    /// <summary>
    /// 自定义数据查询接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICustomQueryData<TEntity>
    {

        /// <summary>
        /// 根据实体类中字段 查询实体信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        List<TEntity> GetEntityByField(string fieldName,string fieldValue);


    }
}
