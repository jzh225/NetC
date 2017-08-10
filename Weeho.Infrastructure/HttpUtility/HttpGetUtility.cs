using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Weeho.Infrastructure.HttpUtility
{
    /// <summary>
    /// Class HttpGetUtility.
    /// </summary>
    public static class HttpGetUtility
    {
        #region 同步方法

        /// <summary>
        /// GET方式请求URL，并返回T类型
        /// </summary>
        /// <typeparam name="T">接收JSON的数据类型</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="maxJsonLength">允许最大JSON长度</param>
        /// <returns>T.</returns>
        public static T GetJson<T>(string url, Encoding encoding = null)
        {
            string returnText = RequestUtility.HttpGet(url, encoding);


            JavaScriptSerializer js = new JavaScriptSerializer();


            T result = js.Deserialize<T>(returnText);

            return result;
        }
        public static T GetJson<T>(string url, CookieContainer cookieContainer = null,
            SortedDictionary<string, string> headerValues = null,
           Encoding encoding = null, X509Certificate cer = null, int timeOut = 10000)
        {
            string returnText = RequestUtility.HttpGet(url, cookieContainer, headerValues, encoding, cer, timeOut);
            JavaScriptSerializer js = new JavaScriptSerializer();

            T result = js.Deserialize<T>(returnText);

            return result;
        }
        /// <summary>
        /// 从Url下载
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="stream">The stream.</param>
        public static void Download(string url, Stream stream)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            WebClient wc = new WebClient();
            var data = wc.DownloadData(url);
            foreach (var b in data)
            {
                stream.WriteByte(b);
            }
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 异步GetJsonA
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="maxJsonLength">允许最大JSON长度</param>
        /// <returns>Task&lt;T&gt;.</returns>
        /// <exception cref="ErrorJsonResultException"></exception>
        public static async Task<T> GetJsonAsync<T>(string url, Encoding encoding = null, int? maxJsonLength = null)
        {
            string returnText = await RequestUtility.HttpGetAsync(url, encoding);

            JavaScriptSerializer js = new JavaScriptSerializer();
            if (maxJsonLength.HasValue)
            {
                js.MaxJsonLength = maxJsonLength.Value;
            }

            T result = js.Deserialize<T>(returnText);

            return result;
        }

        /// <summary>
        /// 异步从Url下载
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>Task.</returns>
        public static async Task DownloadAsync(string url, Stream stream)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            WebClient wc = new WebClient();
            var data = await wc.DownloadDataTaskAsync(url);
            await stream.WriteAsync(data, 0, data.Length);
            //foreach (var b in data)
            //{
            //    stream.WriteAsync(b);
            //}
        }

        #endregion
    }
}
