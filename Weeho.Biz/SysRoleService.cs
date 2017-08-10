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
    public class SysRoleService : BaseService<SysRole>, ISysRoleService
    {
        private readonly ISysRoleRepository _SysRoleRepository;
        private readonly IRedisCachingService _redisCachingService;
        public SysRoleService(ISysRoleRepository SysRoleRepository, IRedisCachingService redisCachingService)
            : base(SysRoleRepository, redisCachingService)
        {
            _SysRoleRepository = SysRoleRepository;
            _redisCachingService = redisCachingService;
        }
    }
}
