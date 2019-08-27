using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPAS.DataEntity.Enum;
using EPAS.DataEntity.API;
using EPAS.Business.UserSystem;
using EPAS.DataEntity.Entity.Common;
using Microsoft.AspNetCore.Mvc;
using EPAS.Component.Json;
using FPA.BaseEntityDataFac;
using EPAS.Business.BaseObject;
using EPAS.Component.Utilities;
using EPAS.Business.Utilities;

namespace EPAS.API.Controllers
{

    /// <summary>
    /// 适用于客户端框架未实现用户模块的场景
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefaultSystemController : ControllerBase
    {



        /// <summary>
        /// 返回用户登录初始密码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetUserResetPassword()
        {
            return BusinessHelper.EPASResetPassword;
        }

        /// <summary>
        /// 返回用户最高等级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public int GetMaxUserLevel()
        {
            return BusinessHelper.UserLevel;
        }


        /// <summary>
        /// 返回菜单根节点名称
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetRootParentMenuId()
        {
            return BusinessHelper.RootParentMenuId;
        }

        /// <summary>
        /// 返回系统所有菜单项
        /// </summary>
        /// <param name="参数">空</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<MenuInfo>> GetAllMenus()
        {
            SystemBusiness _SystemBusiness = new SystemBusiness();

            List<MenuInfo> itemList = new List<MenuInfo>();

            itemList = _SystemBusiness.GetAllEntitys<MenuInfo>();

            return itemList;
        }


        /// <summary>
        /// 菜单实体 增删改操作
        /// </summary>
        /// <param name="cmd">OPEntityCmd</param>
        /// <returns></returns>
        [HttpPost]
        public bool MenusNoTransactionOPEntitys(OPEntityCmd cmd)
        {
            SystemBusiness _SysInfoBusiness = new SystemBusiness();

            List<MenuInfo> itemList = JsonUtil.fromJson<List<MenuInfo>>(cmd.EntityJson);
            EOPType eopType = (EOPType)cmd.optype;

            return _SysInfoBusiness.NoTransactionOPEntitys<MenuInfo>(itemList, eopType);
        }


        /// <summary>
        /// 初始化数据库表中内容
        /// </summary>
        /// <param name="cmd">OPEntityCmd</param>
        /// <returns></returns>
        [HttpPost]
        public bool InitDBData(OPEntityCmdBase cmd)
        {
            SystemBusiness _SysInfoBusiness = new SystemBusiness();

            List<string> itemList = JsonUtil.fromJson<List<string>>(cmd.EntityJson);
 

            return _SysInfoBusiness.InitDBData(itemList);
        }


        /// <summary>
        /// 删除一条菜单项  待定
        /// </summary>
        /// <param name="menuInfoId"></param>
        /// <param name="newParentId"></param>
        /// <returns></returns>
        [HttpGet]
        public bool DeleteMenuTransaction(string menuInfoId,string newParentId)
        {
            SystemBusiness _SysInfoBusiness = new SystemBusiness();



            MenuInfo deleteMenu = _SysInfoBusiness.GetSingleEntityByField<MenuInfo>("MenuInfoId", menuInfoId);

            if(deleteMenu!=null)
            {
                List<MenuInfo> updataList = _SysInfoBusiness.GetSubMenus(deleteMenu.MenuInfoId);

                if(updataList!=null)
                {
                    foreach (var item in updataList)
                    {
                        item.ParentId = newParentId;//子菜单升一级
                    }
                }
               
                return _SysInfoBusiness.DeleteMenuTransaction(deleteMenu, updataList);
            }

            return false;
            


           
        }

        /// <summary>
        /// 删除一条菜单
        /// </summary>
        ///<param name="json">(string parentId [菜单父级ID]) </param>
        /// <returns></returns>
        //[HttpPost]
        //public bool DeleteMenuTransaction(dynamic json)
        //{
        //    string menuId = json.menuId;
        //    bool isRootParent= json.isRootParent;

        //    SystemBusiness _SysInfoBusiness = new SystemBusiness();

        //    MenuInfo deleteMenu = JsonUtil.fromJson<MenuInfo>((string)json.deleteMenu);

        //    List<MenuInfo> updataList = JsonUtil.fromJson<List<MenuInfo>>((string)json.updataList);



        //    return _SysInfoBusiness.DeleteMenuTransaction(deleteMenu, updataList);
        //}


        /// <summary>
        /// 返回系统中所有角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<RoleInfo>> GetAllRoleInfos()
        {
            SystemBusiness _SysInfoBusiness = new SystemBusiness();

            List<RoleInfo> itemList = new List<RoleInfo>();

            itemList = _SysInfoBusiness.GetAllEntitys<RoleInfo>();

            return itemList;
        }



        /// <summary>
        /// 返回用户可以访问的菜单信息，由于存在 Byte序列化问题 请使用NewtonSoft Json。
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="languageCode">语言代码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<IEnumerable<MenuInfo>> GetMenusByUserId(string userId, string languageCode="")
        {
            SystemBusiness _SysInfoBusiness = new SystemBusiness();

            List<MenuInfo> itemList = new List<MenuInfo>();

            itemList = _SysInfoBusiness.GetMenusByUserId(userId);

            if(!string.IsNullOrWhiteSpace(languageCode))
            {
                //返回指定语言
                itemList = _SysInfoBusiness.UpdateLanguageMenus(itemList, languageCode);
            }
      



            return itemList;
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="fieldName"></param>
      /// <param name="fieldValue"></param>
      /// <returns></returns>
        [HttpPost]
        public ActionResult<IEnumerable<UserInfo>> GetUserEntityByField(string fieldName, string fieldValue)
        {
            SystemBusiness _SysInfoBusiness = new SystemBusiness();

            List<UserInfo> itemList = new List<UserInfo>();

            itemList = _SysInfoBusiness.GetEntityByField<UserInfo>(fieldName,fieldValue);

            return itemList;
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userCode">工号</param>
        /// <param name="password">密码</param>
        /// <returns>1-"用户不能为空"，2-"密码不能为空"，3-"用户不存在"，4-"密码错误"，5-"登录成功"，6-"登录失败"</returns>
        [HttpPost]
        public LoginStatus Login(string userCode, string password)
        {

            SystemBusiness _SysInfoBusiness = new SystemBusiness();

            LoginStatus status = _SysInfoBusiness.Login(userCode, password);
            return status;
        }

        /// <summary>
        /// 用户退出业务
        /// </summary>
        ///<param name="userId">用户ID </param>
        /// <returns></returns>
        [HttpPost]
        public void ApplicationExit(string userId)
        {

            SystemBusiness _SysInfoBusiness = new SystemBusiness();

             _SysInfoBusiness.ApplicationExit(userId);
        }

        /// <summary>
        /// 获取父级下可以访问的子菜单
        /// </summary>
        ///<param name="parentId">菜单父级ID </param>
        ///<param name="languageCode">语言代码 </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<IEnumerable<MenuInfo>> GetSubMenus(string parentId, string languageCode)
        {

            SystemBusiness _SysInfoBusiness = new SystemBusiness();

            List<MenuInfo> itemList = _SysInfoBusiness.GetSubMenus(parentId);
            if (!string.IsNullOrWhiteSpace(languageCode))
            {
                //返回指定语言
                itemList = _SysInfoBusiness.UpdateLanguageMenus(itemList, languageCode);
            }

            return itemList;
        }

        /// <summary>
        /// 获取父级下可以用户可以访问的子菜单
        /// </summary>
        ///<param name="userId">用户ID </param>
        ///<param name="parentId">菜单父级ID </param>
        ///<param name="languageCode">语言代码 </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<IEnumerable<MenuInfo>> GetUserSubMenus(string userId,string parentId, string languageCode)
        {

            SystemBusiness _SysInfoBusiness = new SystemBusiness();

            List<MenuInfo> itemList=_SysInfoBusiness.GetUserSubMenus(userId, parentId);


            if (!string.IsNullOrWhiteSpace(languageCode))
            {
                //返回指定语言
                itemList = _SysInfoBusiness.UpdateLanguageMenus(itemList, languageCode);
            }

            return itemList;
        }



        /// <summary>
        /// 保存角色跟菜单项的关系
        /// </summary>
        /// <param name="cmd">输入角色ID 和 菜单列</param>
        /// <returns></returns>
        [HttpPost]
        public bool SaveRoleMenuRelationship(OPEntityCmdField cmd)
        {

            List<RoleMenuRelationship> itemList = JsonUtil.fromJson<List<RoleMenuRelationship>>(cmd.EntityJson);


            SystemBusiness _SysInfoBusiness = new SystemBusiness();

            return _SysInfoBusiness.SaveRoleMenuRelationship(cmd.fieldValue, itemList);

        }


        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
