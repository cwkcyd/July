using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace July.Events
{
    public interface IEventBus
    {
        IDisposable Subscribe<TEventData, TEventHandler>()
            where TEventHandler : IEventHandler<TEventData>;

        IDisposable Subscribe<TEventData>(IEventHandler<TEventData> handler);

        IDisposable Subscribe<TEventData>(Action<TEventData> handler);

        void Unsubscribe<TEventData, TEventHandler>()
            where TEventHandler : IEventHandler<TEventData>;

        void Unsubscribe<TEventData>(IEventHandler<TEventData> handler);

        void Unsubscribe<TEventData>(Action<TEventData> handler);

        void Publish<TEventData>(TEventData eventData);

        Task PublishAsync<TEventData>(TEventData eventData);
    }
}
