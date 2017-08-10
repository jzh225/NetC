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
    public class SysLogService : BaseService<SysLog>, ISysLogService
    {
        private readonly ISysLogRepository _SysLogRepository;
        private readonly IRedisCachingService _redisCachingService;
        public SysLogService(ISysLogRepository SysLogRepository, IRedisCachingService redisCachingService)
            : base(SysLogRepository, redisCachingService)
        {
            _SysLogRepository = SysLogRepository;
            _redisCachingService = redisCachingService;
        }
    }
}
