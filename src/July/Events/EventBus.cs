using Autofac;
using July.Ioc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace July.Events
{
    [Singleton]
    public class EventBus : IEventBus
    {
        private IIocContainer IocContainer { get; set; }

        private ConcurrentDictionary<Type, List<Type>> HandlerMapping { get; set; }

        public ILogger<EventBus> Logger { get; set; }

        public EventBus(IIocContainer iocContainer)
        {
            IocContainer = iocContainer;
            HandlerMapping = new ConcurrentDictionary<Type, List<Type>>();
        }

        public void Publish<TEventData>(TEventData eventData)
        {
            if (HandlerMapping.TryGetValue(typeof(TEventData), out var handlers))
            {
                using (ILifetimeScope scope = IocContainer.BeginLifetimeScope())
                {
                    foreach (var handlerType in handlers)
                    {
                        IEventHandler<TEventData> handler = (IEventHandler<TEventData>)scope.ResolveOptional(handlerType);

                        if (handler == null)
                        {
                            Logger.LogWarning($"Cannot create instance: {handlerType.FullName}, make sure you have registered it in DI");
                            continue;
                        }

                        handler.Handle(eventData);
                    }
                }
            }
        }

        public void Subscribe(Type eventDataType, Type eventHandlerType)
        {
            var correctHandlerType = typeof(IEventHandler<>);
            correctHandlerType = correctHandlerType.MakeGenericType(eventDataType);

            if (!correctHandlerType.IsAssignableFrom(eventHandlerType))
            {
                Logger.LogWarning($"The handler: {eventHandlerType.FullName} cannot handle the event: {eventDataType.FullName} and it will be ignored");
                return;
            }

            lock (HandlerMapping)
            {
                var handlers = HandlerMapping.GetOrAdd(eventDataType, new List<Type>());
                handlers.Add(eventHandlerType);
            }
        }

        public void Unsubscribe(Type eventDataType, Type eventHandlerType)
        {
            lock (HandlerMapping)
            {
                var handlers = HandlerMapping.GetOrAdd(eventDataType, new List<Type>());
                handlers.Remove(eventHandlerType);
            }
        }
    }
}
