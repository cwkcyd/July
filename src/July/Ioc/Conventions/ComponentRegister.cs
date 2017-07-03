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
        public IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> Register(IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration, Type type)
        {
            ComponentAttribute attribute = type.GetFirstAttribute<ComponentAttribute>(true);

            if (attribute.AsSelf)
            {
                registration = registration.AsSelf();
            }
            if (attribute.AsImplementedInterfaces)
            {
                registration = registration.AsImplementedInterfaces();
            }
            if (attribute.PropertyAutoWired)
            {
                registration = registration.PropertiesAutowired();
            }
            if (attribute.AutoActivate)
            {
                registration = registration.AutoActivate();
            }

            if (attribute.Lifetime == ServiceLifetime.Scoped)
            {
                registration = registration.InstancePerLifetimeScope();
            }
            else if (attribute.Lifetime == ServiceLifetime.Singleton)
            {
                registration = registration.SingleInstance();
            }
            else
            {
                registration = registration.InstancePerDependency();
            }

            return registration;
        }
    }
}
