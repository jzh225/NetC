using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Weeho.Logon;
using Weeho.Biz.Interface;
using Weeho.DependencyResolver;
using Weeho.Infrastructure;

namespace Weeho.Logon.User
{
    public class Main
    {
        static IUsersService UserService = IOC.Resolve<IUsersService>();
        /// <summary>
        /// 用户登录
        /// </summary>
        public static bool Login(string loginName, string plaintext, string ipAddress, bool IsAuto)
        {
            bool result = false;

            var model = UserService.Login(loginName, plaintext, ipAddress);

            //判断是否登录成功
            if (model != null)
            {
                result = true;
                //读取登录日志

                #region 保存票据
                UserLoginEncrypt userLoginEncryptData = new UserLoginEncrypt();

                userLoginEncryptData.UserData = string.Format(Config.userData
                        , model.Id
                        //, model.UserGuid
                        , loginName
                        , plaintext
                        , model.Phone
                        , model.Email
                        , model.NickName
                        , model.bgpic
                        , model.sex
                        , model.IsCertification
                    );

                if (!string.IsNullOrEmpty(userLoginEncryptData.UserData))
                {
                    result = true;
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
                        Cookie.WriteCookie(userLoginEncryptData);
                }
                #endregion
            }
            return result;
        }

        /// <summary>
        /// 二次登录鉴权
        /// </summary>
        internal static bool ReLogin(int id, string plaintext)
        {
            return UserService.ReLogin(id, plaintext);
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
                var model = UserService.Get(w => w.Id == loginData.Id);
                if (model != null)
                {
                    #region 保存票据
                    UserLoginEncrypt userLoginEncryptData = new UserLoginEncrypt();
                    userLoginEncryptData.UserData = string.Format(Config.userData
                            , model.Id
                            , loginData.LoginName
                            , loginData.Password
                            , model.Phone
                            , model.Email
                            , model.NickName
                            , model.bgpic
                            , model.sex
                            , model.IsCertification
                            //, loginData.Logins
                            //, loginData.LoginDate
                            //, loginData.LoginIp
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
