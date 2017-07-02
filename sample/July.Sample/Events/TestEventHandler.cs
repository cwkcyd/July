using July.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace July.Sample.Events
{
    public class TestEventHandler : IEventHandler<TestEventData>
    {
        public ILogger<TestEventHandler> Logger { get; set; }

        public TestEventHandler()
        {
            //Logger.LogWarning("Handler created");
        }

        public void Handle(TestEventData eventData)
        {
            Logger.LogWarning("Handle event");
        }
    }
}
