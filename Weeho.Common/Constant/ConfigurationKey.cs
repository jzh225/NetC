using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Common.Constant
{
    public class ConfigurationKey
    {
        public static string ValidateCode = "VertifyCode";
        public static string PhoneValidateCode = "PhoneVertifyCode";
        public static string AdminModelSessionCode = "AdminModelSession";
        public static string UserModelSessionCode = "UserModelSession";
        public static string UserLoginNoSignCode = "UserLoginNoSignSession";
        public static string UserLoginCountCode = "UserLoginCountSession";
        public static string PhoneCodeModelSession = "PhoneCodeModelName";

        public static string UserLoginNameCookieCode = "_user_name";
        public static string UserLoginUserIdCode = "_user";
        public static string DefaultAlertCookieCode = "_dacc";
        public static string UserInviteCookieCode = "_user_invite";
        public static string UserSourceCookieCode = "_user_source";

    }
}
