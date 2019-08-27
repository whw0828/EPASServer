using EPAS.BaseEntityData;
using EPAS.Component.Json;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Entity.MES;
using System.Collections.Generic;
namespace EPAS.BDA
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-04-17 16:21:22.1779
    /// Description:理论要求 缺陷原因需要根据物料编码分类，实际实施时 可以不填写物料编码！
   //原因：客户无法提供
   //缺陷类型：A.缺料  B.杂质  C.斑点  D.流痕  E.气花/气泡  F.划碰伤  G.修废  H.应力发白  I.熔接痕  J.断裂  K.变型  L.变暗、斑  M.缩凹       N.油污 O.变色 P.皮纹拉伤 Q.冷料/拉丝 R.烧焦 S.泄露 T.爆破 U.热插插坏
    /// </summary>
 class CauseOfNonconformingProductData : BaseEntityData<CauseOfNonconformingProduct>
    {
        
    }
}
