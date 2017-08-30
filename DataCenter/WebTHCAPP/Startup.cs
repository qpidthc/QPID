using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebTHCAPP.Startup))]
namespace WebTHCAPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
