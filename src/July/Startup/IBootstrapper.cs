using System.Threading.Tasks;

namespace July.Startup
{
    public interface IBootstrapper<in TApplication> where TApplication : Application
    {
        void Run();

        Task RunAsync();
    }
}
