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
    public class SysActionService : BaseService<SysAction>, ISysActionService
    {
        private readonly ISysActionRepository _SysActionRepository;
        private readonly IRedisCachingService _redisCachingService;
        public SysActionService(ISysActionRepository SysActionRepository, IRedisCachingService redisCachingService)
            : base(SysActionRepository, redisCachingService)
        {
            _SysActionRepository = SysActionRepository;
            _redisCachingService = redisCachingService;
        }

        public IEnumerable<Model.Custom.Action> GetList(int adminId)
        {
            return _SysActionRepository.GetList(adminId);
        }
    }
}
