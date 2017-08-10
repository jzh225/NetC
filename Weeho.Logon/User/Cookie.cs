using System;
using System.Web;
using System.Web.Security;
using Weeho.Logon;

namespace Weeho.Logon.User
{
    /// <summary>
    /// Cookie的写入读取操作
    /// </summary>
    class Cookie
    {
        /// <summary>
        /// 写入即时过期的Cookie
        /// </summary>
        /// <param name="userData"></param>
        public static void WriteCookie(UserLoginEncrypt userData)
        {
            int userCookieSplitNum = Config.cookieName.Length;
            int len = userData.UserData.Length / userCookieSplitNum;

            for (int i = 0; i < userCookieSplitNum; i++)
            {
                string tmp = userData.UserData.Substring(i * len, len);
                if ((i + 1) == userCookieSplitNum)
                    tmp = userData.UserData.Substring(i * len);

                HttpCookie cookie = new HttpCookie(Config.cookieName[i], tmp);
                //cookie.Domain = Config.cookieDomain;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 写入带有过期的Cookie
        /// </summary>
        /// <param name="userData"></param>
        public static void WriteCookie(UserLoginEncrypt userData, DateTime ExpiresDate)
        {
            int userCookieSplitNum = Config.cookieName.Length;
            int len = userData.UserData.Length / userCookieSplitNum;

            for (int i = 0; i < userCookieSplitNum; i++)
            {
                string tmp = userData.UserData.Substring(i * len, len);
                if ((i + 1) == userCookieSplitNum)
                    tmp = userData.UserData.Substring(i * len);

                HttpCookie cookie = new HttpCookie(Config.cookieName[i], tmp);
                //cookie.Domain = Config.cookieDomain;
                cookie.Expires = ExpiresDate;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 登出时，清除Cookie
        /// </summary>
        public static void ClearCookie()
        {
            try
            {
                HttpContext.Current.Session.Remove(Config.sessionUserName);
                HttpContext.Current.Session.Remove(Config.sessionMd5UserModel);
                HttpContext.Current.Session.Remove(Config.sessionUserName);
            }
            catch { }

            for (int i = 0; i < Config.cookieName.Length; i++)
            {
                HttpCookie cookie = new HttpCookie(Config.cookieName[i]);
                //cookie.Domain = Config.cookieDomain;
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}
