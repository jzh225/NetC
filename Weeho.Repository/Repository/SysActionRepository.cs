using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Dac.Interface;
using Weeho.Model;
using Weeho.Model.Entity;

namespace Weeho.Dac.Repository
{
    public class SysActionRepository : BaseRepository<SysAction>, ISysActionRepository
    {
        public SysActionRepository(IContext dbContext)
            : base((DbContext)dbContext)
        {

        }

        public IEnumerable<Model.Custom.Action> GetList(int adminId)
        {
            string sql = string.Format(@"select a.Id,a.Name,a.ActionCode,a.ActionIcon,a.ActionTitle,a.IsShow from SysRoleAction ra left join SysAdminRole ar on ra.RoleId=ar.RoleId left join SysAction a on ra.ActionId=a.Id and a.IsShow=1 where ar.AdminId={0}", adminId);

            return _dataContext.Database.SqlQuery<Model.Custom.Action>(sql);
        }
    }
}
