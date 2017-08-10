using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Weeho.DependencyResolver
{
    /// <summary>
    /// Class UnityDependencyResolver.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.IDependencyResolver" />
    public class UnityDependencyResolver : IDependencyResolver
    {

        /// <summary>
        /// The container
        /// </summary>
        private IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyResolver" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// 解析支持任意对象创建的一次注册的服务。
        /// </summary>
        /// <param name="serviceType">所请求的服务或对象的类型。</param>
        /// <returns>请求的服务或对象。</returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                //额外操作，如日志
                return null;
            }
        }

        /// <summary>
        /// 解析多次注册的服务。
        /// </summary>
        /// <param name="serviceType">所请求的服务的类型。</param>
        /// <returns>请求的服务。</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                //额外操作，如日志
                return Enumerable.Empty<object>();
            }
        }
    }
}
