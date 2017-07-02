using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Configuration
{
    public interface IStartupConfiguration
    {
        IConfiguration Configuration { get; }

        IHostingEnvironment HostingEnvironment { get; }

        ILoggerFactory LoggerFactory { get; }
    }
}
