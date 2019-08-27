using EPAS.BaseEntityData;
using EPAS.Component.Json;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Entity.MES;
using System.Collections.Generic;
namespace EPAS.BDA
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-04-17 16:21:22.8002
    /// Description:条码来源 ：仓库、生产
   
   //仓库黑名单：禁止生产备料、出库操作
   
   //生产黑名单：禁止工序流转、作业报工、生产入库操作
    /// </summary>
 class QCBarCodeLockData : BaseEntityData<QCBarCodeLock>
    {
        
    }
}
