using EPAS.BaseEntityData;
using EPAS.Component.Json;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Entity.MES;
using System.Collections.Generic;
namespace EPAS.BDA
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-04-17 16:21:22.5659
    /// Description:该条码 含 工序中间品的条码 和 产品条码（最后一道工序）
   
   
   //条码类型（1-投料码转报工码  2-  非转序报工码）	
   
   //1、投料码转报工码  的业务场景：成品的条码以投料的方式进入工序生产
  // 2、工序新建报工码的业务场景：工序自己打印的新报工条码
   
   
    /// </summary>
 class ProductionMakeMaterialBarcodeData : BaseEntityData<ProductionMakeMaterialBarcode>
    {
        
    }
}
