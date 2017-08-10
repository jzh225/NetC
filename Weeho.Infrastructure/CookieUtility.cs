// ***********************************************************************
// Assembly         : Infrastructure
// Author           : 
// Created          : 11-12-2016
//
// Last Modified By : 
// Last Modified On : 11-14-2016
// ***********************************************************************
// <copyright file="CookieUtility.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Weeho.Infrastructure.Extensions;

namespace Weeho.Infrastructure
{
    /// <summary>
    /// Class CookieUtility.
    /// </summary>
    public class CookieUtility
    {
        /// <summary>
        /// 取Cookie
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>HttpCookie.</returns>
        public static HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        /// <summary>
        /// 取Cookie值
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        public static string GetValue(string name)
        {
            var httpCookie = Get(name);
            if (httpCookie != null)
                return httpCookie.Value;
            else
                return string.Empty;
        }

        /// <summary>
        /// 移除Cookie
        /// </summary>
        /// <param name="name">The name.</param>
        public static void Remove(string name)
        {
            CookieUtility.Remove(CookieUtility.Get(name));
        }

        /// <summary>
        /// Removes the specified cookie.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        public static void Remove(HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now;
                CookieUtility.Save(cookie);
            }
        }

        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiresHours">The expires hours.</param>
        public static void Save(string name, string value, int expiresHours = 0)
        {
            var httpCookie = Get(name);
            if (httpCookie == null)
                httpCookie = Set(name);

            httpCookie.Value = value;
            CookieUtility.Save(httpCookie, expiresHours);
        }


        /// <summary>
        /// Saves the specified cookie.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        /// <param name="expiresHours">The expires hours.</param>
        public static void Save(HttpCookie cookie, int expiresHours = 0)
        {
            string domain = HttpContext.Current.Request.GetServerDomain();
            string urlHost = HttpContext.Current.Request.Url.Host.ToLower();
            if (domain != urlHost)
                cookie.Domain = domain;

            if (expiresHours > 0)
                cookie.Expires = DateTime.Now.AddHours(expiresHours);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Sets the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>HttpCookie.</returns>
        public static HttpCookie Set(string name)
        {
            return new HttpCookie(name);
        }
    }
}
