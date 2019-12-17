using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPNetApp.Startup))]
namespace ASPNetApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
