using System;
using System.Collections.Generic;
using System.Linq;
using EPAS.BaseEntityData;
using EPAS.DataEntity.Enum;
using EPAS.Business.Utilities;
using EPAS.DataEntity.Entity.Common;
using EPAS.DataEntity.Entity.MES;
using EPAS.Interface.BusinessInterface;
using EPAS.Business.BaseObject;
using FPA.BaseEntityDataFac;

namespace EPAS.Business.UserSystem
{
    /// <summary>
    /// 系统业务  含 用户、角色、菜单 的组合业务
    /// </summary>
    public class SystemBusiness:BaseBusinessObject
    {


        #region 业务数据查询

        /// <summary>
        /// 获取子菜单项
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public List<MenuInfo> GetSubMenus(string ParentId)
        {
            List<MenuInfo> itemList = new List<MenuInfo>();
            itemList = BaseEntityFac.GetEntityByField<MenuInfo>("ParentId", ParentId);

            if(itemList!=null && itemList.Count>0)
            {
                itemList= itemList.OrderBy(x => x.OrderNo).ToList();
            }
            return itemList;

        }

        public List<MenuInfo> GetUserSubMenus(string userId, string parentId)
        {
            List<MenuInfo> itemList = new List<MenuInfo>();

            List<MenuInfo> itemSubList = GetSubMenus(parentId);//节点下所有子菜单

            List<MenuInfo> itemVisitList = new List<MenuInfo>();

            itemVisitList = GetMenusByUserId(userId);//用户可以访问的所有菜单


            if (itemSubList != null && itemSubList.Count > 0)
            {
                if (itemVisitList != null && itemVisitList.Count > 0)
                {
                    foreach (var item in itemSubList)
                    {
                        if (itemVisitList.Find(x => x.MenuInfoId == item.MenuInfoId) != null)
                        {
                            itemList.Add(item);
                        }
                    }

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            return itemList;
        }




        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="deleteMenu">被删除的菜单</param>
        /// <param name="updataList">被删除菜单的子菜单</param>
        /// <returns></returns>
        public bool DeleteMenuTransaction(MenuInfo deleteMenu, List<MenuInfo> updataList)
        {
            List<MenuInfo> deleteList = new List<MenuInfo>();

            deleteList.Add(deleteMenu);
       
            //获取角色菜单
            List<RoleMenuRelationship> itemList = BaseEntityFac.GetEntityByField<RoleMenuRelationship>("MenuInfoId", deleteMenu.MenuInfoId);


           bool rel= BaseEntityFac.TransactionOPEntitys<RoleMenuRelationship>((cn, transaction) =>
            {
                bool result = false;
                result = BaseEntityFac.TransactionOPEntitysAdd<RoleMenuRelationship>(cn, transaction, EOPType.Delete, itemList);

                result = BaseEntityFac.TransactionOPEntitysAdd<MenuInfo>(cn, transaction, EOPType.Delete, deleteList);

                result = BaseEntityFac.TransactionOPEntitysAdd<MenuInfo>(cn, transaction, EOPType.Update, updataList);

                
                return result;
            }

           );

            return rel;
        }

        /// <summary>
        /// 保存角色跟菜单项的关系
        /// </summary>
        /// <param name="RoleInfoId"></param>
        /// <param name="insertItem"></param>
        /// <returns></returns>
        public bool SaveRoleMenuRelationship(string RoleInfoId, List<RoleMenuRelationship> insertItem)
        {
            BaseBusinessObject _BaseBusinessObject = new BaseBusinessObject();

            List<RoleMenuRelationship> itemDel = _BaseBusinessObject.GetEntityByField<RoleMenuRelationship>("RoleInfoId", RoleInfoId);
   

            bool rel = BaseEntityFac.TransactionOPEntitys<RoleMenuRelationship>((cn, transaction) =>
            {
                bool result = false;
               
                result = BaseEntityFac.TransactionOPEntitysAdd<RoleMenuRelationship>(cn, transaction, EOPType.Delete, itemDel);
                result = BaseEntityFac.TransactionOPEntitysAdd<RoleMenuRelationship>(cn, transaction, EOPType.Insert, insertItem);


                return result;
            }

            );

            return rel;
        }


        public List<MenuInfo> UpdateLanguageMenus(List<MenuInfo> itemList,string languageCode)
        {         
            if(itemList==null)
            {
                return itemList;
            }
            BaseBusinessObject bbo = new BaseBusinessObject();
            foreach (var item in itemList)
            {
               List< MenuInfoLanguage> itemLanguage= bbo.GetEntityByField<MenuInfoLanguage>("MenuInfoId", item.MenuInfoId);
                if(itemLanguage!=null && itemLanguage.Count>0)
                {
                    MenuInfoLanguage mil=itemLanguage.Find(x => x.LanguageCode == languageCode);
                    if(mil!=null)
                    {
                        item.MenuName = mil.MenuName;//多国语言
                    }
                }
            }
            
            return itemList;
        }

        /// <summary>
        /// 根据用户ID 获取用户可以访问的菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<MenuInfo> GetMenusByUserId(string userId)
        {
            List<MenuInfo> menuInfo = new List<MenuInfo>();
            List<RoleInfo> roleInfo = new List<RoleInfo>();

            List<RoleMenuRelationship> roleMenuRelationship = new List<RoleMenuRelationship>();

            //获取用户拥有的角色
            List<UserRoleRelationship> itemList =BaseEntityFac.GetEntityByField<UserRoleRelationship>("UserInfoId", userId);

            foreach (var item in itemList)
            {
                if (roleInfo.Find(x => x.RoleInfoId == item.RoleInfoId) == null)
                {
                    //去掉用户拥有角色中的重复角色
                    RoleInfo ri = BaseEntityFac.GetEntityById<RoleInfo>(item.RoleInfoId);
                    if(ri!=null)
                    {
                        roleInfo.Add(ri);
                    }                   
                }
            }

            foreach (var item in roleInfo)
            {
                //获取角色可以访问的菜单
                List<RoleMenuRelationship> itemRMRList = BaseEntityFac.GetEntityByField<RoleMenuRelationship>("RoleInfoId", item.RoleInfoId);

                foreach(var itemMenu in itemRMRList)
                {
                    if (roleMenuRelationship.Find(x => x.MenuInfoId == itemMenu.MenuInfoId) == null)
                    {
                        MenuInfo mi = BaseEntityFac.GetEntityById<MenuInfo>(itemMenu.MenuInfoId);
                        if(mi!=null)
                        {
                            menuInfo.Add(mi);
                        }
                       
                    }
                }
             
            }

            
            if(menuInfo!=null && menuInfo.Count>0)
            {
                menuInfo = menuInfo.OrderBy(x => x.OrderNo).ToList();
            }
         

            return menuInfo;
        }

        #endregion

        #region 业务数据操作

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginStatus Login(string userCode,string password)
        {
            LoginStatus result = LoginStatus.Failed;

            try
            {
                if(string.IsNullOrWhiteSpace(userCode))
                {
                    return LoginStatus.UserIsNull;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    return LoginStatus.PasswordIsNull;
                }

                List<UserInfo> itemList = BaseEntityFac.GetEntityByField<UserInfo>("EmployeeNum", userCode);

                if (itemList != null && itemList.Count > 0)
                {
                    UserInfo item = itemList.FirstOrDefault();

                    //检查用户是否锁定
                    if(item.LockStatus==(int)LockStatus.Lock)
                    {
                        return LoginStatus.Failed; 
                    }

                    if (item.UserPasswd.CompareTo(password) == 0)
                    {
                        result = LoginStatus.Sucess;

                        #region 记录实时用户登录信息
                        
                        
                        List<RealLoginUserInfo> rlList = BaseEntityFac.GetEntityByField<RealLoginUserInfo>("UserInfoId", item.UserInfoId);
                        if (rlList == null || rlList.Count ==0)
                        {                           
                            List<RealLoginUserInfo> insertList = new List<RealLoginUserInfo>();
                            RealLoginUserInfo rl = new RealLoginUserInfo();
                            rl.UserInfoId = item.UserInfoId;
                            rl.RealLoginUserInfoId = BusinessHelper.CreateGUID();
                            rl.LastTime = BusinessHelper.GetServerTime();

                            insertList.Add(rl);

                            BaseEntityFac.NoTransactionOPEntitys(insertList, EOPType.Insert);

                        }
                        #endregion

                    }
                    else
                    {
                        result = LoginStatus.PasswordError;
                    }

                }
                else
                {
                    result = LoginStatus.UserNotExist;
                }

            }
            catch(Exception ex)
            {
                BusinessHelper.DebugLog(ex.Message);
            }
           

            return result;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="UserInfoId"></param>
        public void ApplicationExit(string UserInfoId)
        {

            List<RealLoginUserInfo> rlList = BaseEntityFac.GetEntityByField<RealLoginUserInfo>("UserInfoId", UserInfoId);
            if (rlList != null && rlList.Count > 0)
            {
                BaseEntityFac.NoTransactionOPEntitys<RealLoginUserInfo>(rlList, EOPType.Delete);
            }
        }


  

        /// <summary>
        /// 初始化 数据库中数据
        /// </summary>
        /// <param name="tableList"></param>
        /// <returns></returns>
        public bool InitDBData(List<string> tableList)
        {
            string initSql = "";

            foreach (var item in tableList)
            {
                initSql += item;
            }
           if( BaseEntityFac.Execute<object>(initSql)!=-1)
            {
                return true;
            }else
            {
                return false;
            }
        }




        /// <summary>
        /// 新添加基础数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemList">已存在的基础数据</param>
        /// <returns></returns>
        public UserInfo CreateUserBaseData(List<UserInfo> itemList)
        {
            return new UserInfo();
        }
  

        /// <summary>
        /// 获取文件中基础数据并保存到数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public bool ImportBaseDataFromFile(string filePath,string sheetName)
        {


            return true;
        }


        /// <summary>
        /// 导出数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ExportBaseDataToFile(string filePath)
        {
            return true;
        }

        #endregion

    }
}
