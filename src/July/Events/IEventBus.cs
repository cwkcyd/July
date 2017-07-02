using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public interface IEventBus
    {
        void Subscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>;

        void UbSubscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>;

        void Publish<TEventData>(TEventData eventData)
            where TEventData : IEventData;
    }
}
