using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events.Internal
{
    public class IocEventHandlerUnsubscriber<TEventData, TEventHandler> : IDisposable
        where TEventHandler : IEventHandler<TEventData>
    {
        private IEventBus _eventBus;

        public IocEventHandlerUnsubscriber(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<TEventData, TEventHandler>();
        }
    }
}
