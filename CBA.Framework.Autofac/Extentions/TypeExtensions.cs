using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CBA.Framework.Autofac.Attributes;


namespace CBA.Framework.Autofac.Extentions
{
    public static class TypeExtensions
    {
        public static bool IsConcerete(this Type type)
        {
            return !type.IsInterface && !type.IsAbstract;
        }

        public static bool HasAttribute<T>(this ICustomAttributeProvider customAttributeProvider, bool inherit = false) where T : Attribute
        {
            return customAttributeProvider.IsDefined(typeof(T), inherit);
        }

        public static IEnumerable<TAttribute> GetAllAttributes<TAttribute>(this ICustomAttributeProvider customAttributeProvider, bool inherit = false)
        {
            return customAttributeProvider.GetCustomAttributes(inherit).Where(a => a is TAttribute).Cast<TAttribute>();
        }
    }
}
