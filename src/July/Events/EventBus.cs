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
using July.Extensions;
using System.Linq;

namespace July.Events
{
    [Singleton]
    public class EventBus : IEventBus
    {
        private ILifetimeScope LifetimeScope { get; set; }

        private readonly ConcurrentDictionary<Type, List<IEventHandler>> _handlerMapping = new ConcurrentDictionary<Type, List<IEventHandler>>();

        public ILogger<EventBus> Logger { get; set; }

        public EventBus(ILifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope;
        }

        public void Publish<TEventData>(TEventData eventData)
            where TEventData : IEventData
        {
            var handlers = GetEventHandlerList<TEventData>();
            foreach (var handler in handlers)
            {
                IEventHandler<TEventData> eventHandler = (IEventHandler<TEventData>)handler;
                eventHandler.Handle(eventData);
            }
        }

        public IDisposable Subscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>
        {
            var eventHandler = new Internal.IocEventHandler<TEventData>(LifetimeScope, typeof(TEventHandler));

            AddEventHandler<TEventData>(eventHandler);

            return new Internal.IocEventHandlerUnsubscriber<TEventData, TEventHandler>(this);
        }

        public IDisposable Subscribe<TEventData>(IEventHandler<TEventData> handler)
            where TEventData : IEventData
        {
            AddEventHandler<TEventData>(handler);

            return new Internal.InstanceEventHandlerUnsubscriber<TEventData>(this, handler);
        }

        public IDisposable Subscribe<TEventData>(Action<TEventData> handler)
            where TEventData : IEventData
        {
            var eventHandler = new Internal.ActionEventHandler<TEventData>(handler);

            AddEventHandler<TEventData>(eventHandler);

            return new Internal.ActionEventHandlerUnsubscriber<TEventData>(this, handler);
        }

        public void Unsubscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>
        {
            GetEventHandlerList<TEventData>().Lock(handlers =>
            {
                handlers.RemoveAll(t => t.CastAndMatch<Internal.IocEventHandler<TEventData>>(handler => handler.EventHandlerType == typeof(TEventHandler)));

                return handlers;
            });
        }

        public void Unsubscribe<TEventData>(IEventHandler<TEventData> handler)
            where TEventData : IEventData
        {
            GetEventHandlerList<TEventData>().Lock(handlers =>
            {
                handlers.RemoveAll(t => t == handler);

                return handlers;
            });
        }

        public void Unsubscribe<TEventData>(Action<TEventData> handler)
            where TEventData : IEventData
        {
            GetEventHandlerList<TEventData>().Lock(handlers =>
            {
                handlers.RemoveAll(t => t.CastAndMatch<Internal.ActionEventHandler<TEventData>>(h => h.Action == handler));

                return handlers;
            });
        }

        public Task PublishAsync<TEventData>(TEventData eventData)
            where TEventData : IEventData
        {
            List<Task> tasks = new List<Task>();
            var handlers = GetEventHandlerList<TEventData>();
            foreach (var handler in handlers)
            {
                IEventHandler<TEventData> eventHandler = (IEventHandler<TEventData>)handler;

                tasks.Add(Task.Factory.StartNew(() => eventHandler.Handle(eventData)));
            }

            return Task.WhenAll(tasks);
        }

        private void AddEventHandler<TEventData>(IEventHandler eventHandler)
        {
            var handlers = GetEventHandlerList<TEventData>();
            handlers.Add(eventHandler);
        }

        private List<IEventHandler> GetEventHandlerList<TEventData>()
        {
            Type key = typeof(TEventData);

            return _handlerMapping.GetOrAdd(key, new List<IEventHandler>());
        }
    }
}
