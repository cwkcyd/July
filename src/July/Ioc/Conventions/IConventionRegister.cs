using Autofac.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc.Conventions
{
    public interface IConventionRegister<TLimit, TActivatorData, TRegistrationStyle>
    {
        IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> Register(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type);
    }
}
