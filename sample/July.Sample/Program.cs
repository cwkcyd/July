using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using July.Bootstrap;

namespace July.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Bootstrapper<Startup>(args).Run();
        }
    }
}
