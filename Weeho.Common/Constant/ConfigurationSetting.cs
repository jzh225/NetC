using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Common.Constant
{
    /// <summary>
    /// Class ConfigurationSetting.
    /// </summary>
    public class ConfigurationSetting
    {
        /// <summary>
        /// The host
        /// </summary>
        public static string Host = ConfigurationManager.AppSettings["Host"];
        public static string UserApiHost = ConfigurationManager.AppSettings["UserApiHost"];
        public static string ImagePath = ConfigurationManager.AppSettings["ImagePath"];
        public static string WebName = ConfigurationManager.AppSettings["WebName"];
        public static string WebNameEng = ConfigurationManager.AppSettings["WebNameEng"];
        //网站域名
        public static string WebHostName = ConfigurationManager.AppSettings["Host"];
        public static string ImgVirtual = ConfigurationManager.AppSettings["ImgVirtual"];
        public static string CustomerHostName = ConfigurationManager.AppSettings["CustomerHostName"];
        public static string IsOnline = ConfigurationManager.AppSettings["IsOnline"];
        //支付宝
        //public static string AlipayPartner = ConfigurationManager.AppSettings["AlipayPartner"];
        //public static string AlipayKey = ConfigurationManager.AppSettings["AlipayKey"];
        ////微信接口地址
        //public static string SnatchApiHost = ConfigurationManager.AppSettings["SnatchApiHost"];
        ////公众号信息接口地址
        //public static string WeiXinInFo = string.Format("{0}/ggjy/gzhxx", SnatchApiHost);
        ////公众号推送时间接口
        //public static string WeiXinPushTime = string.Format("{0}/ggjy/tssj", SnatchApiHost);
        ////公众号数据概览接口
        //public static string WeiXinDataOverview = string.Format("{0}/ggjy/sjgl", SnatchApiHost);
        ////公众号阅读量接口
        //public static string WeiXinReadingVolume = string.Format("{0}/ggjy/ydl", SnatchApiHost);
        ////公众号文章接口
        //public static string WeiXinArticle = string.Format("{0}/ggjy/rwcx", SnatchApiHost);
        ////公众号文章按时间分类接口
        //public static string WeiXinArticleDateGroup = string.Format("{0}/ggjy/zxfb", SnatchApiHost);
        ////公众号详情大接口
        //public static string WeiXinDetails = string.Format("{0}/ggjy/big_1", SnatchApiHost);
        //密码正则
        //public static string PasswordRegex = @"^(?![^a-zA-Z]+$)(?!\D+$).{6,16}$";


        public static bool Debug = ConfigurationManager.AppSettings["Debug"] == "1";


        //供应商后台首页
        public static string SupplierHomePage = "/Supplier/Index";
        //分支公司用户后台首页
        public static string CompanyHomePage = "/CompanyAdmin/Index";
        public static string MediaHomePageLogin = "/MediaOrder/OrderList?from=login";
        public static string MediaHomePage = "/MediaOrder/OrderList";

        public static string MediaListPage = "/Media/MediaList";
        public static string WeiXinToKen= ConfigurationManager.AppSettings["token"];

        //微信公众号网页授权接口
        public static string WeiXinWebPageAuthorization = "https://open.weixin.qq.com/connect/oauth2/authorize?";
        //微信获取网页版access_token接口
        public static string WeiXinGetWebToken = "https://api.weixin.qq.com/sns/oauth2/access_token?";
        //得到用户信息通过网页版access_token
        public static string WeiXinWebUserInfo = "https://api.weixin.qq.com/sns/userinfo?";
        //获取调用微信接口的token,只有用户关注后才可以调用
        public static string WeiXinGetToKenUrl = "https://api.weixin.qq.com/cgi-bin/token?";
        //设置微信菜单接口
        public static string SetWeiXinCaiDan = "https://api.weixin.qq.com/cgi-bin/menu/create?";
        //创建临时二维码接口
        public static string GetLinShiEeWeiMaUrl = "https://api.weixin.qq.com/cgi-bin/qrcode/create?";

        public static string EmailHostAddress = ConfigurationManager.AppSettings["EmailHostAddress"];

        public static string Jpush_app_key = ConfigurationManager.AppSettings["Jpush_app_key"];
        public static string Jpush_master_secret = ConfigurationManager.AppSettings["Jpush_master_secret"];
    }
}
