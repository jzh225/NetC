using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weeho.Common.Helper;

namespace Weeho.Web.Areas.Admin.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult ValidateCode()
        {
            return File(VerifyCode.CharsVertify(), "image/jpeg");
        }
    }
}