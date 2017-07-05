using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public class EventBusOptions
    {
        public ConcurrentDictionary<Type, List<Type>> HandlerMappings { get; set; }

        public EventBusOptions()
        {
            HandlerMappings = new ConcurrentDictionary<Type, List<Type>>();
        }
    }
}
