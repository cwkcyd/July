using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events.Internal
{
    internal class ActionEventHandlerUnsubscriber<TEventData> : IDisposable
    {
        private IEventBus _eventBus;

        private Action<TEventData> _eventHandler;

        public ActionEventHandlerUnsubscriber(IEventBus eventBus, Action<TEventData> eventHandler)
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
