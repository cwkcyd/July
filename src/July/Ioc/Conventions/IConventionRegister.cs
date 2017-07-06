using Autofac.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc.Conventions
{
    public interface IConventionRegister
    {
        void Register<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type);
    }
}
