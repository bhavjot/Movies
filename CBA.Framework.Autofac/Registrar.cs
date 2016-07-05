using System;
using Autofac;
using Autofac.Builder;
using System.Linq;
using System.Reflection;
using CBA.Framework.Autofac.Enums;
using CBA.Framework.Autofac.Extentions;
using CBA.Framework.Autofac.Attributes;
using CBA.Framework.Autofac.Interfaces;

namespace CBA.Framework.Autofac
{
    public class Registrar : IRegistrar
    {
        private readonly ContainerBuilder builder;

        public Registrar(ContainerBuilder builder)
        {
            this.builder = builder;
        }
        public void Register(Assembly assembly)
        {
            RegisterAutoWiredTypes(assembly);
        }

        protected void RegisterAutoWiredTypes(Assembly assembly)
        {
            var concreteTypes = assembly.GetTypesWithAttribute<AutoWiredAttribute>();

            foreach (var concreteType in concreteTypes)
            {
                var autoWiredAttribute = concreteType.GetAllAttributes<AutoWiredAttribute>().Single();
                var type = concreteType;
                foreach (var implementedInterface in concreteType.GetInterfaces().Where(implementedInterface => CanRegister(implementedInterface, type)))
                {
                    Register(concreteType, implementedInterface, autoWiredAttribute.LifeTimeScope);
                }
            }
        }
    
        protected virtual bool CanRegister(Type typeInterface, Type component)
        {
            return true;
        }

        public void Register<TImplementation, TInterface>(LifetimeScope lifetimeScope)
        {
            Register(typeof(TImplementation), typeof (TInterface), lifetimeScope);
        }

        public void Register<TImplementation>(Type typeInterface, LifetimeScope lifetimeScope)
        {
            Register(typeof(TImplementation), typeInterface, lifetimeScope);
        }

        protected void Register(Type implementation, Type contract, LifetimeScope lifetimeScope)
        {
            var registration = builder.RegisterType(implementation).As(contract);
            registration.PropertiesAutowired();
            ApplyLifetimeScope(registration, lifetimeScope);
        }

        private static void ApplyLifetimeScope<TContract>(IRegistrationBuilder<TContract, IConcreteActivatorData, SingleRegistrationStyle> registration, LifetimeScope lifetimeScope)
        {
            switch (lifetimeScope)
            {
                case LifetimeScope.InstancePerDependency:
                    registration.InstancePerDependency();
                    break;
                case LifetimeScope.InstancePerLifetimeScope:
                    registration.InstancePerLifetimeScope();
                    break;
                case LifetimeScope.SingleInstance:
                    registration.SingleInstance();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("lifetimeScope");
            }
        }
       
    }
}
