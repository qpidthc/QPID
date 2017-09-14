using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebHTCBackEnd.Startup))]
namespace WebHTCBackEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
