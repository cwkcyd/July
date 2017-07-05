using Autofac;
using July.Ioc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using July.Settings;
using System.Threading.Tasks;

namespace July.Events
{
    [Singleton]
    public class EventBus : IEventBus
    {
        private IIocContainer IocContainer { get; set; }

        private ConcurrentDictionary<Type, List<IEventHandler>> HandlerMapping { get; set; }

        public ILogger<EventBus> Logger { get; set; }

        public EventBus(IIocContainer iocContainer)
        {
            IocContainer = iocContainer;
            HandlerMapping = GlobalSettings.Instance.EventBus().HandlerMappings;
        }

        public void Publish<TEventData>(TEventData eventData)
        {
            if (HandlerMapping.TryGetValue(typeof(TEventData), out var handlers))
            {
                foreach (var handler in handlers)
                {
                    IEventHandler<TEventData> eventHandler = (IEventHandler<TEventData>)handler;
                    eventHandler.Handle(eventData);
                }
            }
        }

        public IDisposable Subscribe<TEventData, TEventHandler>() 
            where TEventHandler : IEventHandler<TEventData>
        {
            var eventHandler = new Internal.IocEventHandler<TEventData, TEventHandler>();

            Type key = typeof(TEventData);

            var handlers = HandlerMapping.GetOrAdd(key, new List<IEventHandler>());
            handlers.Add(eventHandler);

            return null;
        }

        public IDisposable Subscribe<TEventData>(IEventHandler<TEventData> handler)
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe<TEventData>(Action<TEventData> handler)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<TEventData, TEventHandler>()
            where TEventHandler : IEventHandler<TEventData>
        {

        }

        public void Unsubscribe<TEventData>(IEventHandler<TEventData> handler)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<TEventData>(Action<TEventData> handler)
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync<TEvent>(TEvent eventData)
        {
            throw new NotImplementedException();
        }
    }
}
