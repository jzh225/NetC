using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Common.Helper
{
    public class HtmlHelper
    {
        /// <summary>
        /// replaced html tags 
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(string html, int length = 0)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            //trim
            strText = strText.Replace("\r", "");
            strText = strText.Replace("\n", "");
            strText = strText.Trim();

            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);

            return strText;
        }
    }
}
