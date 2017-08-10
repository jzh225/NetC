using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Logon
{
    /// <summary>
    /// 登录的数据的加密模型
    /// </summary>
    [Serializable]
    public class UserLoginEncrypt
    {
        public UserLoginEncrypt(){ }
        protected string _userData = string.Empty;

        /// <summary>
        /// 会员登录成功之后加密的数据
        /// </summary>
        public string UserData
        {
            get { return _userData; }
            set { this._userData = value; }
        }
    }
}
