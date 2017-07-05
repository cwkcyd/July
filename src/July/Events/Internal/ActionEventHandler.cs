using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events.Internal
{
    internal class ActionEventHandler<TEventData> : IEventHandler<TEventData>
    {
        private Action<TEventData> _action;

        public ActionEventHandler(Action<TEventData> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Handle(TEventData eventData)
        {
            _action.Invoke(eventData);
        }
    }
}
