using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Model.Entity;

namespace Weeho.Biz.Interface
{
    public interface ISysActionService : IService<SysAction>
    {
        IEnumerable<Model.Custom.Action> GetList(int adminId);
    }
}
