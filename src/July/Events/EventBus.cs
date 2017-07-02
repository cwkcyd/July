using Autofac;
using July.Ioc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public class EventBus : IEventBus
    {
        private IIocContainer IocContainer { get; set; }

        private ConcurrentDictionary<Type, List<Type>> HandlerMapping { get; set; }

        public EventBus(IIocContainer iocContainer)
        {
            IocContainer = iocContainer;
            HandlerMapping = new ConcurrentDictionary<Type, List<Type>>();
        }

        public void Publish<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            if (HandlerMapping.TryGetValue(typeof(TEventData), out var handlers))
            {
                using (ILifetimeScope scope = IocContainer.BeginLifetimeScope())
                {
                    foreach (var handlerType in handlers)
                    {
                        IEventHandler<TEventData> handler = (IEventHandler<TEventData>)scope.ResolveOptional(handlerType);

                        handler.Handle(eventData);
                    }
                }
            }
        }

        public void Subscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>
        {
            lock (HandlerMapping)
            {
                var handlers = HandlerMapping.GetOrAdd(typeof(TEventData), new List<Type>());
                handlers.Add(typeof(TEventHandler));
            }
        }

        public void UbSubscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>
        {
            lock (HandlerMapping)
            {
                var handlers = HandlerMapping.GetOrAdd(typeof(TEventData), new List<Type>());
                handlers.Remove(typeof(TEventHandler));
            }
        }
    }
}
