using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Caching.Redis
{
    /// <summary>
    /// Provides a per tenant <see cref="ICacheService" /> implementation.
    /// Default timeout is 20 minutes.
    /// </summary>
    /// <seealso cref="Caching.ICachingService" />
    public class RedisCachingService : IRedisCachingService
    {
        /// <summary>
        /// The prefix
        /// </summary>
        private readonly string _prefix;
        /// <summary>
        /// The PRCM
        /// </summary>
        private PooledRedisClientManager prcm;
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCachingService" /> class.
        /// </summary>
        public RedisCachingService()
        {
            InitClint();
        }
        /// <summary>
        /// Initializes the clint.
        /// </summary>
        private void InitClint()
        {
            RedisConfigInfo redisConfigInfo = RedisConfigInfo.GetConfig();
            string[] writeServerList = redisConfigInfo.WriteServerList.Split(',');
            string[] readServerList = redisConfigInfo.ReadServerList.Split(',');

            prcm = new PooledRedisClientManager(readServerList, writeServerList,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = redisConfigInfo.MaxWritePoolSize,
                                 MaxReadPoolSize = redisConfigInfo.MaxReadPoolSize,
                                 AutoStart = redisConfigInfo.AutoStart,
                             });
        }
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public T Get<T>(string key)
        {
            using (var redisClient = prcm.GetClient())
            {
                return redisClient.Get<T>(key);
            }
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="factory">The factory.</param>
        /// <returns>T.</returns>
        public T Get<T>(string key, Func<T> factory)
        {
            using (var redisClient = prcm.GetClient())
            {
                var result = redisClient.Get<T>(key);
                if (result == null)
                {
                    var computed = factory();
                    if (computed == null)
                        return default(T);
                    Put(key, computed);
                    return computed;
                }
                return result;
            }
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Put<T>(string key, T value)
        {
            using (var redisClient = prcm.GetClient())
            {
                redisClient.Add<T>(key, value);
            }
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
            using (var redisClient = prcm.GetClient())
            {
                redisClient.Add<T>(key, value, validFor);
            }

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
            using (var redisClient = prcm.GetClient())
            {
                redisClient.Add<T>(key, value, absoluteExpiration);
            }
        }
        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            using (var redisClient = prcm.GetClient())
            {
                redisClient.Remove(key);
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            using (var redisClient = prcm.GetClient())
            {
                redisClient.RemoveAll(redisClient.GetAllKeys());
            }
        }


    }
}
