#define REMOTE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebTHCEventUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            THC_Library.DataBase.DataBaseControl.ResetDatabase("60.251.140.166", "THC_ACTIVITY");

#if(REMOTE)
          
                 THC_Library.APPCURL.APP_URL = "http://60.251.140.166/WebTHCApp/";
#else

            THC_Library.APPCURL.APP_URL = "http://localhost:51323/";
#endif

        }
    }
}
