using July.Ioc.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    public class IocConventionOptions
    {
        public List<IConventionRegister> ConventionRegisters { get; private set; }

        public IocConventionOptions()
        {
            ConventionRegisters = new List<IConventionRegister>();            
        }

        internal static IocConventionOptions Default
        {
            get
            {
                var options = new IocConventionOptions();

                options.ConventionRegisters.Add(new ComponentRegister());
                options.ConventionRegisters.Add(new LifetimeEventsRegister());
                options.ConventionRegisters.Add(new AspectRegister());
                options.ConventionRegisters.Add(new EventHandlerRegister());

                return options;
            }
        }
    }
}
