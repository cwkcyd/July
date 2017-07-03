using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using July.Ioc;

namespace July.Sample
{
    [Singleton]
    public class TestInterceptor : StandardInterceptor
    {
        public TestInterceptor()
        {

        }

        protected override void PreProceed(IInvocation invocation)
        {
            base.PreProceed(invocation);
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            base.PerformProceed(invocation);
        }

        protected override void PostProceed(IInvocation invocation)
        {
            base.PostProceed(invocation);
        }
    }
}
