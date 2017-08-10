using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Weeho.Common.Helper
{
    public class VersionControl
    {
        /// <summary>
        /// 在css文件和js文件后加版本号，防止缓存
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string getVersion(string url)
        {
            System.IO.FileInfo objFI;
            string fileName=HttpContext.Current.Server.MapPath(url);
            // 创建objFI对象
            objFI = new System.IO.FileInfo(fileName);
            string result = objFI.LastWriteTime.Ticks.ToString();
            return "v=" + result;
        }
    }
}
