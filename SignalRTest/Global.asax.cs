using Signal.Common;
using SignalRTest.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SignalRTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/log4net.config")));
            LogHelper.Default.WriteInfo("AppStart");

            LogHelper.Default.WriteWarning("Warning");
            LogHelper.Default.WriteError("Error");
            LogHelper.Default.WriteFatal("Fatal");

            try
            {
                int a = 3 / 4;
                int r = 4 / a;
            }
            catch (Exception ex)
            {
                LogHelper.Default.WriteError(ex.Message, ex);
            }
        }
    }
}
