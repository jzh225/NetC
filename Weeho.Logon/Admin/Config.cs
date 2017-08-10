using System;

namespace Weeho.Logon.Admin
{
    /// <summary>
    /// 登录系统所需要一些基本配置
    /// </summary>
    class Config
    {
        #region 声明所有需要的私用变量

        /// <summary>
        /// Cookie组名称
        /// </summary>        
        public static readonly string[] cookieName = { "weeho_admin_SESS",
                                                         "weeho_admin_OPEN",
                                                         "weeho_admin_LAN",
                                                         "weeho_admin_ID",
                                                         "weeho_admin_NPI" };
        /// <summary>
        /// 存储用户数据
        /// </summary>
        public static string userData = @"
<user>
  <id>{0}</id>  
  <loginName>{1}</loginName>
  <password>{2}</password>
  <nicknName>{3}</nicknName>
  <status>{4}</status>
  <loginDate>{5}</loginDate>
  <loginIp>{6}</loginIp>  
  <isAdmin>{7}</isAdmin>  
</user>";

        /// <summary>
        /// Cookies域名名称
        /// </summary>
        public static string cookieDomain
        {
            get
            {
                string[] _host = System.Web.HttpContext.Current.Request.Url.Host.Split('.');
                string host = "";
                if (_host.Length <= 1)
                    host = _host[0];
                else
                    host = _host[_host.Length - 2] + "." + _host[_host.Length - 1];
                return host;
            }
        }

        /// <summary>
        /// 票据版本号
        /// </summary>
        public static readonly int version = 20170719;
        #endregion

        /// <summary>
        /// 管理员Session的名称
        /// </summary>
        public static readonly string sessionUserName = "_weeho_admin_UserId";
        /// <summary>
        /// 管理员Session登录数据模型的值
        /// </summary>
        public static readonly string sessionUserModelName = "_weeho_admin_UserModel";
        /// <summary>
        /// MD5校验值
        /// </summary>
        public static readonly string sessionMd5UserModel = "_weeho_admin_UserMd5";
        /// <summary>
        /// 单用户登录的每个用户独立的guid名称
        /// </summary>
        public static string sessionUserGUID = "_weeho_admin_UserGUID";

        public static string cookiesUserGUID = "_weeho_admin_cookies_UserGUID";

    }
}
