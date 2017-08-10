using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Common.Constant;
using Weeho.Infrastructure;

namespace Weeho.Common.Helper
{
    public class LoginHelper
    {
        public static bool Login()
        {
            int count = SessionUtility.Get(ConfigurationKey.UserLoginCountCode, () => 0);
            if (count>5)
                return false;
            count++;
            SessionUtility.Set(ConfigurationKey.UserLoginCountCode,count);
            return true;
        }
    }
}
