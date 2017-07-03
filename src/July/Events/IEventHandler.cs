using July.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public interface IEventHandler
    {

    }
    
    public interface IEventHandler<TEventData> : IEventHandler
    {
        void Handle(TEventData eventData);
    }
}
