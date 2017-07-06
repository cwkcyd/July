using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace July.Events
{
    public class NullEventBus : IEventBus
    {
        public void Publish<TEventData>(TEventData eventData)
            where TEventData : IEventData
        {

        }

        public Task PublishAsync<TEventData>(TEventData eventData)
            where TEventData : IEventData
        {
            return Task.FromResult(0);
        }

        public IDisposable Subscribe<TEventData, TEventHandler>() where TEventHandler : IEventHandler<TEventData>
            where TEventData : IEventData
        {
            return new NullEventSubscriber();
        }

        public IDisposable Subscribe<TEventData>(IEventHandler<TEventData> handler)
            where TEventData : IEventData
        {
            return new NullEventSubscriber();
        }

        public IDisposable Subscribe<TEventData>(Action<TEventData> handler)
            where TEventData : IEventData
        {
            return new NullEventSubscriber();
        }

        public void Unsubscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>
        {
            
        }

        public void Unsubscribe<TEventData>(IEventHandler<TEventData> handler)
            where TEventData : IEventData
        {

        }

        public void Unsubscribe<TEventData>(Action<TEventData> handler)
            where TEventData : IEventData
        {

        }
    }
}
