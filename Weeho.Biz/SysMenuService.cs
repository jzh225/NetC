using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Biz;
using Weeho.Biz.Interface;
using Weeho.Caching.Redis;
using Weeho.Dac.Interface;
using Weeho.Model.Entity;

namespace Weeho.Biz
{
    public class SysMenuService : BaseService<SysMenu>, ISysMenuService
    {
        private readonly ISysMenuRepository _SysMenuRepository;
        private readonly IRedisCachingService _redisCachingService;
        public SysMenuService(ISysMenuRepository SysMenuRepository, IRedisCachingService redisCachingService)
            : base(SysMenuRepository, redisCachingService)
        {
            _SysMenuRepository = SysMenuRepository;
            _redisCachingService = redisCachingService;
        }

        public IEnumerable<Model.Custom.Menu> GetList(int adminId, bool isAdmin)
        {
            return _SysMenuRepository.GetList(adminId, isAdmin);
        }
    }
}
