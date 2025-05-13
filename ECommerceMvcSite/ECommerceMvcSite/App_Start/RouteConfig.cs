using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ECommerceMvcSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Admin için özel route ekleyelim
            routes.MapRoute(
                name: "AdminDashboard", // Route adını belirtiyoruz
                url: "Admin/AdminPanel", // İstenilen URL
                defaults: new { controller = "Admin", action = "AdminDashboard" } // AdminController'daki AdminDashboard action'ına yönlendiriyoruz
            );

            // Default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

    }
}
