using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Builder;

namespace July.Ioc.Conventions
{
    public class ConventionRegister<TLimit, TActivatorData, TRegistrationStyle> : List<IConventionRegister<TLimit, TActivatorData, TRegistrationStyle>>, IConventionRegister<TLimit, TActivatorData, TRegistrationStyle>
    {
        public ConventionRegister()
        {
            Add(new ComponentRegister<TLimit, TActivatorData, TRegistrationStyle>());
            Add(new LifetimeEventsRegister<TLimit, TActivatorData, TRegistrationStyle>());
            Add(new AspectRegister<TLimit, TActivatorData, TRegistrationStyle>());
        }

        public IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> Register(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type)
        {
            foreach (var register in this)
            {
                registration = register.Register(registration, type);
            }

            return registration;
        }
    }
}
