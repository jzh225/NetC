using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Weeho.Caching.Runtime
{
    /// <summary>
    /// Provides a per tenant <see cref="ICacheService" /> implementation.
    /// Default timeout is 20 minutes.
    /// </summary>
    /// <seealso cref="Caching.ICachingService" />
    public class RuntimeCachingService : ICachingService
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public T Get<T>(string key)
        {
            return (T)HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Put<T>(string key, T value)
        {
            HttpRuntime.Cache.Insert(
               key,
               value,
               null,
               System.Web.Caching.Cache.NoAbsoluteExpiration,
               System.Web.Caching.Cache.NoSlidingExpiration,
               System.Web.Caching.CacheItemPriority.Normal,
               null);
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="validFor">The valid for.</param>
        public void Put<T>(string key, T value, TimeSpan validFor)
        {
            HttpRuntime.Cache.Insert(
                key,
                value,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                validFor,
                System.Web.Caching.CacheItemPriority.Normal,
                null);
        }
        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="absoluteExpiration">The absolute expiration.</param>
        public void Put<T>(string key, T value, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(
                key,
                value,
                null,
                absoluteExpiration,
                System.Web.Caching.Cache.NoSlidingExpiration,
                System.Web.Caching.CacheItemPriority.Normal,
                null);
        }
        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            var all = HttpRuntime.Cache
                .AsParallel()
                .Cast<DictionaryEntry>()
                .Select(x => x.Key.ToString())
                .ToList();

            foreach (var key in all)
            {
                Remove(key);
            }
        }


    }
}
