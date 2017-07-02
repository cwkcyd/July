using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    public interface IIocScope : IDisposable
    {
        IocBuilder IocManager { get; }
    }
}
