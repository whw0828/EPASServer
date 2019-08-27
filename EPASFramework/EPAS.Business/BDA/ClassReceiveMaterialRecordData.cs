using EPAS.BaseEntityData;
using EPAS.Component.Json;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Entity.MES;
using System.Collections.Generic;
namespace EPAS.BDA
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-04-17 16:21:22.2607
    /// Description:需要仓库模块 在 PurchaseBarInfo表中提供条码信息，生产才能获取领料的条码信息（例如 物料编码、物料名称、数量、单位 供应商等）
    /// </summary>
 class ClassReceiveMaterialRecordData : BaseEntityData<ClassReceiveMaterialRecord>
    {
        
    }
}
