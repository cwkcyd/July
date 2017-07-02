using July.Events;
using July.Ioc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace July.Sample.Events
{
    public class TestEventHandler : IEventHandler<TestEventData>, ILifetimeEvents
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

        public void OnActivating()
        {
            Logger.LogWarning("Activating");
        }

        public void OnActivated()
        {
            Logger.LogWarning("Activated");
        }

        public void OnRelease()
        {
            Logger.LogWarning("Released");
        }
    }
}
