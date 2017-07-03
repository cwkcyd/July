using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Builder;
using Autofac;
using System.Reflection;

namespace July.Ioc.Conventions
{
    public class LifetimeEventsRegister : IConventionRegister
    {
        public IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> Register(IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration, Type type)
        {
            if (typeof(ILifetimeEvents).IsAssignableFrom(type))
            {
                registration.OnActivating(e => { ((ILifetimeEvents)e.Instance).OnActivating(); });
                registration.OnActivated(e => { ((ILifetimeEvents)e.Instance).OnActivated(); });
                registration.OnRelease(e =>
                {
                    ((ILifetimeEvents)e).OnRelease();
                    if (e is IDisposable)
                    {
                        ((IDisposable)e).Dispose();
                    }
                });
            }

            return registration;
        }
    }
}
