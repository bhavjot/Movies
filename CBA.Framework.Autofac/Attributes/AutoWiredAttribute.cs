using System;
using CBA.Framework.Autofac.Enums;

namespace CBA.Framework.Autofac.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false,  Inherited = false)]
    public sealed class AutoWiredAttribute : Attribute
    {
        public LifetimeScope LifeTimeScope { get; set; }

        public AutoWiredAttribute() : this(LifetimeScope.InstancePerDependency) { }

        public AutoWiredAttribute(LifetimeScope lifeTimeScope)
        {
            LifeTimeScope = lifeTimeScope;
           
        }
    }
}
