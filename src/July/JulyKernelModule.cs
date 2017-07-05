using July.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using July.Ioc;
using Autofac;
using July.Events;
using July.Settings;

namespace July
{
    public class JulyKernelModule : JulyModule
    {
        public override void Initialize()
        {
            base.Initialize();
            
            Settings.Set(new EventBusOptions());
            Settings.Set(IocConventionOptions.Default);
        }
    }
}
