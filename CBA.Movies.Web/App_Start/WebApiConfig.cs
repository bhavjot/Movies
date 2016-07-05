using System.Web.Http;

namespace CBA.Movies.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

           
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{api}/{controller}/{id}",
                defaults: new {api = "api", controller = "Movies", id = RouteParameter.Optional }
            );
           
        }
    }
}
