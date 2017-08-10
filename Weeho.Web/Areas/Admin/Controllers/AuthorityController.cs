using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weeho.Biz.Interface;
using Weeho.Dac;
using Weeho.Infrastructure.Extensions;
using Weeho.Model.Entity;

namespace Weeho.Web.Areas.Admin.Controllers
{
    public class AuthorityController : AdminBaseController
    {
        // GET: Admin/Authority

        private readonly ISysActionService _sysActionService;
        private readonly ISysAdminService _sysAdminService;
        private readonly ISysRoleService _sysRoleService;
        private readonly ISysRoleActionService _sysRoleActionService;
        private readonly ISysAdminRoleService _sysAdminRoleService;
        private readonly ISysMenuService _sysMenuService;
        public AuthorityController(ISysActionService sysActionService,
            ISysAdminService sysAdminService,
            ISysRoleService sysRoleService,
            ISysRoleActionService sysRoleActionService,
            ISysAdminRoleService sysAdminRoleService,
            ISysMenuService sysMenuService)
        {
            _sysActionService = sysActionService;
            _sysAdminService = sysAdminService;
            _sysRoleService = sysRoleService;
            _sysRoleActionService = sysRoleActionService;
            _sysAdminRoleService = sysAdminRoleService;
            _sysMenuService = sysMenuService;
        }

        public ActionResult MenuList()
        {
            string keyword = GetQueryString("keyword");
            var result = _sysMenuService.GetMany(m => (string.IsNullOrEmpty(keyword) || m.Name.Contains(keyword)));
            return View(result);
        }

        #region 菜单管理
        public ActionResult Menu()
        {
            int id = GetQueryInt("id");
            return View(_sysMenuService.Get(m => m.Id == id) ?? new SysMenu());
        }
        [HttpPost]
        public ActionResult Menu(SysMenu model)
        {

            if (model.Name.IsNullOrEmpty())
                return JsonError("名称不能为空");
            if (model.Controller.IsNullOrEmpty())
                return JsonError("Controller不能为空");
            if (model.Url.IsNullOrEmpty())
                return JsonError("Url不能为空");
            if (model.Id > 0)
            {
                var menu = _sysMenuService.Get(m => m.Id == model.Id);
                if (menu != null && menu.Id > 0)
                {
                    menu.Name = model.Name;
                    menu.Controller = model.Controller;
                    menu.Action = model.Action;
                    menu.Url = model.Url;
                    menu.ParentId = model.ParentId ?? 0;
                    menu.SortId = model.SortId;
                    menu.IsShow = GetFormCheck("IsShow");
                    if (_sysMenuService.Update(menu) > 0)
                    {
                        return JsonSuccess(menu.Id.ToString());
                    }
                }
            }
            else
            {
                var menu = new SysMenu
                {
                    Id = -1,
                    Name = model.Name,
                    Controller = model.Controller,
                    Action = model.Action,
                    Url = model.Url,
                    ParentId = model.ParentId ?? 0,
                    SortId = model.SortId,
                    IsShow = GetFormCheck("IsShow")
                };
                if (_sysMenuService.Insert(menu) > 0)
                {
                    return JsonSuccess(menu.Id.ToString());
                }
            }

            return JsonError("操作失败，请稍后重试");

        }
        [HttpPost]
        public ActionResult MenuDelete(int[] ids)
        {

            if (_sysMenuService.Delete(m => ids.Contains(m.Id)) > 0)
            {
                LogWarning("删除内容：" + string.Join(",", ids));
                return JsonSuccess("");
            }

            return JsonError("操作失败");
        }

        public ActionResult MenuJson()
        {
            string keyword = GetFormString("keyword");
            int parentId = GetQueryInt("parentId");
            var paging = new Paging
            {
                PageIndex = GetFormInt("page")
            };
            var result = _sysMenuService.GetMany(m => (string.IsNullOrEmpty(keyword) || m.Name.Contains(keyword))
            && (m.ParentId == parentId), paging);
            return Json(result.Item3.Select(m => new
            {
                id = m.Id,
                text = m.Name
            }));
        }
        #endregion

