using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ComponentAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; private set; }

        public bool PropertyAutoWired { get; set; } = true;

        public bool AsImplementedInterfaces { get; set; } = true;

        public bool AsSelf { get; set; } = true;

        public bool AutoActivate { get; set; } = false;

        public ComponentAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}
