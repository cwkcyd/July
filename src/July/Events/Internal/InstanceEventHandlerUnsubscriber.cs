using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events.Internal
{
    public class InstanceEventHandlerUnsubscriber<TEventData> : IDisposable
        where TEventData : IEventData
    {
        private IEventBus _eventBus;

        private IEventHandler<TEventData> _eventHandler;

        public InstanceEventHandlerUnsubscriber(IEventBus eventBus, IEventHandler<TEventData> eventHandler) 
        {
            _eventBus = eventBus;
            _eventHandler = eventHandler;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<TEventData>(_eventHandler);
        }
    }
}
