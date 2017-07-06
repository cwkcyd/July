using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Builder;
using July.Events;
using July.Settings;

namespace July.Ioc.Conventions
{
    public class EventHandlerRegister : IConventionRegister
    {
        public void Register<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type type)
        {
            if (!typeof(IEventHandler).IsAssignableFrom(type))
            {
                return;
            }

            var interfaces = type.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (!typeof(IEventHandler).IsAssignableFrom(@interface))
                {
                    continue;
                }

                var genericArguments = @interface.GetGenericArguments();
                if (genericArguments.Length != 1)
                {
                    continue;
                }

                var genericArgument = genericArguments[0];

                if (!typeof(IEventHandler<>).MakeGenericType(genericArgument).IsAssignableFrom(@interface))
                {
                    continue;
                }

                GlobalSettings.Instance.EventBus().AddInitialHandler(genericArgument, type);
            }
        }
    }
}
