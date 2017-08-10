using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Common;
using Weeho.DependencyResolver;
using Weeho.Model;
using Weeho.Biz.Interface;
using System.Data.SqlClient;
using Weeho.Model.Entity;
using Weeho.Model.Custom;

namespace Weeho.Authority
{
    public class MenuAuthority
    {
        public static List<Menu> GetMenu(int adminID, bool isAdmin)
        {
            ISysMenuService SysMenuService = IOC.Resolve<ISysMenuService>();

            var  list = SysMenuService.GetList(adminID, isAdmin).ToList();

            var menuList = MenuTree(list);

            return menuList;
        }

        public static string GetMenuJson(int adminID, bool isAdmin)
        {
            ISysMenuService SysMenuService = IOC.Resolve<ISysMenuService>();

            var list = SysMenuService.GetList(adminID, isAdmin).ToList();

            var menuList = MenuTree(list);

            var result= Common.Helper.JsonHelper.Serializer(menuList);

            return Common.Helper.JsonHelper.Serializer(menuList);
        }

        /// <summary>
        /// 树形化菜单数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<Model.Custom.Menu> MenuTree(List<Model.Custom.Menu> list)
        {
            List<Model.Custom.Menu> menuList = new List<Model.Custom.Menu>();

            foreach (var item in list.Where(p => p.ParentId == 0).OrderBy(o => o.SortId))
            {
                menuList.Add(item);
                //递归调用
                MenuFormate(list, item.Id, ref menuList);
            }
            return menuList;
        }

        private static void MenuFormate(List<Model.Custom.Menu> list, int parentId, ref List<Model.Custom.Menu> newList)
        {
            foreach (var item in list.Where(p => p.ParentId == parentId).OrderBy(o => o.SortId))
            {
                newList.Add(item);
                //递归调用
                MenuFormate(list, item.Id, ref newList);
            }
        }
    }
}
