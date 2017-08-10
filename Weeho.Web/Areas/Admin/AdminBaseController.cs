using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Weeho.Common.Constant;
using Weeho.Logon.Admin;
using Weeho.Models;
using Weeho.Infrastructure.Extensions;
using Weeho.DependencyResolver;
using Weeho.Biz.Interface;

namespace Weeho.Web.Areas.Admin
{
    /// <summary>
    /// Class UserBaseController.
    /// </summary>
    /// <seealso cref="Common.Controllers.BaseController" />

    public class AdminBaseController : BaseController
    {
        public UserLoginData admin = null;
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            if (!Main.IsLogin())
            {
                Response.Redirect("/Admin/Account/Login?returnUrl=" + filterContext.HttpContext.Request.Url.PathAndQuery);
            }
            else
            {
                var result = Main.IsSingleLogin();
                if (result.IsNullOrEmpty())
                {
                    admin = Logon.Admin.Main.LoginData;
                    ViewBag.Admin = admin;
                    ViewBag.Title = GetTilte();
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('"+ result+"');location = '/Admin/Account/Login?returnUrl="+ filterContext.HttpContext.Request.Url.PathAndQuery + "';</script>");
                    Response.End();
                }   
            }
        }

        public override void Log(string content, LogType logType = LogType.Info)
        {
            content = "管理员：" + admin.LoginName + "-" + content;
            base.Log(content, logType); 
        }

        #region 获取页面title
        public string GetTilte()
        {
            string controller = ControllerContext.RouteData.Values["controller"].ToString().ToLower(),
                action = ControllerContext.RouteData.Values["action"].ToString().ToLower();

            var menus = Weeho.Authority.MenuAuthority.GetMenu(admin.Id, admin.IsAdmin);

            var menu = IOC.Resolve<ISysMenuService>().Get(w => controller.Equals(w.Controller.ToLower()) && action.Equals(w.Action.ToLower()));

            return menu != null ? menu.Name : "";
        }
        #endregion
    }
}
