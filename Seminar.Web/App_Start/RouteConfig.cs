using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Seminar.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Nalozi_Index",
                url: "Nalozi",
                defaults: new { controller = "TravelWarrant", action = "Index" }
            );
            routes.MapRoute(
                name: "Nalozi_Create",
                url: "Nalozi/Dodaj",
                defaults: new { controller = "TravelWarrant", action = "Create" }
            );
            routes.MapRoute(
                name: "Nalozi_Details",
                url: "Nalozi/Detalji/{id}",
                defaults: new {controller = "TravelWarrant", action = "Details"}
            );
            routes.MapRoute(
                name: "Nalozi_Edit",
                url: "Nalozi/Uredi/{id}",
                defaults: new { controller = "TravelWarrant", action = "Edit" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
