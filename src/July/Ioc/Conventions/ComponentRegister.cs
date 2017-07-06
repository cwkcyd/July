using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Builder;
using System.Reflection;
using System.Linq;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using July.Extensions;

namespace July.Ioc.Conventions
{
    public class ComponentRegister : IConventionRegister
    {
        public void Register<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type)
        {
            ComponentAttribute attribute = type.GetFirstAttribute<ComponentAttribute>(true);

            if (attribute == null)
            {
                return;
            }

            if (attribute.AsSelf)
            {
                registration.As(type);
            }
            if (attribute.AsImplementedInterfaces)
            {
                var interfaceTypes = type.GetInterfaces();
                registration.As(interfaceTypes);
            }
            if (attribute.PropertyAutoWired)
            {
                registration.PropertiesAutowired();
            }
            if (attribute.AutoActivate)
            {
                registration.AutoActivate();
            }

            if (attribute.Lifetime == ServiceLifetime.Scoped)
            {
                registration.InstancePerMatchingLifetimeScope(attribute.MatchingLifetimeScope);
            }
            else if (attribute.Lifetime == ServiceLifetime.Singleton)
            {
                registration.SingleInstance();
            }
            else
            {
                registration.InstancePerDependency();
            }
        }
    }
}
