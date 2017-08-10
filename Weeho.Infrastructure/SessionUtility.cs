// ***********************************************************************
// Assembly         : Infrastructure
// Author           : 
// Created          : 11-12-2016
//
// Last Modified By : 
// Last Modified On : 11-14-2016
// ***********************************************************************
// <copyright file="SessionUtility.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace Weeho.Infrastructure
{
    /// <summary>
    /// Class SessionUtility.
    /// </summary>
    public class SessionUtility
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        public static object Get(string key)
        {
            return HttpContext.Current.Session[key];
        }
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public static T Get<T>(string key)
        {
            return (T)Get(key);
        }
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="factory">The factory.</param>
        /// <returns>T.</returns>
        public static T Get<T>(string key, Func<T> factory)
        {
            var result = Get(key);
            if (result == null)
            {
                var computed = factory();
                if (computed == null)
                    return default(T);
                HttpContext.Current.Session[key] = computed;
                return computed;
            }
            return (T)result;
        }
        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Set(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

    }
}
