using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Startup
{
    public interface IStartupService
    {
        IConfiguration Configuration { get; }

        IHostingEnvironment HostingEnvironment { get; }

        ILoggerFactory LoggerFactory { get; }
    }
}
