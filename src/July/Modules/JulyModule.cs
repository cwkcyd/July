using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Modules
{
    public abstract class JulyModule
    {
        public virtual void PreInitialize()
        {

        }

        public virtual void Initialize(IServiceCollection services)
        {

        }

        public virtual void Load()
        {

        }
    }
}
