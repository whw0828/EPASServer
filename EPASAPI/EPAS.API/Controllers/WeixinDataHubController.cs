using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPAS.DataEntity.Enum;
using EPAS.Business.UserSystem;
using EPAS.DataEntity.Entity.Common;
using Microsoft.AspNetCore.Mvc;
using EPAS.Component.DataHub;
using EPAS.DataEntity.Entity.MES;
using EPAS.DataEntity.Entity.Weixin;

namespace EPAS.API.Controllers
{

    /// <summary>
    /// 腾讯微信消息接口
    /// 公众号关注用户
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeixinDataHubController : ControllerBase
    {



        /// <summary>
        /// 返回微信公众号中关注的用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<IEnumerable<WXUser>> GetAllWeixinUsers(string SubSystemName)
        {
         
            //Data Hub 查询命令
            string SqlCommand = "select * from wxuser";

            List<WXUser> itemList = DataHubHelper.GetDataFromDataHub<WXUser>(DataHubHelper.WeixinBaseUrl,
                DataHubHelper.apiUrl, SubSystemName, SqlCommand);



            return itemList;
        }

        /// <summary>
        /// 发送微信公众号通知
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool SendBusinessNotification(string noticeJson, string SubSystemName)
        {
            //Data Hub 查询命令
            string SqlCommand = noticeJson;

            bool itemList = DataHubHelper.UpLoadDataToDataHub(DataHubHelper.WeixinBaseUrl,
                DataHubHelper.apiUrl, SubSystemName, SqlCommand);

            return true;
        }


 
    }
}
