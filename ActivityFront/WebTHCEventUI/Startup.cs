using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebTHCEventUI.Startup))]
namespace WebTHCEventUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
