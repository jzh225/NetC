using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Weeho.Biz.Interface;
using Weeho.Model.Entity;
using Weeho.Caching.Redis;
using Weeho.Infrastructure;
using Weeho.Dac.Interface;

namespace Weeho.Biz
{
    public class UsersService : BaseService<Users>, IUsersService
    {
        private readonly IUsersRepository _UsersRepository;
        private readonly IRedisCachingService _redisCachingService;
        public UsersService(IUsersRepository UsersRepository, IRedisCachingService redisCachingService)
            : base(UsersRepository, redisCachingService)
        {
            _UsersRepository = UsersRepository;
            _redisCachingService = redisCachingService;
        }

        public Users Login(string loginName, string plaintext, string ipAddress)
        {
            var model = new Users();

            string passwdSha256 = Security.SHA256(plaintext);

            Expression<Func<Users, bool>> lam = w => w.Email == loginName || w.Phone == loginName;

            if (_UsersRepository.Count(lam) > 0)
            {
                model = _UsersRepository.Get(lam);

                if (model.Password == passwdSha256)
                {
                    //登录成功
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
            throw new NotImplementedException();
        }

    }
}
