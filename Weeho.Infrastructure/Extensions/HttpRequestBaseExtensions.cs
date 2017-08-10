using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Weeho.Infrastructure.Extensions
{
    /// <summary>
    /// Class HttpRequestBaseExtensions.
    /// </summary>
    public static class HttpRequestBaseExtensions
    {
        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string GetQueryString(this HttpRequestBase request, string name, string defaultValue = "")
        {
            string value = request.QueryString[name];
            if (value.IsNullOrEmpty())
                value = (string)(request.RequestContext.RouteData.Values[name] ?? string.Empty);
            return (value.IsNullOrEmpty() ? defaultValue : value.Trim());
        }
        /// <summary>
        /// Gets the query int.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetQueryInt(this HttpRequestBase request, string name, int defaultValue = 0)
        {
            return request.GetQueryString(name, defaultValue.ToString()).ToInt(defaultValue);
        }
        public static decimal GetQueryDecimal(this HttpRequestBase request, string name, decimal defaultValue = 0)
        {
            return request.GetQueryString(name, defaultValue.ToString()).ToDecimal(defaultValue);
        }
        /// <summary>
        /// Gets the query long.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        public static long GetQueryLong(this HttpRequestBase request, string name, long defaultValue = 0)
        {
            return request.GetQueryString(name, defaultValue.ToString()).ToLong(defaultValue);
        }
        /// <summary>
        /// Gets the query date time.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <returns>DateTime.</returns>
        public static DateTime GetQueryDateTime(this HttpRequestBase request, string name)
        {
            return request.GetQueryString(name).ToDateTime();
        }
        /// <summary>
        /// Gets the form string.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string GetFormString(this HttpRequestBase request, string name, string defaultValue = "")
        {
            string value = request.Form[name];
            return ((value == null) ? defaultValue : value.Trim());
        }
        /// <summary>
        /// Gets the form int.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetFormInt(this HttpRequestBase request, string name, int defaultValue = 0)
        {
            return request.GetFormString(name, defaultValue.ToString()).ToInt();
        }
        public static decimal GetFormDecimal(this HttpRequestBase request, string name, decimal defaultValue = 0)
        {
            return request.GetFormString(name, defaultValue.ToString()).ToDecimal(defaultValue);
        }
        /// <summary>
        /// Gets the form long.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Int64.</returns>
        public static long GetFormLong(this HttpRequestBase request, string name)
        {
            return request.GetFormString(name).ToLong();
        }
        /// <summary>
        /// Gets the form date time.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <returns>DateTime.</returns>
        public static DateTime GetFormDateTime(this HttpRequestBase request, string name)
        {
            return request.GetFormString(name).ToDateTime();
        }
        /// <summary>
        /// Gets the query string values.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.String[].</returns>
        public static string[] GetQueryStringValues(this HttpRequestBase request, string name)
        {
            var values = request.GetQueryString(name);
            if (!values.IsNullOrEmpty())
            {
                List<string> result = new List<string>();
                var array = values.Split(',');
                foreach (var a in array)
                    result.Add(a);
                return result.ToArray();
            }
            else
            {
                return null;
            }
        }
        public static string[] GetFormValues(this HttpRequestBase request, string name)
        {
            var values = request.GetFormString(name);
            if (!values.IsNullOrEmpty())
            {
                List<string> result = new List<string>();
                var array = values.Split(',');
                foreach (var a in array)
                    result.Add(a);
                return result.ToArray();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Gets the query int values.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Int32[].</returns>
        public static int[] GetQueryIntValues(this HttpRequestBase request, string name)
        {
            var values = request.GetQueryString(name);
            List<int> result = new List<int>();
            var array = values.Split(',');
            foreach (var a in array)
                if (a.IsInt())
                    result.Add(a.ToInt());
            return result.ToArray();
        }
        /// <summary>
        /// Gets the query long values.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Int64[].</returns>
        public static long[] GetQueryLongValues(this HttpRequestBase request, string name)
        {
            var values = request.GetQueryString(name);
            List<long> result = new List<long>();
            var array = values.Split(',');
            foreach (var a in array)
                if (a.IsLong())
                    result.Add(a.ToLong());
            return result.ToArray();
        }
        /// <summary>
        /// Gets the form string values.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.String[].</returns>
        public static string[] GetFormStringValues(this HttpRequestBase request, string name)
        {
            var values = request.GetFormString(name);
            List<string> result = new List<string>();
            var array = values.Split(',');
            foreach (var a in array)
                result.Add(a);
            return result.ToArray();
        }
        /// <summary>
        /// Gets the form int values.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Int32[].</returns>
        public static int[] GetFormIntValues(this HttpRequestBase request, string name)
        {
            var values = request.GetFormString(name);
            List<int> result = new List<int>();
            var array = values.Split(',');
            foreach (var a in array)
                if (a.IsInt())
                    result.Add(a.ToInt());
            return result.ToArray();
        }
        /// <summary>
        /// Gets the form long values.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.Int64[].</returns>
        public static long[] GetFormLongValues(this HttpRequestBase request, string name)
        {
            var values = request.GetFormString(name);
            List<long> result = new List<long>();
            var array = values.Split(',');
            foreach (var a in array)
                if (a.IsLong())
                    result.Add(a.ToLong());
            return result.ToArray();
        }

        /// <summary>
        /// Servers the domain.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        public static string ServerDomain(this HttpRequestBase request)
        {
            string urlHost = request.Url.Host.ToLower();
            string[] urlHostArray = urlHost.Split(new char[] { '.' });
            if ((urlHostArray.Length < 3) || urlHost.IsIp())
            {
                return urlHost;
            }
            string urlHost2 = urlHost.Remove(0, urlHost.IndexOf(".") + 1);
            if ((urlHost2.StartsWith("com.") || urlHost2.StartsWith("net.")) || (urlHost2.StartsWith("org.") || urlHost2.StartsWith("gov.")))
            {
                return urlHost;
            }
            return urlHost2;
        }

        /// <summary>
        /// Gets the user ip.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        public static string GetUserIP(this HttpRequestBase request)
        {
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            switch (result)
            {
                case null:
                case "":
                    result = request.ServerVariables["REMOTE_ADDR"];
                    break;
            }
            if (!result.IsIp())
            {
                return "Unknown";
            }
            return result;
        }
        /// <summary>
        /// Determines whether [is we chat browser] [the specified request].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns><c>true</c> if [is we chat browser] [the specified request]; otherwise, <c>false</c>.</returns>
        public static bool IsWeChatBrowser(this HttpRequestBase request)
        {
            return request.UserAgent.ToLower().Contains("micromessenger");
        }
    }
}
