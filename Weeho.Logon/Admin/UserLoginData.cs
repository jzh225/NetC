using System;

namespace Weeho.Logon.Admin
{
    /// <summary>
    /// 登录之后的数据模型
    /// </summary>
    [Serializable]
    public class UserLoginData
    {
        public UserLoginData()
        { }

        protected int _id = 0;
        //protected Guid _userguid;
        protected string _loginname;
        protected string _password;
        //protected string _mobile = "";
        //protected string _email;
        protected string _nickname;
        //protected string _photo;
        //protected int _gender = 0;
        protected bool _status;
        //protected int _logins = 0;
        protected DateTime _logindate;
        protected string _loginip;
        protected bool _isAdmin;

        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        /// <summary>
        /// 会员统一标识(GUID)
        /// </summary>
        //public Guid UserGuid
        //{
        //    get { return this._userguid; }
        //    set { this._userguid = value; }
        //}

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName
        {
            get { return this._loginname; }
            set { this._loginname = value; }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        //public string Mobile
        //{
        //    get { return this._mobile; }
        //    set { this._mobile = value; }
        //}

        /// <summary>
        /// 邮箱
        /// </summary>
        //public string Email
        //{
        //    get { return this._email; }
        //    set { this._email = value; }
        //}

        /// <summary>
        /// 姓名
        /// </summary>
        public string NickName
        {
            get { return this._nickname; }
            set { this._nickname = value; }
        }

        /// <summary>
        /// 头像
        /// </summary>
        //public string Photo
        //{
        //    get { return this._photo; }
        //    set { this._photo = value; }
        //}

        /// <summary>
        /// 性别
        /// </summary>
        //public int Gender
        //{
        //    get { return this._gender; }
        //    set { this._gender = value; }
        //}

        /// <summary>
        /// 员工状态
        /// </summary>
        public bool Status
        {
            get { return this._status; }
            set { this._status = value; }
        }

        /// <summary>
        /// 登录次数
        /// </summary>
        //public int Logins
        //{
        //    get { return this._logins; }
        //    set { this._logins = value; }
        //}

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LoginIp
        {
            get { return this._loginip; }
            set { this._loginip = value; }
        }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LoginDate
        {
            get { return this._logindate; }
            set { this._logindate = value; }
        }

        public bool IsAdmin
        {
            get { return this._isAdmin; }
            set { this._isAdmin = value; }
        }
    }
}
