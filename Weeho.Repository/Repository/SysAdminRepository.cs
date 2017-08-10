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
    public class SysAdminRepository : BaseRepository<SysAdmin>, ISysAdminRepository
    {
        public SysAdminRepository(IContext dbContext)
            : base((DbContext)dbContext)
        {
            IncludeEntity.Add(m => m.SysAdminRole.Select(x => x.SysRole));
        }
    }
}
