using System.Web.Mvc;

namespace Seminar.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Automobili_Index",
                url: "Admin/Automobili",
                defaults: new { controller = "Car", action = "Index" }
            );
            context.MapRoute(
                name: "Automobili_Detalji",
                url: "Admin/Automobili/Detalji/{id}",
                defaults: new { controller = "Car", action = "Details" }
            );
            context.MapRoute(
                name: "Automobili_Dodaj",
                url: "Admin/Automobili/Dodaj",
                defaults: new { controller = "Car", action = "Create" }
            );
            context.MapRoute(
                name: "Automobili_Uredi",
                url: "Admin/Automobili/Uredi/{id}",
                defaults: new { controller = "Car", action = "Edit" }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}