using July.Aspect;
using July.Events;
using July.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace July.Sample
{
    public class TestEventHandler : IEventHandler<TestEventData>
    {
        public TestEventHandler()
        {

        }

        public void Handle(TestEventData eventData)
        {

        }
    }

    public class TestEventData : IEventData
    {

    }
}
