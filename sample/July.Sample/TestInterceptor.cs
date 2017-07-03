using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using July.Ioc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace July.Sample
{
    [Singleton]
    public class TestInterceptor : StandardInterceptor
    {
        public ILogger<TestInterceptor> Logger { get; set; }

        public TestInterceptor()
        {
            Logger = NullLogger<TestInterceptor>.Instance;
        }

        protected override void PreProceed(IInvocation invocation)
        {
            Logger.LogError("PreProceed");

            base.PreProceed(invocation);
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            Logger.LogError("PerformProceed");
        }

        protected override void PostProceed(IInvocation invocation)
        {
            Logger.LogError("PostProceed");

            base.PostProceed(invocation);
        }
    }
}
