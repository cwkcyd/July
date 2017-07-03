using July.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public abstract class JulyEventHandler<TEventData> : IEventHandler<TEventData>
    {
        public abstract void Handle(TEventData eventData);
    }
}
