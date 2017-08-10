using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Model.Entity;

namespace Weeho.Biz.Interface
{
    public interface IUsersService : IService<Users>
    {
        Users Login(string loginName, string plaintext, string ipAddress);
        bool ReLogin(int id, string plaintext);
    }
}
