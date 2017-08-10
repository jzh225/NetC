using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Weeho.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var result = new Weeho.Authority.MenuAuthority().GetMenu(10013);
            //var result = Weeho.Logon.Admin.Main.Login("admin", "123456", "");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}