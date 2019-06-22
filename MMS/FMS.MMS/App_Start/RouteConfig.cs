using System.Web.Mvc;
using System.Web.Routing;

namespace MMS.FMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MMS.FMS.Controllers" }
            );
            routes.MapRoute(
               "FMS_default",
               "FMS/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}