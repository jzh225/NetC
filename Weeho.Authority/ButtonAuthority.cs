using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.DependencyResolver;
using Weeho.Biz.Interface;

namespace Weeho.Authority
{
    public class ButtonAuthority
    {
        public static string GetButtonsJson(int adminID)
        {
            var list = IOC.Resolve<ISysActionService>().GetList(adminID).ToList();

            return Common.Helper.JsonHelper.Serializer(list);
        }

        public static List<Model.Custom.Action> GetButtons(int adminID)
        {
            return IOC.Resolve<ISysActionService>().GetList(adminID).ToList();
        }
    }
}
