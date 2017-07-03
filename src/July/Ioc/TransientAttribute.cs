using Microsoft.Extensions.DependencyInjection;

namespace July.Ioc
{
    public class TransientAttribute : ComponentAttribute
    {
        public TransientAttribute() : base(ServiceLifetime.Transient)
        {

        }
    }
}
