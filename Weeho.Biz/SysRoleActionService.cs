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
    public class SysRoleActionService : BaseService<SysRoleAction>, ISysRoleActionService
    {
        private readonly ISysRoleActionRepository _SysRoleActionRepository;
        private readonly IRedisCachingService _redisCachingService;
        public SysRoleActionService(ISysRoleActionRepository SysRoleActionRepository, IRedisCachingService redisCachingService)
            : base(SysRoleActionRepository, redisCachingService)
        {
            _SysRoleActionRepository = SysRoleActionRepository;
            _redisCachingService = redisCachingService;
        }
    }
}
