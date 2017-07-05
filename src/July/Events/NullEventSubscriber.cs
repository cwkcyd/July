using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events
{
    public class NullEventSubscriber : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
