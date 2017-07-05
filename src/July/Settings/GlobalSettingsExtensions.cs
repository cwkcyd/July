using July.Ioc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Settings
{
    public static class GlobalSettingsExtensions
    {
        /// <summary>
        /// Retrive the startup configuration
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IConfiguration Configuration(this GlobalSettings settings)
        {
            return settings.Get<IConfiguration>();
        }

        /// <summary>
        /// Retrive the startup environment information
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IHostingEnvironment HostingEnvironment(this GlobalSettings settings)
        {
            return settings.Get<IHostingEnvironment>();
        }

        /// <summary>
        /// Retrive the IoC convention options
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IocConventionOptions IocConventionOptions(this GlobalSettings settings)
        {
            return settings.Get<IocConventionOptions>();
        }
    }
}
