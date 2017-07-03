using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public class NullEventBus : IEventBus
    {
        public void Publish<TEventData>(TEventData eventData) 
        {

        }

        public void Subscribe(Type eventDataType, Type eventHandlerType)
        {

        }

        public void Unsubscribe(Type eventDataType, Type eventHandlerType)
        {

        }
    }
}
