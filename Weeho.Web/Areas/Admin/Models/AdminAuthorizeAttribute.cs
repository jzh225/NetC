using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Weeho.Biz.Interface;
using Weeho.DependencyResolver;

namespace Weeho.Web.Areas.Admin.Models
{
    public class AdminAuthorizeAttribute: AuthorizeAttribute
    {
        public AdminAuthorizeAttribute()
        {
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.RequestContext.HttpContext;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                if (httpContext.Request.HttpMethod.ToLower() == "post")
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            success = false,
                            msg = "无权操作"
                        }
                    };
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Admin/Account/NoAccess?returnUrl=" + filterContext.HttpContext.Request.Url.PathAndQuery);
                }
               
            }
            else
            {
                filterContext.Result = new RedirectResult("/Admin/Account/Login?returnUrl=" + filterContext.HttpContext.Request.Url.PathAndQuery);
            }
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            //根据用户角色
            string actionName = httpContext.Request.RequestContext.RouteData.Values["action"].ToString().ToLower();
            string controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString().ToLower();
            string httpMethod = httpContext.Request.HttpMethod.ToLower();


            ISysAdminService sysAdminService = IOC.Resolve<ISysAdminService>();
            //判断是否超级管理员
            var sysAdmin = sysAdminService.Get(m=> m.IsAdmin &&  m.UserName == httpContext.User.Identity.Name);
            if (sysAdmin != null && sysAdmin.Id>0 )
            {
                return base.AuthorizeCore(httpContext);
            }

            ISysAdminRoleService sysAdminRoleService = IOC.Resolve<ISysAdminRoleService>();
            var entity = sysAdminRoleService.Get(m => m.SysRole.SysRoleMenu.Any(w => w.SysMenu.Controller.ToLower() == controllerName && w.SysMenu.Action.ToLower() == actionName));
            if (entity != null && entity.Id>0)
            {
                return base.AuthorizeCore(httpContext);
            }

            return false;
        }
    }
}
