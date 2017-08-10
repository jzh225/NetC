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
    /// Class HttpPostUtility.
    /// </summary>
    public static class HttpPostUtility
    {
        /// <summary>
        /// 获取Post结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnText">The return text.</param>
        /// <returns>T.</returns>
        public static T GetResult<T>(string returnText)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            T result = js.Deserialize<T>(returnText);
            return result;
        }


        #region 同步方法

        /// <summary>
        /// 发起Post请求，可上传文件
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="fileDictionary">The file dictionary.</param>
        /// <param name="postDataDictionary">The post data dictionary.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns>T.</returns>
        public static T PostFileGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null, Dictionary<string, string> postDataDictionary = null, Encoding encoding = null, X509Certificate cer = null, int timeOut = 10000)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                postDataDictionary.FillFormDataStream(ms); //填充formData
                string returnText = RequestUtility.HttpPost(url, cookieContainer, ms, fileDictionary, null, encoding, cer, timeOut);
                var result = GetResult<T>(returnText);
                return result;
            }
        }
        public static string PostGetString(string url, CookieContainer cookieContainer = null, Stream fileStream = null, Encoding encoding = null, X509Certificate cer = null, int timeOut = 10000, bool checkValidationResult = false)
        {
            string returnText = RequestUtility.HttpPost(url, cookieContainer, fileStream, null, null, encoding, cer, timeOut, checkValidationResult);

            return returnText;
        }
        /// <summary>
        /// 发起Post请求
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="fileStream">文件流</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <returns>T.</returns>
        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Stream fileStream = null, Encoding encoding = null, X509Certificate cer = null, int timeOut = 10000, bool checkValidationResult = false)
        {
            string returnText = RequestUtility.HttpPost(url, cookieContainer, fileStream, null, null, encoding, cer, timeOut, checkValidationResult);

            var result = GetResult<T>(returnText);
            return result;
        }

        /// <summary>
        /// PostGetJson
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="cookieContainer">The cookie container.</param>
        /// <param name="formData">The form data.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut">The time out.</param>
        /// <returns>T.</returns>
        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null, X509Certificate cer = null, int timeOut = 10000)
        {
            string returnText = RequestUtility.HttpPost(url, cookieContainer, formData, encoding, cer, timeOut);
            var result = GetResult<T>(returnText);
            return result;
        }

        public static T PostGetJson<T>(string url, string parameter)
        {
            var query = System.Web.HttpUtility.ParseQueryString(parameter);
            var dictionary = query.AllKeys
                 .ToDictionary(v => v, v => query[v]);
            string returnText = RequestUtility.HttpPost(url, formData: dictionary);
            var result = GetResult<T>(returnText);
            return result;
        }
        public static string PostGetString(string url, string parameter, Encoding encoding = null)
        {
            var query = System.Web.HttpUtility.ParseQueryString(parameter);
            var dictionary = query.AllKeys
                 .ToDictionary(v => v, v => query[v]);
            string returnText = RequestUtility.HttpPost(url, formData: dictionary, encoding: encoding);

            return returnText;
        }
        /// <summary>
        /// 使用Post方法上传数据并下载文件或结果
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <param name="stream">The stream.</param>
        public static void Download(string url, string data, Stream stream)
        {
            WebClient wc = new WebClient();
            var file = wc.UploadData(url, "POST", Encoding.UTF8.GetBytes(string.IsNullOrEmpty(data) ? "" : data));
            foreach (var b in file)
            {
                stream.WriteByte(b);
            }
        }

        #endregion

        #region 异步方法


        /// <summary>
        /// 【异步方法】发起Post请求，可上传文件
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="fileDictionary">The file dictionary.</param>
        /// <param name="postDataDictionary">The post data dictionary.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public static async Task<T> PostFileGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null, Dictionary<string, string> postDataDictionary = null, Encoding encoding = null, X509Certificate cer = null, int timeOut = 10000)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                postDataDictionary.FillFormDataStream(ms); //填充formData
                string returnText = await RequestUtility.HttpPostAsync(url, cookieContainer, ms, fileDictionary, null, encoding, cer, timeOut);
                var result = GetResult<T>(returnText);
                return result;
            }
        }


        /// <summary>
        /// 【异步方法】PostGetJson的异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="cookieContainer">The cookie container.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut">The time out.</param>
        /// <param name="checkValidationResult">if set to <c>true</c> [check validation result].</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public static async Task<T> PostGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Stream fileStream = null, Encoding encoding = null, X509Certificate cer = null, int timeOut = 10000, bool checkValidationResult = false)
        {
            string returnText = await RequestUtility.HttpPostAsync(url, cookieContainer, fileStream, null, null, encoding, cer, timeOut, checkValidationResult);

            var result = GetResult<T>(returnText);
            return result;
        }


        /// <summary>
        /// PostGetJson的异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="cookieContainer">The cookie container.</param>
        /// <param name="formData">The form data.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut">The time out.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public static async Task<T> PostGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null, X509Certificate cer = null, int timeOut = 10000)
        {
            string returnText = await RequestUtility.HttpPostAsync(url, cookieContainer, formData, encoding, cer, timeOut);
            var result = GetResult<T>(returnText);
            return result;
        }

        ///// <summary>
        ///// PostFileGetJson的异步版本
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <param name="cookieContainer"></param>
        ///// <param name="fileDictionary"></param>
        ///// <param name="encoding"></param>
        ///// <param name="cer">证书，如果不需要则保留null</param>
        ///// <param name="timeOut"></param>
        ///// <returns></returns>
        //public static async Task<T> PostFileGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null, Encoding encoding = null, X509Certificate cer = null, int timeOut = 10000)
        //{
        //    string returnText = await RequestUtility.HttpPostAsync(url, cookieContainer, null, fileDictionary, null, encoding, cer, timeOut);
        //    var result = GetResult<T>(returnText);
        //    return result;
        //}



        /// <summary>
        /// 使用Post方法上传数据并下载文件或结果
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>Task.</returns>
        public static async Task DownloadAsync(string url, string data, Stream stream)
        {
            WebClient wc = new WebClient();

            var fileBytes = await wc.UploadDataTaskAsync(url, "POST", Encoding.UTF8.GetBytes(string.IsNullOrEmpty(data) ? "" : data));
            await stream.WriteAsync(fileBytes, 0, fileBytes.Length);//也可以分段写入
        }

        #endregion
    }
}
