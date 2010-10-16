using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Infrastructure;

namespace SportsStore.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               null, 
               "", // empty
               new { controller = "Products", action = "List", category = (string)null, page = 1 } // Parameter defaults
           );

            routes.MapRoute(
               null, 
               "Page{page}", // URL with parameters
               new { controller = "Products", action = "List", category = (string)null },
               new { page = @"\d+" }// numerical
           );
            
            routes.MapRoute(
                null ,
                "{category}", // category
                new { controller = "Products", action = "List", page = 1 } // Parameter defaults
            );

            routes.MapRoute(
               null, // Route name
               "{category}/Page{page}", // ~/category/page123
               new { controller = "Products", action = "List" }, // Parameter defaults
               new { page = @"\d+" }// numerical
           );

            routes.MapRoute(null, "{controller}/{action}");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}