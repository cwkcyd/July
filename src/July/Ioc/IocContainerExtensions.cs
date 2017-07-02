using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    public static class IocContainerExtensions
    {
        public static T Resolve<T>(this IIocContainer iocContainer)
        {
            object obj = iocContainer.Resolve(typeof(T));

            return (T)obj;
        }

        public static bool IsRegistered<T>(this IIocContainer iocContainer)
        {
            return iocContainer.IsRegistered(typeof(T));
        }
    }
}
