using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Weeho.Model.Entity;
using Weeho.Infrastructure.Extensions;
using Weeho.Biz.Interface;
using System.ComponentModel;
using Weeho.Infrastructure;
using Weeho.Models;

namespace Weeho.Web.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ISysAdminService _sysAdminService;

        public AccountController(ISysAdminService sysAdminService)
        {
            _sysAdminService = sysAdminService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [Description("管理员登录")]
        [HttpPost]
        public ActionResult Login(string loginname, string password, string checkCode, string remember)
        {
            try
            {
                var IsAuto = remember == "1";

                var result = Logon.Admin.Main.Login(loginname, password, UserIP, checkCode, IsAuto);

                if (result.IsNullOrEmpty())
                    return JsonSuccess("");
                else
                    return JsonError(result);
            }
            catch (Exception ex)
            {
                Common.Log4Net.Log.WriteLog(typeof(Exception), ex);
            }
            return JsonError("登录失败");
        }
        public ActionResult Logout()
        {
            Logon.Admin.Main.Logout(UserIP);

            return Redirect(GetQueryString("returnUrl", "/Admin"));
        }
        [AllowAnonymous]
        public ActionResult NoAccess()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}