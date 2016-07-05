using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Module = Autofac.Module;
using CBA.Framework.Autofac.Interfaces;

namespace CBA.Framework.Autofac.Modules
{
    public class RegisterAssembliesModule:Module
    {
        private IRegistrar registrar;
        private readonly IEnumerable<Assembly> assemblies;
        public RegisterAssembliesModule(IEnumerable<Assembly> assemblies )
        {
            this.assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            registrar = new Registrar(builder);
            
            foreach (var assembly in assemblies)
            {
                registrar.Register(assembly);
            }
        }
    }
}
