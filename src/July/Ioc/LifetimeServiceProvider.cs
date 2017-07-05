using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    public class LifetimeServiceProvider : IServiceProvider, ISupportRequiredService
    {
        private ILifetimeScope LifetimeScope { get; set; }

        public LifetimeServiceProvider(ILifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
        }

        public object GetService(Type serviceType)
        {
            return LifetimeScope.ResolveOptional(serviceType);
        }

        public object GetRequiredService(Type serviceType)
        {
            return LifetimeScope.Resolve(serviceType);
        }
    }
}
