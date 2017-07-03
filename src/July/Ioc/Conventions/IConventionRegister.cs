using Autofac.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc.Conventions
{
    public interface IConventionRegister
    {
        IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> Register(IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration, Type type);
    }
}
