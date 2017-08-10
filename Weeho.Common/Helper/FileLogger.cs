using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Weeho.DependencyResolver;

namespace Weeho.Common.Helper
{
    public class FileLogger
    {
        public void LogException(Exception e, string url)
        {
            string dir = System.Web.Hosting.HostingEnvironment.MapPath("~/") + "App_Data//";
            var filePath = dir + DateTime.Now.ToString("dd-MM-yyyy hh mm ss") + ".txt";
            
            File.WriteAllLines(filePath,
                new string[]
                  {
                      "Page:"+url,
                      "Message:"+e.Message,
                      "Stacktrace:"+e.StackTrace
                  });
        }
    }
}