using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Phongkham
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DoctorLogin",
                url: "LoginBS/{action}/{id}",
                defaults: new { controller = "LoginBS", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PatientLogin",
                url: "LoginBN/{action}/{id}",
                defaults: new { controller = "LoginBN", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
