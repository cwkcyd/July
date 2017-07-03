using July.Events;
using July.Ioc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace July.Sample.Events
{
    [Transient(EnableClassInterceptors = true, InterceptorBy = new Type[] { typeof(TestInterceptor) })]
    public class TestEventHandler : JulyEventHandler<TestEventData>, ILifetimeEvents
    {
        public ILogger<TestEventHandler> Logger { get; set; }

        public TestEventHandler()
        {
            Logger = NullLogger<TestEventHandler>.Instance;
        }

        public override void Handle(TestEventData eventData)
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
