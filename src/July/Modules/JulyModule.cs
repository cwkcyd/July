using July.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace July.Modules
{
    public abstract class JulyModule
    {
        public virtual void Initialize(IocBuilder builder)
        {
            
        }

        public virtual void Load(IIocContainer container)
        {
            
        }
    }
}
