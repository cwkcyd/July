using Microsoft.Extensions.DependencyInjection;

namespace July.Ioc
{
    public class ScopedAttribute : ComponentAttribute
    {
        public ScopedAttribute() : base(ServiceLifetime.Scoped)
        {
        }
    }
}
