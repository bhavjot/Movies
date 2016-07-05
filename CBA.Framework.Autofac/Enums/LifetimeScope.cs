using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBA.Framework.Autofac.Enums
{
    public enum LifetimeScope
    {
        InstancePerDependency,
        InstancePerLifetimeScope,
        SingleInstance
    }
}
