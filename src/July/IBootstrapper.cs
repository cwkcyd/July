using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace July
{
    public interface IBootstrapper<in TApplication> where TApplication : Application
    {
        void Run();

        Task RunAsync();
    }
}
