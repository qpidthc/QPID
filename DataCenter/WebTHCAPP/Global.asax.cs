﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebTHCAPP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            THC_Library.DataBase.DataBaseControl.ResetDatabase("60.251.140.166", "THC_CENTER");

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Do whatever you want to do with the error

            //Show the custom error page...
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";

            if ((Context.Server.GetLastError() is HttpException) && ((Context.Server.GetLastError() as HttpException).GetHttpCode() != 404))
            {
                routeData.Values["action"] = "Index";
            }
            else
            {
                //Handle 404 error and response code
                Response.StatusCode = 404;
                routeData.Values["action"] = "NotFound404";
            }
            Response.TrySkipIisCustomErrors = true;//If you are using IIS7, have this line
            IController errorsController = new Controllers.ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            var rc = new System.Web.Routing.RequestContext(wrapper, routeData);
            errorsController.Execute(rc);
        }
    }
}
