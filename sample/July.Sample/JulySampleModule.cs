using July.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using July.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace July.Sample
{
    [DependOn(typeof(JulySample2Module))]
    public class JulySampleModule : JulyModule
    {
        public override void Initialize(IocBuilder builder)
        {
            base.Initialize(builder);

            builder.AddMvc();
        }
    }

    public class JulySample2Module : JulyModule
    {
        public override void Initialize(IocBuilder builder)
        {
            base.Initialize(builder);
        }
    }
}
