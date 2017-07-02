using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    public interface IIocContainer
    {
        T Resolve<T>();
    }
}
