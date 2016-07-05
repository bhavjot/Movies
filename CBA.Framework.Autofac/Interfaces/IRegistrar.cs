using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CBA.Framework.Autofac.Enums;

namespace CBA.Framework.Autofac.Interfaces
{
    public interface IRegistrar
    {
        /// <summary>
        /// Register all concrete types to their interfaces which have attribute AutoWired on them
        /// </summary>
        /// <param name="assembly">Assembly for which we are registering all Auto Wired.</param>
        void Register(Assembly assembly);
        void Register<TImplementation, TInterface>(LifetimeScope lifetimeScope);
        void Register<TImplementation>(Type typeInterface, LifetimeScope lifetimeScope);
    }
}
