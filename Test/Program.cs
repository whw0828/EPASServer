
using EPAS.Business.Utilities;
using EPAS.Business.BaseData;
using EPAS.DataEntity.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using EPAS.Business.UserSystem;
using EPAS.DataEntity.Enum;
using EPAS.Component.DataHub;
using EPAS.DataEntity.Utilities;
using EPAS.DataEntity.Utilities.SQLServer;

namespace Test
{
   
    class Program
    {
        private static string LocalLanguage(string des)
        {
            if (des == null)
            {
                return "";
            }
            string local = des;
            Dictionary<string, string> lanMap = new Dictionary<string, string>();// 语言 显示
            char splitDis = ',';//显示分割符
            char splitEnd = ';';//定义结束分割符

            string[] lanArrar = des.Split(splitEnd);

            //系统语言
            string localPCLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            if (lanArrar != null && lanArrar.Length > 0)
            {
                foreach (var item in lanArrar)
                {
                    string[] lanDisplay = item.Split(splitDis);

                    if (lanDisplay != null && lanDisplay.Length > 1)
                    {
                        lanMap.Add(lanDisplay[0], lanDisplay[1]);
                    }
                }
            }
            if (lanMap.ContainsKey(localPCLanguage))
            {
                des = lanMap[localPCLanguage];
            }
            return des;
        }
        static void Main(string[] args)
        {
            string str = EE.Name<EnumData>(x => x.DisplayMember);
            str = EE.Name<EnumData>(x => x.ValueMember);

            string  UserInfoId1 = "84667fb5-4318-442f-99c7-243246c7c863";

          var sqlCmd=  MsGenerateSql.SelectFromTable<MenuInfo>().
                LeftJoin<RoleMenuRelationship, MenuInfo>((t1,t2)=>
                t1.MenuInfoId==t2.MenuInfoId).
                LeftJoin<RoleInfo,RoleMenuRelationship>((t1, t2) => 
                t1.RoleInfoId==t2.RoleInfoId).
                LeftJoin<UserRoleRelationship, RoleInfo>((t1, t2) =>
                t1.RoleInfoId==t2.RoleInfoId).
            LeftJoin<UserInfo, UserRoleRelationship>((t1, t2) => 
                t1.UserInfoId==t2.UserInfoId).Distinct().Where< UserInfo>((t) =>t.UserInfoId == UserInfoId1 && t.EmployeeNum=="101");

            DateTime dt = DateTime.Parse("2019-07-01");
            sqlCmd = MsGenerateSql.SelectFromTable<MaterialNumber>()
           .Where<MaterialNumber>((t) => t.ValidFrom>dt);



            EE.Name<MenuInfo>(t=>t.MenuName);

            //MsGenerateSql.Name<MenuInfo>((t) => new object[]
            //{
            //    t.MenuName
            //});

            string sql = sqlCmd.AutoMapDisplayItem<RoleInfo>().GenerateSql();

            sql = sqlCmd.ManualMapDisplayItem(EE.NameSql<UserInfo>(t=> new object[]
            {
                t.EmployeeNum,t.EmployeeName,t.Mobile,t.Email
            })).GenerateSql();

            return;
            //EnumAttribute.EnumToList<MaterialType>();
            //LocalLanguage("zh-CN,原材料;en-US,RawMaterial");
            //SystemBusiness _UserInfoBusiness = new SystemBusiness();

            //List<MenuInfo> itemList = new List<MenuInfo>();
       
            //itemList = _UserInfoBusiness.GetAllEntitys<MenuInfo>();


            //    LoginStatus status = _UserInfoBusiness.Login("101", "123");

            //_UserInfoBusiness.GetMenusByUserId("84667fb5-4318-442f-99c7-243246c7c863");

      

            ////List<QCItemType> itemQCNewNewList = new List<QCItemType>();
            ////List<MaterialNumber> mnUpdateList = new List<MaterialNumber>();

            ////DateTime dt = DateTime.Now;

            ////MaterialNumber mn = mnd.GetEntityById(mnList.FirstOrDefault().MaterialNumberId);

            ////MaterialNumber itemMNU = ObjectHelper.DeepClone<MaterialNumber>(mn);
            ////MaterialNumber itemMNNew = ObjectHelper.DeepClone<MaterialNumber>(mn);

            ////itemMNU.MaterialName = mn.MaterialName + "更新";
            ////mnUpdateList.Add(itemMNU);

           

            ////itemMNNew.MaterialNumberId = Guid.NewGuid().ToString();
            ////itemMNNew.MaterialNum = mn.MaterialNum + "新建";
            ////itemMNNew.MaterialName = mn.MaterialName + "新建";

            ////mnUpdateList.Add(itemMNNew);

            //string path = @"C:\Users\koukou\Desktop\EMAS系统上线演示\基础数据\物料信息201922713399.xls";
            //if(mnd.ImportBaseDataFromFile(path, "物料信息"))
            //{
            //    Console.Write("数据导入成功！");
            //}

            //ProductionBusiness pb = new ProductionBusiness();

            //path = @"C:\Users\koukou\Desktop\EMAS系统上线演示\基础数据\客户物料信息关联20192281579.xls";
            //if (pb.ImportBaseDataFromFile(path, "客户物料信息关联"))
            //{
            //    Console.Write("数据导入成功！");
            //}
            ////if(mnd.SaveBaseData(mnUpdateList))
            ////{
            ////    Console.Write("保存成功");
            ////}

            //foreach (var item in mnList)
            //{
                



            //    //if (mnUpdateList.Count == 0)
            //    //{

            //    //    MaterialNumber itemMNNew = ObjectHelper.DeepClone<MaterialNumber>(mn);

            //    //    //MaterialNumber itemMNNew = new MaterialNumber();

            //    //    itemMNNew.MaterialNumberId = itemMNNew.MaterialNumberId;
            //    //    itemMNNew.MaterialName = itemMNNew.MaterialName+"更新";
            //    //    mnUpdateList.Add(itemMNNew);


            //    //    QCItemType itemQCNew = new QCItemType();
            //    //    itemQCNew.QCItemTypeId = Guid.NewGuid().ToString();
            //    //    int typecode = dt.Year + dt.Month + dt.Day + dt.Hour + dt.Minute + dt.Second + dt.Millisecond;
            //    //    itemQCNew.QCItemTypeCode = typecode.ToString();
            //    //    itemQCNew.QCItemTypeName = "首件";
            //    //    itemQCNewNewList.Add(itemQCNew);

            //    //    mnd.TransactionOPEntitys((cn, transaction) =>
            //    //    {
            //    //        bool result = false;
            //    //        result = mnd.TransactionOPEntitysAdd<QCItemType>(cn, transaction, EOPType.Insert, itemQCNewNewList);

            //    //        result = mnd.TransactionOPEntitysAdd<MaterialNumber>(cn, transaction, EOPType.Update, mnUpdateList);

            //    //        return result;
            //    //    }

            //    //   );
            //    //}


            //    //Console.WriteLine(mn.MaterialName);
           // }

        }
    }
}