        #region 管理员管理
        public ActionResult AdminList()
        {
            int pageIndex = GetQueryInt("page");
            string keyword = GetQueryString("keyword");
            var paging = new Paging
            {
                PageIndex = pageIndex
            };
            var result = _sysAdminService.GetMany(m => (string.IsNullOrEmpty(keyword) || m.Name.Contains(keyword) || m.UserName.Contains(keyword)),
                paging);

            return View(result);
        }
        public ActionResult Admin()
        {
            int id = GetQueryInt("id");
            return View(_sysAdminService.Get(m => m.Id == id) ?? new SysAdmin());
        }
        [Description("添加/修改管理员")]
        [HttpPost]
        public ActionResult Admin(SysAdmin model, int[] Roles)
        {
            if (model.UserName.IsNullOrEmpty())
                return JsonError("登录名不能为空");

            if (model.Name.IsNullOrEmpty())
                return JsonError("名称不能为空");

            if (model.Id > 0)
            {
                var sysAdmin = _sysAdminService.Get(m => m.Id == model.Id);
                if (sysAdmin != null && sysAdmin.Id > 0)
                {
                    sysAdmin.Name = model.Name;
                    sysAdmin.UserName = model.UserName;
                    if (!model.Password.IsNullOrEmpty())
                    {
                        if (model.Password.IsContainCharAndNum())
                            return JsonError("登录密码格式不正确");
                        if (model.Password != GetFormString("ConfirmPassword"))
                            return JsonError("两次密码输入不一样");

                        sysAdmin.Password = Infrastructure.Security.DesEncrypt(model.Password);
                    }
                    sysAdmin.LastLoginIp = Request.GetUserIP();
                    sysAdmin.LastLoginTime = DateTime.Now;
                    sysAdmin.IsAdmin = GetFormCheck("IsAdmin");
                    sysAdmin.Enabled = GetFormCheck("Enabled");
                    _sysAdminRoleService.Delete(m => m.AdminId == sysAdmin.Id);
                    if (_sysAdminService.Update(sysAdmin) > 0)
                    {
                        _sysAdminRoleService.Insert(Roles.Select(m => new SysAdminRole
                        {
                            AdminId = sysAdmin.Id,
                            RoleId = m
                        }).ToArray());
                        return JsonSuccess(sysAdmin.Id.ToString());
                    }
                }
            }
            else
            {
                if (model.Password.IsContainCharAndNum())
                    return JsonError("登录密码格式不正确");
                if (model.Password != GetFormString("ConfirmPassword"))
                    return JsonError("两次密码输入不一样");

                var sysAdmin = new SysAdmin
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Password = Infrastructure.Security.DesEncrypt(model.Password),
                    LastLoginIp = Request.GetUserIP(),
                    LastLoginTime = DateTime.Now,
                    IsAdmin = GetFormCheck("IsAdmin"),
                    Enabled = GetFormCheck("Enabled")
                };
                if (_sysAdminService.Insert(sysAdmin) > 0)
                {
                    _sysAdminRoleService.Insert(Roles.Select(m => new SysAdminRole
                    {
                        AdminId = sysAdmin.Id,
                        RoleId = m
                    }).ToArray());
                    return JsonSuccess(sysAdmin.Id.ToString());
                }
            }

            return JsonError("操作失败，请稍后重试");

        }
        [Description("删除管理员")]
        [HttpPost]
        public ActionResult AdminDelete(int[] ids)
        {
            if (_sysAdminService.Delete(m => ids.Contains(m.Id) && !m.IsAdmin) > 0)
            {
                return JsonSuccess("");
            }

            return JsonError("操作失败");
        }
        #endregion

