using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public class NullEventBus : IEventBus
    {
        public void Publish<TEventData>(TEventData eventData) where TEventData : IEventData
        {

        }

        public void Subscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>
        {

        }

        public void UbSubscribe<TEventData, TEventHandler>()
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>
        {

        }
    }
}
