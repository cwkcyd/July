using Autofac.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc.Conventions
{
    public interface IConventionRegister
    {
        IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> Register<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type);
    }
}
