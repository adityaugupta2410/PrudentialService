using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using WeatherData.BusinessLayer;
using WeatherDataAPI.CustomFilters;

namespace WeatherDataAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new LoggingFilterAttribute());

            var container = new UnityContainer();
            container.RegisterType<IWeatherRepository, WeatherRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new DI_Implementation.UnityResolver(container);
        }
    }
}
