using July.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using July.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using July.Sample.Events;
using July.Events;

namespace July.Sample
{
    public class JulySampleModule : JulyModule
    {
        public override void Initialize(IocBuilder builder)
        {
            base.Initialize(builder);

            builder.AddMvc();

            builder.RegisterAssemblyByConvention(ThisAssembly);
        }

        public override void Load(IIocContainer container)
        {
            base.Load(container);

            container.Resolve<IEventBus>().Subscribe<TestEventData, TestEventHandler>();
        }
    }
}
