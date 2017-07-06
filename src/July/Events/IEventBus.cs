using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace July.Events
{
    public interface IEventBus
    {
        IDisposable Subscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>;

        IDisposable Subscribe<TEventData>(IEventHandler<TEventData> handler)
            where TEventData : IEventData;

        IDisposable Subscribe<TEventData>(Action<TEventData> handler)
            where TEventData : IEventData;

        void Unsubscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>;

        void Unsubscribe<TEventData>(IEventHandler<TEventData> handler)
            where TEventData : IEventData;

        void Unsubscribe<TEventData>(Action<TEventData> handler)
            where TEventData : IEventData;

        void Publish<TEventData>(TEventData eventData)
            where TEventData : IEventData;

        Task PublishAsync<TEventData>(TEventData eventData)
            where TEventData : IEventData;
    }
}
