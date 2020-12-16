using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPIFilters.Controllers;

namespace WebAPIFilters
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // config.Filters.Add(new MyActionFilter("Filter at global level"));

            config.Filters.Add(new MyExceptionFilter2());

            //config.Filters.Add(new CustomExceptionsFilter.CustomExceptionFilter());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
