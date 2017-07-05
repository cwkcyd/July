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

        private string _matchingLifetimeScope;

        public string MatchingLifetimeScope
        {
            get
            {
                if (_matchingLifetimeScope == null)
                {
                    return Consts.LifetimeScope.ROOT;
                }
                return _matchingLifetimeScope;
            }
            set
            {
                _matchingLifetimeScope = value;
            }
        }

        public ComponentAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}
