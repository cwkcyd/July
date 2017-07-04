using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using July.Bootstrap.AspNetCore;
using Microsoft.Extensions.Logging;

namespace July.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            IBootstrapper<SampleApplication> bootstrapper = new Bootstrapper<SampleApplication>(args)
                    .ConfigureWebHostBuilder(builder =>
                    {
                        builder.UseKestrel()
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseIISIntegration()
                            .UseApplicationInsights();
                    });

            bootstrapper.Run();
        }
    }
}
