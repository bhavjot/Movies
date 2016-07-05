using Autofac;
using Autofac.Integration.WebApi;

namespace CBA.Movies.Web
{
    public class WebModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            
        }
    }
}