using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Model.Entity;

namespace Weeho.Biz.Interface
{
    public interface ISysMenuService : IService<SysMenu>
    {
        IEnumerable<Model.Custom.Menu> GetList(int adminId, bool isAdmin);
    }
}
