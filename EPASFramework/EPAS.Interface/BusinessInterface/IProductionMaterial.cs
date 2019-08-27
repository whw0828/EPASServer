using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Interface.BusinessInterface
{
    /// <summary>
    /// 生产物料接口  
    /// </summary>
   public interface IProductionMaterial
    {
       
        /// <summary>
        /// 生产领料
        /// </summary>
        /// <param name="CRMSerialNumber">领料单流水号</param>
        /// <param name="BarCode">领料条码号</param>
        /// <returns>领料业务返回值</returns>
        int ReceiveMaterial(string CRMSerialNumber,string BarCode);


        int FeedMaterial(string WorkPlanNo, string BarCode,string MainBarCode="");


    }
}
