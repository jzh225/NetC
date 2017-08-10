using System;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Weeho.Infrastructure;
using Weeho.Logon;

namespace Weeho.Logon.Admin
{
    /// <summary>
    /// 取得登录用户的数据
    /// Auther: JT
    /// Date:   2017-07-19
    /// </summary>
    class LoginModel : IRequiresSessionState
    {
        public static UserLoginData GetUser()
        {
            /*
             * 采用Cookie和Session双重验证
             * 方法：
             *      1、Session和Cookie同时存在时，直接读取Session值
             *      2、Cookie存在、Session丢失，把Cookie值，自动二次登录生成Session，同时验证Cookie的完整性
             *      3、Cookie丢失，清除Cookie和Session记录
             * 要点：
             *      1、Cookie用于记着用户登录，Session用于即时验证
             *      2、用户在修改加密后的Cookie值时，不会对当前登录账户造成造成影响，只有Session丢失之后，在解密时才会失败，同时清除Cookie值
             */

            //循环读取会员生成的Cookie组数据
            string xml = "";
            for (int i = 0; i < Config.cookieName.Length; i++)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[Config.cookieName[i]];
                if (cookie != null)
                {
                    xml += cookie.Value;
                }
            }
            UserLoginData userLoginData = new UserLoginData();

            //校验cookie值是否已经修改和Session的及时性
            bool isCookieMd5 = false;
            try
            {
                if (SessionUser.Md5 == Security.Md5(xml))
                    isCookieMd5 = true;
            }
            catch { }

            //一、判断Session和Cookie同时存在
            if (SessionUser.Exists() && !string.IsNullOrEmpty(xml) && isCookieMd5)
            {
                try
                {
                    userLoginData = SessionUser.UserModel;
                }
                catch { }
                //System.IO.File.AppendAllText(System.Web.HttpContext.Current.Server.MapPath("/_logs/log.txt"), "SessionUser.Md5\r\n");
            }
            else
            {
                //二、Cookie存在、Session丢失                
                if (!string.IsNullOrEmpty(xml))
                {
                    string encryptXml = xml;
                    xml = Security.DesDecrypt(xml);
                    userLoginData = XmlToModel.ToUser(xml);
                    if (userLoginData != null)
                    {
                        //自动二次登录生成Session
                        UserLoginEncrypt userLoginEncrypt = new UserLoginEncrypt();

                        if (Main.ReLogin(userLoginData.Id, userLoginData.Password))
                        {
                            SessionUser.WriteSession(userLoginData.Id);
                            SessionUser.WriteSession(userLoginData);
                            SessionUser.WriteSessionMd5(Security.Md5(encryptXml));
                        }
                        else
                        {
                            Cookie.ClearCookie();
                        }
                    }
                    else
                    {
                        Cookie.ClearCookie();
                    }

                }
                else
                {
                    //三、Cookie丢失，清除Cookie和Session记录
                    Cookie.ClearCookie();
                }

            }
            return userLoginData;
        }
    }
}
