using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Weeho.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            //ViewBag.Admin = admin;
            return View();
        }

        public ActionResult Echarts()
        {
            return View();
        }

        public ActionResult Ueditor()
        {
            return View();
        }
    }
}