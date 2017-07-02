using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public interface IEventHandler<TEventData>
        where TEventData : IEventData
    {
        void Handle(TEventData eventData);
    }
}
