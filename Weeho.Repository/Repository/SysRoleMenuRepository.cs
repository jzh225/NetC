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
    public class SysRoleMenuRepository : BaseRepository<SysRoleMenu>, ISysRoleMenuRepository
    {
        public SysRoleMenuRepository(IContext dbContext)
            : base((DbContext)dbContext)
        {
            
        }
    }
}
