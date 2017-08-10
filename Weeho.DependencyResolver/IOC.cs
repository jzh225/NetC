using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Weeho.DependencyResolver
{
    /// <summary>
    /// Class IOC.
    /// </summary>
    public class IOC
    {
        /// <summary>
        /// The resolver
        /// </summary>
        private static IDependencyResolver _resolver;


        /// <summary>
        /// Initializes the with.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        public static void InitializeWith(IDependencyResolver resolver)
        {

            _resolver = resolver;
        }
        /// <summary>
        /// Gets the dependency resolver.
        /// </summary>
        /// <returns>IDependencyResolver.</returns>
        public static IDependencyResolver GetDependencyResolver()
        {
            return _resolver;
        }
        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        public static T Resolve<T>()
        {
            return _resolver.GetService<T>();
        }
    }
}
