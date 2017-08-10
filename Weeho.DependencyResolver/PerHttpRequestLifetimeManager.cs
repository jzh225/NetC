using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Weeho.DependencyResolver
{
    /// <summary>
    /// Class PerHttpRequestLifetimeManager.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Unity.LifetimeManager" />
    public class PerHttpRequestLifetimeManager : LifetimeManager
    {
        /// <summary>
        /// The key
        /// </summary>
        private readonly Guid _key = Guid.NewGuid();

        /// <summary>
        /// Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>the object desired, or null if no such object is currently stored.</returns>
        public override object GetValue()
        {
            return HttpContext.Current.Items[_key];
        }

        /// <summary>
        /// Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[_key] = newValue;
        }

        /// <summary>
        /// Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
            var obj = GetValue();
            HttpContext.Current.Items.Remove(obj);
        }
    }
}
