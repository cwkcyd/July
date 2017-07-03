using System;
using System.Collections.Generic;
using System.Text;

namespace July.Aspect
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class InterceptAttribute : Attribute
    {
        public bool EnableClassInterceptors { get; set; }

        public bool EnableInterfaceInterceptors { get; set; }

        public Type[] InterceptBy { get; set; }

        public InterceptAttribute(params Type[] interceptBy)
        {
            InterceptBy = interceptBy ?? throw new ArgumentNullException(nameof(interceptBy));
        }
    }
}
