using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public class EventSubscriber<TEventData> : IEventSubscriber
    {
        private ConcurrentDictionary<Type, List<IEventHandler>> _handlerMappings;

        public EventSubscriber()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
