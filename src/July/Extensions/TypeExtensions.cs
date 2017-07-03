using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace July.Extensions
{
    public static class TypeExtensions
    {
        public static TAttribute GetFirstAttribute<TAttribute>(this Type type, bool includeInterfaceAttributes = true)
            where TAttribute : Attribute
        {
            IEnumerable<TAttribute> classAttributes = type.GetTypeInfo().GetCustomAttributes<TAttribute>();

            IEnumerable<TAttribute> interfaceAttributes = Enumerable.Empty<TAttribute>();
            if (includeInterfaceAttributes)
            {
                interfaceAttributes = type.GetInterfaces().SelectMany(t => t.GetTypeInfo().GetCustomAttributes<TAttribute>());
            }

            return classAttributes.Union(interfaceAttributes).FirstOrDefault();
        }
    }
}
