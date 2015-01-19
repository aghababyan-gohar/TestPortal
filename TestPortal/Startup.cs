using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestPortal.Startup))]
namespace TestPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
