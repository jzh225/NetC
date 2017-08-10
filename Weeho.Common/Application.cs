using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Weeho.Biz;
using Weeho.Biz.Interface;
using Weeho.Caching.Redis;
using Weeho.Dac.Interface;
using Weeho.Dac.Repository;
using Weeho.DependencyResolver;
using Weeho.Model;

namespace Weeho.Common
{
    /// <summary>
    /// Class WebApplication.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Configurations this instance.
        /// </summary>
        /// <returns>IDependencyResolver.</returns>
        public static IDependencyResolver Config()
        {
            return ConfigService(typeof(PerHttpRequestLifetimeManager));
        }
        public static IDependencyResolver ConfigService(Type managerType)
        {
            var builder = new UnityContainer();
            builder.RegisterType<IContext, Weeho.Model.Entity.Entities>((LifetimeManager)Activator.CreateInstance(managerType))
                    .RegisterType<IRedisCachingService, RedisCachingService>((LifetimeManager)Activator.CreateInstance(managerType))

//SysAdmin
.RegisterType<ISysAdminRepository, SysAdminRepository>((LifetimeManager)Activator.CreateInstance(managerType))
.RegisterType<ISysAdminService, SysAdminService>((LifetimeManager)Activator.CreateInstance(managerType))
//SysAction
.RegisterType<ISysActionRepository, SysActionRepository>((LifetimeManager)Activator.CreateInstance(managerType))
.RegisterType<ISysActionService, SysActionService>((LifetimeManager)Activator.CreateInstance(managerType))
//SysAdminRole
.RegisterType<ISysAdminRoleRepository, SysAdminRoleRepository>((LifetimeManager)Activator.CreateInstance(managerType))
.RegisterType<ISysAdminRoleService, SysAdminRoleService>((LifetimeManager)Activator.CreateInstance(managerType))
//SysMenu
.RegisterType<ISysMenuRepository, SysMenuRepository>((LifetimeManager)Activator.CreateInstance(managerType))
.RegisterType<ISysMenuService, SysMenuService>((LifetimeManager)Activator.CreateInstance(managerType))
//SysRole
.RegisterType<ISysRoleRepository, SysRoleRepository>((LifetimeManager)Activator.CreateInstance(managerType))
.RegisterType<ISysRoleService, SysRoleService>((LifetimeManager)Activator.CreateInstance(managerType))
//SysRoleAction
.RegisterType<ISysRoleActionRepository, SysRoleActionRepository>((LifetimeManager)Activator.CreateInstance(managerType))
.RegisterType<ISysRoleActionService, SysRoleActionService>((LifetimeManager)Activator.CreateInstance(managerType))
//SysRoleMenu
.RegisterType<ISysRoleMenuRepository, SysRoleMenuRepository>((LifetimeManager)Activator.CreateInstance(managerType))
.RegisterType<ISysRoleMenuService, SysRoleMenuService>((LifetimeManager)Activator.CreateInstance(managerType))
//SysLog
.RegisterType<ISysLogRepository, SysLogRepository>((LifetimeManager)Activator.CreateInstance(managerType))
.RegisterType<ISysLogService, SysLogService>((LifetimeManager)Activator.CreateInstance(managerType))
//Users
.RegisterType<IUsersRepository, UsersRepository>((LifetimeManager)Activator.CreateInstance(managerType))
.RegisterType<IUsersService, UsersService>((LifetimeManager)Activator.CreateInstance(managerType));

            IOC.InitializeWith(new UnityDependencyResolver(builder));
            return IOC.GetDependencyResolver();
        }
    }
}
