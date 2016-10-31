using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;
using SwipeJob.Core;
using System.Data.Entity;
using SwipeJob.Model.EF;

namespace SwipeJob.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer<AppDbContext>(null); // khi model thay doi, khong lam gì hết, 
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteTable.Routes.MapMvcAttributeRoutes();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            XmlConfigurator.Configure();
            EmailDelivery.Config();
        }
    }
}
