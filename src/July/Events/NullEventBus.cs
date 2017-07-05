using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace July.Events
{
    public class NullEventBus : IEventBus
    {
        public void Publish<TEvent>(TEvent eventData)
        {

        }

        public Task PublishAsync<TEvent>(TEvent eventData)
        {
            return Task.FromResult(0);
        }

        public IDisposable Subscribe<TEventData, TEventHandler>() where TEventHandler : IEventHandler<TEventData>
        {
            return new NullEventSubscriber();
        }

        public IDisposable Subscribe<TEventData>(IEventHandler<TEventData> handler)
        {
            return new NullEventSubscriber();
        }

        public IDisposable Subscribe<TEventData>(Action<TEventData> handler)
        {
            return new NullEventSubscriber();
        }

        public void Unsubscribe<TEventData, TEventHandler>() where TEventHandler : IEventHandler<TEventData>
        {
            
        }

        public void Unsubscribe<TEventData>(IEventHandler<TEventData> handler)
        {

        }

        public void Unsubscribe<TEventData>(Action<TEventData> handler)
        {

        }
    }
}
