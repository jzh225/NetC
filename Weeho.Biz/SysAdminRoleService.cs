using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Biz.Interface;
using Weeho.Caching.Redis;
using Weeho.Dac.Interface;
using Weeho.Model.Entity;

namespace Weeho.Biz
{
    public class SysAdminRoleService : BaseService<SysAdminRole>, ISysAdminRoleService
    {
        private readonly ISysAdminRoleRepository _SysAdminRoleRepository;
        private readonly IRedisCachingService _redisCachingService;
        public SysAdminRoleService(ISysAdminRoleRepository SysAdminRoleRepository, IRedisCachingService redisCachingService)
            : base(SysAdminRoleRepository, redisCachingService)
        {
            
        }
    }
}
