using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace CBA.Framework.Autofac.Extentions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>(this Assembly assembly) where TAttribute : Attribute
        {
            return GetTypesReliably(assembly).Where(type => !type.IsAbstract && !type.IsInterface && type.HasAttribute<TAttribute>());
        }

        public static IEnumerable<Type> GetTypesReliably(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null);
            }
        }
        public static IEnumerable<Type> GetTypesReliably<TType>(this Assembly assembly)
        {
            return assembly.GetTypesReliably().Where(type => type == typeof(TType) || type.IsSubclassOf(typeof(TType)));
        }
    }
}
