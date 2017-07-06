using Autofac;
using July.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events.Internal
{
    internal class IocEventHandler<TEventData>: IEventHandler<TEventData>
        where TEventData : IEventData
    {
        private ILifetimeScope _lifetimeScope;

        public Type EventHandlerType { get; private set; }

        public IocEventHandler(ILifetimeScope lifetimeScope, Type eventHandlerType)
        {
            _lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(_lifetimeScope));
            EventHandlerType = eventHandlerType;
        }

        public void Handle(TEventData eventData)
        {
            IEventHandler<TEventData> handler = _lifetimeScope.Resolve(EventHandlerType) as IEventHandler<TEventData>;

            if (handler == null)
            {
                throw new InvalidOperationException("Cannot create event handler: " + EventHandlerType.FullName + ", make sure it implements IEventHandler<" + typeof(TEventData).Name + ">");
            }

            handler.Handle(eventData);
        }
    }
}
