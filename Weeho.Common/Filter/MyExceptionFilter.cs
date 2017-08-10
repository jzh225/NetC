using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zhongtaixie.Common.Helper;

namespace Weeho.Common.Filter
{
    public class ExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //FileLogger logger = new FileLogger();       
            Common.Log4Net.Log.WriteLog(typeof(ExceptionContext), filterContext.Exception);
            if (filterContext.HttpContext.Request.HttpMethod.ToLower() == "post")
            {
                filterContext.Result = new JsonResult {
                    Data= new
                    {
                        success = false,
                        msg = "发生内部错误，请稍后重试。"
                    }
                };
                filterContext.HttpContext.Response.StatusCode = 200;
            }      
            base.OnException(filterContext);

        }
    }
}