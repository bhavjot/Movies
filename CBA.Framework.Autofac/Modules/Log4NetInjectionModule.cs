using System;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Core;
using log4net;
using log4net.Config;
using log4net.Core;

namespace CBA.Framework.Autofac.Modules
{
    public class Log4NetInjectionModule : Module
    {
        private static readonly ILog Logger = LogManager.GetLogger("");
        protected override void Load(ContainerBuilder builder)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            XmlConfigurator.ConfigureAndWatch(new FileInfo(path));
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            if (HasPropertyDependencyOnILogger(registration.Activator.LimitType))
                registration.Activated += InjectLog4NetInstance;
        }

        private static bool HasPropertyDependencyOnILogger(Type type)
        {
            return type.GetProperties().Any(x => x.CanWrite && x.PropertyType == typeof(ILog));
        }

        private static void InjectLog4NetInstance(object sender, ActivatedEventArgs<object> e)
        {
            var itemType = e.Instance.GetType();
            var propertyInfo = itemType.GetProperties().First(x => x.CanWrite && x.PropertyType == typeof(ILog));
            propertyInfo.SetValue(e.Instance, Logger, null);
        }
    }
}