using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public class EventSubscriber<TEventData> : IEventSubscriber
    {
        public EventSubscriber(IEventBus eventBus, IEventHandler<TEventData> handler)
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
