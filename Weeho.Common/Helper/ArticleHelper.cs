using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace zhongtaixie.Common.Helper
{
    public class ArticleHelper
    {
        public static string GetContentFromUrll(string _requestUrl)
        {
            string _StrResponse = "";
            HttpWebRequest _WebRequest = (HttpWebRequest)WebRequest.Create(_requestUrl);
            _WebRequest.Method = "GET";
            WebResponse _WebResponse = _WebRequest.GetResponse();
            StreamReader _ResponseStream = new StreamReader(_WebResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
            _StrResponse = _ResponseStream.ReadToEnd();
            _WebResponse.Close();
            _ResponseStream.Close();
            return _StrResponse;
        }

        public static void DownloadImg(string html_url_file, string fileurl)
        {
            if (fileurl.Contains('.'))//url路径必须是绝对路径 例如http://xxx.com/img/logo.jpg 
            {
                WebClient mywebclient = new WebClient();
                mywebclient.DownloadFile(html_url_file, fileurl);
            }
        }
    }
}