        #region 角色
        public ActionResult RoleList()
        {
            int pageIndex = GetQueryInt("page");
            string keyword = GetQueryString("keyword");
            var paging = new Paging
            {
                PageIndex = pageIndex
            };
            var result = _sysRoleService.GetMany(m => (string.IsNullOrEmpty(keyword) || m.Name.Contains(keyword)), paging);

            return View(result);
        }
        public ActionResult Role()
        {
            int id = GetQueryInt("id");
            return View(_sysRoleService.Get(m => m.Id == id) ?? new SysRole());
        }
        [HttpPost]
        public ActionResult Role(SysRole model, int[] Actions)
        {
            if (model.Name.IsNullOrEmpty())
                return JsonError("名称不能为空");
            if (Actions.Length == 0)
                return JsonError("操作项不能为空");
            if (model.Id > 0)
            {
                var role = _sysRoleService.Get(m => m.Id == model.Id);
                if (role != null && role.Id > 0)
                {
                    role.Name = model.Name;
                    _sysRoleActionService.Delete(m => m.RoleId == role.Id);
                    if (_sysRoleService.Update(role) > 0)
                    {
                        _sysRoleActionService.Insert(Actions.Select(m => new SysRoleAction
                        {
                            RoleId = role.Id,
                            ActionId = m
                        }).ToArray());
                        return JsonSuccess(role.Id.ToString());
                    }
                }
            }
            else
            {
                var role = new SysRole
                {
                    Name = model.Name,
                };
                if (_sysRoleService.Insert(role) > 0)
                {
                    _sysRoleActionService.Insert(Actions.Select(m => new SysRoleAction
                    {
                        RoleId = role.Id,
                        ActionId = m
                    }).ToArray());
                    return JsonSuccess(role.Id.ToString());
                }
            }

            return JsonError("操作失败，请稍后重试");

        }
        [HttpPost]
        public ActionResult RoleDelete(int[] ids)
        {
            try
            {
                if (_sysRoleService.Delete(m => ids.Contains(m.Id)) > 0)
                {
                    LogWarning("删除内容：" + string.Join(",", ids));
                    return JsonSuccess("");
                }
            }
            catch
            {
                return JsonError("对不起，角色正在使用，要想删除角色，请先把管理是此角色的用户更改成其它角色！");
            }
            return JsonError("对不起，操作失败！");

        }
        [HttpPost]
        public ActionResult RoleJson()
        {
            string keyword = GetFormString("keyword");
            var paging = new Paging
            {
                PageIndex = GetFormInt("page")
            };
            var result = _sysRoleService.GetMany(m => (string.IsNullOrEmpty(keyword) || m.Name.Contains(keyword)), paging);
            return Json(result.Item3.Select(m => new
            {
                id = m.Id,
                text = m.Name

            }));
        }
        #endregion

        #region 权限项
        public ActionResult ActionList()
        {
            int pageIndex = GetQueryInt("page");
            string keyword = GetQueryString("keyword");
            var paging = new Paging
            {
                PageIndex = pageIndex
            };
            var result = _sysActionService.GetMany(m => (string.IsNullOrEmpty(keyword) || m.Name.Contains(keyword)), paging);

            return View(result);
        }
        public ActionResult Action()
        {
            int id = GetQueryInt("id");
            return View(_sysActionService.Get(m => m.Id == id) ?? new SysAction());
        }
        [HttpPost]
        public ActionResult Action(SysAction model)
        {

            if (model.Name.IsNullOrEmpty())
                return JsonError("名称不能为空");
            if (model.ActionCode.IsNullOrEmpty())
                return JsonError("ActionCode不能为空");

            if (model.Id > 0)
            {
                var action = _sysActionService.Get(m => m.Id == model.Id);
                if (action != null && action.Id > 0)
                {
                    action.Name = model.Name;
                    action.ActionCode = model.ActionCode;
                    action.ActionIcon = model.ActionIcon;
                    action.ActionTitle = model.ActionTitle;
                    if (_sysActionService.Update(action) > 0)
                    {
                        return JsonSuccess(action.Id.ToString());
                    }
                }
            }
            else
            {
                var action = new SysAction
                {
                    Name = model.Name,
                    ActionCode = model.ActionCode,
                    ActionIcon = model.ActionIcon,
                    ActionTitle = model.ActionTitle
                };
                if (_sysActionService.Insert(action) > 0)
                {
                    return JsonSuccess(action.Id.ToString());
                }
            }

            return JsonError("操作失败，请稍后重试");

        }
        [HttpPost]
        public ActionResult ActionDelete(int[] ids)
        {

            if (_sysActionService.Delete(m => ids.Contains(m.Id)) > 0)
            {
                LogWarning("删除内容：" + string.Join(",", ids));
                return JsonSuccess("");
            }

            return JsonError("操作失败");
        }
        [HttpPost]
        public ActionResult ActionJson()
        {
            string keyword = GetFormString("keyword");
            var paging = new Paging
            {
                PageIndex = GetFormInt("page")
            };
            var result = _sysActionService.GetMany(m => (string.IsNullOrEmpty(keyword) || m.Name.Contains(keyword)), paging);
            return Json(result.Item3.Select(m => new
            {
                id = m.Id,
                text = m.Name

            }));//投射
        }
        #endregion
    }
}