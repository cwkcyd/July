using Microsoft.Extensions.DependencyInjection;

namespace July.Ioc
{
    public class SingletonAttribute : ComponentAttribute
    {
        public SingletonAttribute() : base(ServiceLifetime.Singleton)
        {

        }
    }
}
