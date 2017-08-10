using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Model.Entity;
using Weeho.Model;

namespace Weeho.Dac.Repository
{
    public class UsersRepository : BaseRepository<Users>, Interface.IUsersRepository
    {
        public UsersRepository(IContext dbContext)
            : base((DbContext)dbContext)
        {
            //IncludeEntity.Add(m => m.UsersAttachedInfo);
            //IncludeEntity.Add(m => m.UsersAttachedInfo.province1);
            //IncludeEntity.Add(m => m.UsersAttachedInfo.city1);
            //IncludeEntity.Add(m => m.UsersAttachedInfo.county);
            //IncludeEntity.Add(m => m.UsersAttachedInfo.town1);
            //IncludeEntity.Add(m => m.UserBank);
            //IncludeEntity.Add(m => m.Users3.UsersAttachedInfo);
        }
    }
}
