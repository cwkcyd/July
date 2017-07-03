using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace July
{
    public interface IBootstraper<in TApplication> where TApplication : class, IApplication
    {
        void Run();

        Task RunAsync();
    }
}
