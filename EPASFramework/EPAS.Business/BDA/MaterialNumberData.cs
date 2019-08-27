using EPAS.BaseEntityData;
using EPAS.Component.Json;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Entity.MES;
using System.Collections.Generic;
namespace EPAS.BDA
{
    /// <summary>
    /// Author:CodeFactory
    /// Create Date:2019-04-17 15:44:09.0177
    /// Description:
    /// </summary>
    class MaterialNumberData : BaseEntityData<MaterialNumber>
    {
        //自定义查询
        public List<MaterialNumber> GetMaterialNumberView(string MaterialNum)
        {
            string Sql = "select * FROM MaterialNumber a WHERE a.MaterialNum=@MaterialNum";
            var msg = this.GetEntityViewJson<MaterialNumber>(Sql, new { MaterialNum = MaterialNum });

            if ((bool)msg.Msg)
            {
                List<MaterialNumber> itemList = JsonUtil.fromJson<List<MaterialNumber>>((string)msg.Content);
                return itemList;
            }
            return null;
        }
    }
}
