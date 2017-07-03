using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Builder;

namespace July.Ioc.Conventions
{
    public class ConventionRegister : List<IConventionRegister>, IConventionRegister
    {
        public ConventionRegister()
        {
            Add(new ComponentRegister());
            Add(new LifetimeEventsRegister());
            Add(new AspectRegister());
        }

        public IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> Register(IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration, Type type)
        {
            foreach (var register in this)
            {
                registration = register.Register(registration, type);
            }

            return registration;
        }
    }
}
