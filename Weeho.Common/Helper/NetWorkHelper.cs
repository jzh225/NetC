using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Weeho.Common.Helper
{
    /// <summary>
    /// 网络访问帮助类
    /// edit by zhaoke 2017-07-05
    /// </summary>
    public class NetWorkHelper
    {
        /// <summary>
        /// POST request
        /// </summary>
        /// <param name="param">Parameters eg. "myMethod",myValue</param>
        /// <param name="methodName">method name or url</param>
        /// <returns></returns>
        public async static Task<KeyValuePair<bool, string>> VisitWebService(Dictionary<string, string> param, string webAddress, string methodName)
        {
            try
            {
                var result = await Task.Run<KeyValuePair<bool, string>>(() =>
                {
                    return SendHttpRequest(webAddress, methodName.Trim(), param);
                });
                return result;
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public static KeyValuePair<bool, string> VisitWebServiceSync(Dictionary<string, string> param, string webAddress, string methodName)
        {
            try
            {
                return SendHttpRequest(webAddress, methodName.Trim(), param);

            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        /// <summary>
        /// Send Post request useing http
        /// </summary>
        /// <param name="requestURI"></param>
        /// <param name="requestMethod"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> SendHttpRequest(string requestURI, string requestMethod, Dictionary<string, string> param)
        {
            try
            {
                string strURL = string.Format("{0}/{1}", requestURI, requestMethod);
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                //Post request
                request.Method = "POST";
                // type
                request.ContentType = "application/x-www-form-urlencoded";
                //parameters format by url coded
                string paraUrlCoded = "";
                foreach (var item in param)
                {
                    if (!item.Equals(param.FirstOrDefault()))
                    {
                        paraUrlCoded += "&";
                    }
                    paraUrlCoded += System.Web.HttpUtility.UrlEncode(item.Key);
                    paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode(item.Value);
                }

                byte[] payload;

                payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
                request.ContentLength = payload.Length;
                System.IO.Stream writer = request.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();
                System.Net.HttpWebResponse response;
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string responseText = myreader.ReadToEnd();
                myreader.Close();
                return new KeyValuePair<bool, string>(true, responseText);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }

        public static string Serializer(object obj)
        {
            if (obj != null)
            {
                string rst = string.Empty;
                var js = new JavaScriptSerializer();
                rst = js.Serialize(obj);
                return rst;
            }
            else
            {
                return "";
            }
        }

        public static T Deserializer<T>(string json)
        {
            var js = new JavaScriptSerializer();
            return js.Deserialize<T>(json);
        }

    }
}
