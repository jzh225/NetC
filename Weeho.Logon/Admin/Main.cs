using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Weeho.Logon;
using Weeho.Biz.Interface;
using Weeho.DependencyResolver;
using Weeho.Common.Helper;
using Weeho.Infrastructure;
using System.Collections;

namespace Weeho.Logon.Admin
{
    public class Main
    {
        static ISysAdminService AdminService = IOC.Resolve<ISysAdminService>();

        /// <summary>
        /// 用户登录
        /// </summary>
        public static string Login(string loginName, string plaintext, string ipAddress, string checkCode, bool IsAuto=false)
        {
            if (!VerifyCode.Validate(checkCode))
                return "验证码输入错误";

            var model = AdminService.Login(loginName, plaintext, ipAddress);

            //判断是否登录成功
            if (model != null)
            {
                #region 保存票据
                UserLoginEncrypt userLoginEncryptData = new UserLoginEncrypt();
                userLoginEncryptData.UserData = string.Format(Config.userData
                        , model.Id
                        , model.UserName
                        , plaintext
                        , model.Name
                        , model.Enabled
                        , model.LastLoginTime
                        , model.LastLoginIp
                        , model.IsAdmin
                    );

                if (!string.IsNullOrEmpty(userLoginEncryptData.UserData))
                {
                    //写入Session值
                    if (!string.IsNullOrEmpty(userLoginEncryptData.UserData))
                    {
                        UserLoginData _data = XmlToModel.ToUser(userLoginEncryptData.UserData);
                        if (_data != null)
                        {
                            SessionUser.WriteSession(_data.Id);
                            SessionUser.WriteSession(_data);
                        }
                        //用于校验cookie值是否被修改和Session的及时性
                        SessionUser.WriteSessionMd5(Security.Md5(userLoginEncryptData.UserData));
                    }
                    //写入Cookie
                    userLoginEncryptData.UserData = Security.DesEncrypt(userLoginEncryptData.UserData);
                    if (IsAuto)
                        Cookie.WriteCookie(userLoginEncryptData, DateTime.Now.AddDays(7));
                    else
                        Cookie.WriteCookie(userLoginEncryptData, DateTime.Now.AddHours(1));

                    #region 单用户登录
                    HttpContext.Current.Session[Config.sessionUserGUID] = Guid.NewGuid().ToString("N");
                    CookieUtility.Save(Config.cookiesUserGUID, HttpContext.Current.Session[Config.sessionUserGUID].ToString(), 10);
                    Hashtable hOnline = (Hashtable)HttpContext.Current.Application["Online"];
                    if (hOnline != null)
                    {
                        IDictionaryEnumerator idE = hOnline.GetEnumerator();
                        string strKey = "";
                        while (idE.MoveNext())
                        {
                            if (idE.Value != null && idE.Value.ToString().Equals(model.Id.ToString()))
                            { 
                                strKey = idE.Key.ToString();
                                hOnline[strKey] = "XXXXXX";
                                break;
                            }
                        }
                    }
                    else
                    {
                        hOnline = new Hashtable();
                    }
                    hOnline[HttpContext.Current.Session[Config.sessionUserGUID].ToString()] = model.Id;
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application["Online"] = hOnline;
                    HttpContext.Current.Application.UnLock();
                    #endregion
                }
                #endregion
                return "";
            }
            else
            {
                return "登录名或密码错误";
            }
        }

        public static string IsSingleLogin()
        {
            string result = string.Empty;
            Hashtable hOnline = (Hashtable)HttpContext.Current.Application["Online"];
            bool isHaveGuid = true;
            string strGUId = string.Empty;
            if (HttpContext.Current.Session[Config.sessionUserGUID] == null)
            {
                if (CookieUtility.Get(Config.cookiesUserGUID) == null)
                {
                    result = "请重新登录";
                    isHaveGuid = false;
                }
                else
                {
                    strGUId = CookieUtility.GetValue(Config.cookiesUserGUID);
                }
            }
            else
            {
                strGUId = HttpContext.Current.Session[Config.sessionUserGUID].ToString();
            }

            if (hOnline != null && isHaveGuid)
            {
                IDictionaryEnumerator idE = hOnline.GetEnumerator();
                while (idE.MoveNext())
                {
                    if (idE.Key != null && idE.Key.ToString().Equals(strGUId))
                    {
                        //already login 
                        if (idE.Value != null && "XXXXXX".Equals(idE.Value.ToString()))
                        {
                            hOnline.Remove(strGUId);
                            HttpContext.Current.Application.Lock();
                            HttpContext.Current.Application["Online"] = hOnline;
                            HttpContext.Current.Application.UnLock();
                            result = "你的帐号已在别处登陆，你被强迫下线！";
                        }
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Session过期或者退出系统时释放资源
        /// </summary>
        public static void ClearApplication()
        {
            Hashtable hOnline = (Hashtable)HttpContext.Current.Application["Online"];
            if (hOnline[HttpContext.Current.Session[Config.sessionUserName].ToString()] != null)
            {
                hOnline.Remove(HttpContext.Current.Session[Config.sessionUserName].ToString());
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["Online"] = hOnline;
                HttpContext.Current.Application.UnLock();
            }
        }

        /// <summary>
        /// 二次登录鉴权
        /// </summary>
        internal static bool ReLogin(int id, string plaintext)
        {
            return AdminService.ReLogin(id, plaintext);
        }

        public static bool IsLogin()
        {
            UserLoginData _data = LoginModel.GetUser();
            if (_data == null || _data.Id == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        public static void Logout(string ipAddress)
        {
            //写入退出日志

            //清空票据
            Cookie.ClearCookie();
        }

        /// <summary>
        /// 取得会员登录信息
        /// </summary>
        public static UserLoginData LoginData
        {
            get
            {
                return LoginModel.GetUser();
            }
        }

        /// <summary>
        /// 清除会员登录缓存信息
        /// </summary>
        public static void ClearUserLogin()
        {
            try
            {
                var loginData = LoginData;

                #region 重新获取并写入Cookie
                var model = AdminService.Get(w => w.Id == loginData.Id);
                if (model != null)
                {
                    #region 保存票据
                    UserLoginEncrypt userLoginEncryptData = new UserLoginEncrypt();
                    userLoginEncryptData.UserData = string.Format(Config.userData
                            , model.Id
                            //, model.UserGuid
                            , model.UserName
                            , loginData.Password
                            //, model.Mobile
                            //, model.Email
                            , model.Name
                            //, model.UserPic
                            //, model.Gender
                            , model.Enabled
                            //, loginData.Logins
                            , loginData.LoginDate
                            , loginData.LoginIp
                            ,loginData.IsAdmin
                        );

                    if (!string.IsNullOrEmpty(userLoginEncryptData.UserData))
                    {

                        //写入Session值
                        if (!string.IsNullOrEmpty(userLoginEncryptData.UserData))
                        {
                            UserLoginData _data = XmlToModel.ToUser(userLoginEncryptData.UserData);
                            if (_data != null)
                            {
                                SessionUser.WriteSession(_data.Id);
                                SessionUser.WriteSession(_data);
                            }
                            //用于校验cookie值是否被修改和Session的及时性
                            SessionUser.WriteSessionMd5(Security.Md5(userLoginEncryptData.UserData));
                        }
                        //写入Cookie
                        userLoginEncryptData.UserData = Security.DesEncrypt(userLoginEncryptData.UserData);
                        Cookie.WriteCookie(userLoginEncryptData, DateTime.Now.AddDays(7));
                    }
                    #endregion
                }
                #endregion

            }
            catch { }
        }
    }
}
