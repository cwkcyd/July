using System;
using System.Collections.Generic;
using System.Text;

namespace July.Aspect
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public class InterceptAttribute : Attribute
    {
        public Type[] InterceptBy { get; set; }

        public InterceptAttribute(params Type[] interceptBy)
        {
            InterceptBy = interceptBy ?? throw new ArgumentNullException(nameof(interceptBy));
        }
    }
}
