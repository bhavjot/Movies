using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CBA.MoviesService.Web;
using CBA.MoviesSource.Data;
using CBA.MoviesSource;

namespace CBA.MoviesService.Web.Infrastructure
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            builder.RegisterType<MovieCache>().As<IMovieCache>().SingleInstance();
            builder.RegisterType<MovieDataSourceRepository>().As<IMovieDataSourceRepository>().SingleInstance();
            builder.RegisterType<MovieDataSourceAdapter>().As<IMovieDataSource>().SingleInstance();
            builder.RegisterType<MovieDataSourceWrapper>().As<IMovieDataSource>().SingleInstance();

            //builder.Register(
            //    ctx => ctx.Resolve<IMovieDataSourceRepository>().Create(new TimeSpan(0, 0, 0), () => DateTimeOffset.UtcNow))
            //    .As<IMovieDataSource>().SingleInstance();
            builder.Register(
               ctx => ctx.Resolve<IMovieDataSourceRepository>().Create(new MovieDataSourceAdapter())
               .As<IMovieDataSourceRepository>().SingleInstance();

            
            builder.RegisterType<MovieDataService>().As<IMovieDataService>().SingleInstance();
        }
    }
}