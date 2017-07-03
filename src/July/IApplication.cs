using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace July
{
    public interface IApplication
    {
        Type StartupModule { get; }

        void Run(IApplicationBuilder app);
    }
}
