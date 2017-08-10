using System;

namespace Weeho.Logon.Admin
{
    /// <summary>
    /// 摘要：会员的Session操作
    /// 作者：JT
    /// 日期：2017-07-19
    /// </summary>
    class SessionUser
    {
        /// <summary>
        /// 检测会员的Session是否存在
        /// </summary>
        /// <returns></returns>
        public static bool Exists()
        {
            if (System.Web.HttpContext.Current.Session[Config.sessionUserName] != null
                && System.Web.HttpContext.Current.Session[Config.sessionUserModelName] != null
                && !string.IsNullOrEmpty(System.Web.HttpContext.Current.Session[Config.sessionUserName].ToString())
                )
                return true;
            return false;
        }

        /// <summary>
        /// 把会员的账号写入到Session
        /// </summary>        
        public static void WriteSession(int staffId)
        {
            System.Web.HttpContext.Current.Session[Config.sessionUserName] = staffId;
        }

        /// <summary>
        /// 把会员的数据写入到Session
        /// </summary>        
        public static void WriteSession(UserLoginData model)
        {
            System.Web.HttpContext.Current.Session[Config.sessionUserModelName] = model;
        }

        /// <summary>
        /// 把Cookie值的Md5校验码存入Session
        /// </summary>
        public static void WriteSessionMd5(string md5Value)
        {
            System.Web.HttpContext.Current.Session[Config.sessionMd5UserModel] = md5Value;
        }

        /// <summary>
        /// 获取会员的账号
        /// </summary>
        public static string Userid
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[Config.sessionUserName] != null)
                {
                    return System.Web.HttpContext.Current.Session[Config.sessionUserName].ToString();
                }
                return "";
            }
        }

        /// <summary>
        /// 获取会员的详细信息
        /// </summary>
        public static UserLoginData UserModel
        {
            get
            {
                UserLoginData model = new UserLoginData();
                if (System.Web.HttpContext.Current.Session[Config.sessionUserModelName] != null)
                {
                    try
                    {
                        model = (UserLoginData)System.Web.HttpContext.Current.Session[Config.sessionUserModelName];
                    }
                    catch { }
                }
                return model;
            }
        }

        /// <summary>
        /// 获取Cookie的Md5校验码
        /// </summary>
        public static string Md5
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[Config.sessionMd5UserModel] != null)
                {
                    return System.Web.HttpContext.Current.Session[Config.sessionMd5UserModel].ToString();
                }
                return "";
            }
        }
    }
}
