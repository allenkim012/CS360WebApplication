using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace issuemoa
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Issues",
                url: "issues/{action}/{itemId}",
                defaults: new { controller = "Issues", action = "Index", itemId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{itemId}",
                defaults: new { controller = "Home", action = "Index", itemId = UrlParameter.Optional }
            );
        }
    }
}
