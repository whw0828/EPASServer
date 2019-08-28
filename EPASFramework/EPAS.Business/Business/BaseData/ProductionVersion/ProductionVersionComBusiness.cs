
using EPAS.DataEntity.Entity.MES;
using EPAS.DataEntity.Utilities.SQLServer;
using FPA.BaseEntityDataFac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Business.BaseData
{
    /// <summary>
    /// 生产版本综合业务
    /// </summary>
    public class ProductionVersionComBusiness 
    {
        #region ①生产版本定义和维护
        


        #endregion

        #region ②生产版本中包含的工序
        public  List<ProductionMakeWorkOrderView> GetProductionMakeWorkOrderView(string whereSql)
        {
            //创建视图SQL
            var sqlCmd = MsGenerateSql.SelectFromTable<ProductionMakeWorkOrder>().
               LeftJoin<WorkOrder, ProductionMakeWorkOrder>((t1, t2) =>
               t1.WorkOrderId == t2.WorkOrderId).
               LeftJoin<ProductionVersion, ProductionMakeWorkOrder>((t1, t2) =>
               t1.ProductionVersionId == t2.ProductionVersionId).Distinct();
               //.Where<ProductionMakeWorkOrder>((t) => t.ProductionVersionId == ProductionVersionId);
            //设置视图返回值
            string sql = sqlCmd.AutoMapDisplayItem<ProductionMakeWorkOrderView>().GenerateSql();


            sql = sql + "  " + whereSql;

            //执行视图SQL
            return BaseEntityFac.GetEntityView<ProductionMakeWorkOrderView>(sql);
         
        }


        public  List<ProductionMakeWorkOrderView> GetProductionMakeWorkOrderView<T>(Expression<Func<T, bool>> infoExps)
        {

            string whereSql = MsGenerateSql.DealExpress(infoExps);

            return GetProductionMakeWorkOrderView(whereSql);
        }



            #endregion

            #region ③工序可以利用的生产资源配置




            #endregion

            #region ④工序基础设置和工位（例如报工配置、打印配置）

            #endregion

            #region ⑤工序的先行依赖工序

            #endregion

            #region ⑥工序投入物料和产出

            #endregion

            #region ⑦工序作业指导书配置

            #endregion

            #region ⑧生产版本发布

            #endregion






        }
}
