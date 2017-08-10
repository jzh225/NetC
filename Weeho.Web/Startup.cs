using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Test.Startup))]
namespace Weeho.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
