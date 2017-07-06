using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events.Internal
{
    internal class ActionEventHandler<TEventData> : IEventHandler<TEventData>
        where TEventData : IEventData
    {
        public Action<TEventData> Action { get; private set; }

        public ActionEventHandler(Action<TEventData> action)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Handle(TEventData eventData)
        {
            Action.Invoke(eventData);
        }
    }
}
