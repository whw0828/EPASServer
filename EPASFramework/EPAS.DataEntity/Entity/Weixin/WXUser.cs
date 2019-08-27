using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Entity.Weixin
{
    public class WXUser
    {
        [DisplayName("subscribe")]
        public int subscribe
        {
            get;
            set;
        }
        [DisplayName("openid")]
        public string openid
        {
            get;
            set;
        }
        [DisplayName("nickname")]
        public string nickname
        {
            get;
            set;
        }
        /// <summary>
        /// 1-男性 2-女性
        /// </summary>
        [DisplayName("sex")]
        public int sex
        {
            get;
            set;
        }
        [DisplayName("city")]
        public string city
        {
            get;
            set;
        }
        [DisplayName("country")]
        public string country
        {
            get;
            set;
        }
        [DisplayName("province")]
        public string province
        {
            get;
            set;
        }
        [DisplayName("language")]
        public string language
        {
            get;
            set;
        }
        /// <summary>
        /// 头像访问网址
        /// </summary>
        //[DisplayName("headimgurl")]
        //public string headimgurl
        //{
        //    get;
        //    set;
        //}
        //[DisplayName("subscribe_time")]
        //public int subscribe_time
        //{
        //    get;
        //    set;
        //}

        //[DisplayName("unionid")]
        //public string unionid
        //{
        //    get;
        //    set;
        //}
        //[DisplayName("remark")]
        //public string remark
        //{
        //    get;
        //    set;
        //}
        //[DisplayName("groupid")]
        //public int groupid
        //{
        //    get;
        //    set;
        //}
        //[DisplayName("errcode")]
        //public string errcode
        //{
        //    get;
        //    set;
        //}
        //[DisplayName("ErrorCodeValue")]
        //public int ErrorCodeValue
        //{
        //    get;
        //    set;
        //}
    }
}
