using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class ComponentAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; private set; }

        public bool PropertyAutoWired { get; set; } = true;

        public bool AsImplementedInterfaces { get; set; } = true;

        public bool AsSelf { get; set; } = true;

        public bool AutoActivate { get; set; } = false;

        public bool EnableClassInterceptors { get; set; } = false;

        public bool EnableInterfaceInterceptors { get; set; } = false;

        public Type[] InterceptorBy { get; set; }

        public ComponentAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}
