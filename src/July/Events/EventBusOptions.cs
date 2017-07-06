using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public class EventBusOptions
    {
        public ConcurrentDictionary<Type, List<Type>> InitialHandlers { get; set; }

        public EventBusOptions()
        {
            InitialHandlers = new ConcurrentDictionary<Type, List<Type>>();
        }

        public void AddInitialHandler(Type eventDataType, Type eventHandlerType)
        {
            var handlerTypeList = InitialHandlers.GetOrAdd(eventDataType, new List<Type>());

            handlerTypeList.Add(eventHandlerType);
        }
    }
}
