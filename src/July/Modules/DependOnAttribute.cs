using System;
using System.Collections.Generic;
using System.Text;

namespace July.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependOnAttribute : Attribute
    {
        public Type[] DependOnModules { get; private set; }

        public DependOnAttribute(params Type[] dependOnModules)
        {
            DependOnModules = dependOnModules ?? throw new ArgumentNullException(nameof(dependOnModules));
        }
    }
}
