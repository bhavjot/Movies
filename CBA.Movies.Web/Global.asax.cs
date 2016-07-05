using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CBA.Framework.Autofac;

namespace CBA.Movies.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            var configurator = new Configurator();
            configurator.Configure();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(configurator.Container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(configurator.Container);           
        
        }
       
    }
}
