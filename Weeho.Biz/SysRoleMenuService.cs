using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Biz.Interface;
using Weeho.Model.Entity;
using Weeho.Caching.Redis;
using Weeho.Dac.Interface;

namespace Weeho.Biz
{
    public class SysRoleMenuService : BaseService<SysRoleMenu>, ISysRoleMenuService
    {
        private readonly ISysRoleMenuRepository _SysRoleMenuRepository;
        private readonly IRedisCachingService _redisCachingService;
        public SysRoleMenuService(ISysRoleMenuRepository SysRoleMenuRepository, IRedisCachingService redisCachingService)
            : base(SysRoleMenuRepository, redisCachingService)
        {
            _SysRoleMenuRepository = SysRoleMenuRepository;
            _redisCachingService = redisCachingService;
        }
    }
}
