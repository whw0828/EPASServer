using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Interface.BusinessInterface
{
    /// <summary>
    /// 基础数据维护业务接口
    /// </summary>
    public interface IBaseDataBusiness<T>
    {
        /// <summary>
        /// 新添加基础数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList">已存在的基础数据</param>
        /// <returns></returns>
        List<T> CreateBaseData(List<T> itemList);
        /// <summary>
        /// 基础数据保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <returns></returns>
        bool SaveBaseData(List<T> itemList);

     

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> RefreshBaseData();

        /// <summary>
        /// 删除基础数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList"></param>
        /// <returns></returns>
        bool DeleteBaseData(List<T> itemList);

        /// <summary>
        /// 获取文件中基础数据并保存到数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        bool ImportBaseDataFromFile(string filePath, string sheetName);


        /// <summary>
        /// 导出数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        bool ExportBaseDataToFile(string filePath);
    }
}
