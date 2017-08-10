using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Caching.Runtime
{
    /// <summary>
    /// Class RuntimeCacheUtility.
    /// </summary>
    public class RuntimeCacheUtility
    {
        /// <summary>
        /// The cache service
        /// </summary>
        private static ICachingService cacheService = new RuntimeCachingService();
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public static T Get<T>(string key)
        {
            return cacheService.Get<T>(key);
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Put(string key, object value)
        {
            cacheService.Put(
               key,
               value);
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="validFor">The valid for.</param>
        public static void Put(string key, object value, TimeSpan validFor)
        {
            cacheService.Put(key, value, validFor);
        }
        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="absoluteExpiration">The absolute expiration.</param>
        public static void Put(string key, object value, DateTime absoluteExpiration)
        {
            cacheService.Put(key, value, absoluteExpiration);
        }
        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void Remove(string key)
        {
            cacheService.Remove(key);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public static void Clear()
        {
            cacheService.Clear();
        }
    }
}
