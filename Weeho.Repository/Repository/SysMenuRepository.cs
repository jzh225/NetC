using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Model.Entity;
using Weeho.Model;
using Weeho.Dac.Interface;

namespace Weeho.Dac.Repository
{
    public class SysMenuRepository : BaseRepository<SysMenu>, ISysMenuRepository
    {
        public SysMenuRepository(IContext dbContext)
            : base((DbContext)dbContext)
        {
            
        }

        public IEnumerable<Model.Custom.Menu> GetList(int adminId, bool isAdmin)
        {
            string sql = string.Empty;

            if (isAdmin)
            {
                sql = string.Format(@"select m.Id,m.Name,m.ParentId,m.Url,m.SortId,m.Controller,m.Action,m.IsShow from SysMenu m where m.IsShow=1");
            }
            else
            {
                sql = string.Format(@"select m.Id,m.Name,m.ParentId,m.Controller,m.Action,m.Url,m.SortId,m.IsShow from SysRoleMenu rm left join SysAdminRole ar on rm.RoleId=ar.RoleId left join SysMenu m on rm.MenuId=m.Id and m.IsShow=1 where ar.AdminId={0}", adminId);
            }

            return _dataContext.Database.SqlQuery<Model.Custom.Menu>(sql);
        }
    }
}
