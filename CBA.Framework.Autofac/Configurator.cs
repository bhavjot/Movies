using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using CBA.Framework.Autofac.Extentions;
using CBA.Framework.Autofac.Interfaces;
using CBA.Framework.Autofac.Modules;
using Module = Autofac.Module;

namespace CBA.Framework.Autofac
{
    public class Configurator : IConfigurator
    {
        private IContainer container;
        private readonly string binFolder;
        private readonly IFileSystem fileSystem;

        public IContainer Container
        {
            get { return container; }
        }

        public Configurator()
            : this(
                HttpContext.Current != null ?
                HttpContext.Current.Server.MapPath(@"~\bin") :
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"))
        {

        }

        public Configurator(string binFolder)
        {
            this.fileSystem = new FileSystem();
            this.binFolder = binFolder;

        }
        public void Configure()
        {
            var builder = new ContainerBuilder();
            var assemblies = LoadAssemblies(FindAssemblies());
            builder.RegisterModule(new Log4NetInjectionModule());
            builder.RegisterModule(new RegisterAssembliesModule(assemblies));
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypesReliably<Module>();
                foreach(var type in types)
                {
                    var instance =  (Module)Activator.CreateInstance(type);
                    builder.RegisterModule(instance);
                }
                
            }

            container = builder.Build();

        }

        private IEnumerable<Assembly> LoadAssemblies(IEnumerable<string> assembliesNames)
        {
            //don't catch any exception let it bubble up.
            return assembliesNames.Select(Assembly.LoadFrom);
        }
        /// <summary>
        /// For now All search expresion is hardcoded(with CBA*). Can be configurable.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> FindAssemblies()
        {
            return fileSystem.GetFiles(binFolder, "CBA.Movies*.dll", SearchOption.AllDirectories)
                             .Select(file => file.ToLowerInvariant()).ToArray();
        }
    }
}
