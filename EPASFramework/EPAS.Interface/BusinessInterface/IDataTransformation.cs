using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Interface.BusinessInterface
{
    /// <summary>
    /// 数据导入和导出接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IDataTransformation<T>
    {
        /// <summary>
        /// CSV格式的文件数据导入
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        List<T> ImportCSVData(string filePath);

        /// <summary>
        /// Excel格式的文件数据导入
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        List<T> ImportExcelData(string filePath);
        /// <summary>
        /// 数据导出
        /// </summary>
        /// <param name="dataSrc"></param>
        /// <returns></returns>
        bool ExportData(List<T> dataSrc);

        /// <summary>
        /// DevExpress 中GridView数据导出
        /// </summary>
        /// <param name="dataSrc"></param>
        /// <returns></returns>
        bool ExportDevData(object gv);

    }
}
