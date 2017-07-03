using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public interface IEventBus
    {
        void Subscribe(Type eventDataType, Type eventHandlerType);

        void Unsubscribe(Type eventDataType, Type eventHandlerType);

        void Publish<TEvent>(TEvent eventData);
    }
}
