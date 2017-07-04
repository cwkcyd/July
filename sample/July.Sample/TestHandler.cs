using July.Aspect;
using July.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace July.Sample
{
    [Transient]
    public class TestHandler : IHandler
    {
        public virtual void Test()
        {

        }
    }
    
    [Intercept(typeof(TestInterceptor))]
    public interface IHandler
    {
        void Test();
    }
}
