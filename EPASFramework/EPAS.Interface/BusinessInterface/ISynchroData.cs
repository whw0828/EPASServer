using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Interface.BusinessInterface
{
    /// <summary>
    /// 系统间数据同步  例如 SAP生产订单传输到 EPAS；EPAS 将生产数据传输给SAP
    /// </summary>
   public interface ISynchroData
    {
        /// <summary>
        /// 获取同步数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        List<T> GetSynchroData<T>(object param = null);

        /// <summary>
        /// 上传同步数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <returns></returns>
        bool SetSynchroData<T>(List<T> itemList);

    }
}
