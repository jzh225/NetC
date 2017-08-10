using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Weeho.Biz.Interface;
using Weeho.Caching.Redis;
using Weeho.Dac.Interface;
using Weeho.Infrastructure;
using Weeho.Model.Entity;

namespace Weeho.Biz
{
    public class SysAdminService : BaseService<SysAdmin>, ISysAdminService
    {
        private readonly ISysAdminRepository _SysAdminRepository;
        private readonly IRedisCachingService _redisCachingService;
        public SysAdminService(ISysAdminRepository SysAdminRepository, IRedisCachingService redisCachingService)
            : base(SysAdminRepository, redisCachingService)
        {
            _SysAdminRepository = SysAdminRepository;
            _redisCachingService = redisCachingService;
        }
        public SysAdmin Login(string loginName, string plaintext, string ipAddress)
        {
            var model = new SysAdmin();

            string passwdSha256 = Security.SHA256(plaintext);

            Expression<Func<SysAdmin, bool>> lam = w => w.UserName == loginName && w.Enabled;

            if (_SysAdminRepository.Count(lam) > 0)
            {
                model = _SysAdminRepository.Get(lam);

                if (model.Password == passwdSha256)
                {
                    //更新登录时间和IP
                    Update(w => w.Id == model.Id, (m) => { m.LastLoginIp = ipAddress; m.LastLoginTime = System.DateTime.Now; });
                }
                else
                {
                    //密码错误，登录失败     
                    //登录失败日志
                    model = null;
                }
            }
            return model;
        }

        public bool ReLogin(int id, string plaintext)
        {
            bool result = false;

            string passwdSha256 = Security.SHA256(plaintext);

            if (_SysAdminRepository.Count(w => w.Id == id && w.Password == passwdSha256) > 0)
                result = true;

            return result;
        }
    }
}
