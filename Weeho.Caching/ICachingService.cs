using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Caching
{
    public interface ICachingService
    {

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        T Get<T>(string key);
        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Put<T>(string key, T value);
        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="validFor">The valid for.</param>
        void Put<T>(string key, T value, TimeSpan validFor);
        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="absoluteExpiration">The absolute expiration.</param>
        void Put<T>(string key, T value, DateTime absoluteExpiration);
        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);
        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();




    }
}
