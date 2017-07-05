using July.Ioc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace July.Settings
{
    public sealed class GlobalSettings
    {
        private static GlobalSettings _instance;

        public static GlobalSettings Instance
        {
            get
            {
                return _instance ?? throw new InvalidOperationException("Cannot get the instance of GlobalSettings, it hasn't been initialized");
            }
        }

        internal static void Initialize(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _instance = new GlobalSettings(configuration, hostingEnvironment);
        }

        private static ConcurrentDictionary<Type, object> _settings = new ConcurrentDictionary<Type, object>();

        private GlobalSettings(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (hostingEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostingEnvironment));
            }

            Set(configuration);
            Set(hostingEnvironment);
            Set(IocConventionOptions.Default);
        }

        /// <summary>
        /// Set setting value
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <param name="value"></param>
        public void Set<TSetting>(TSetting value)
        {
            Type key = typeof(TSetting);

            _settings[key] = value;
        }

        /// <summary>
        /// Get setting value
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <returns></returns>
        public TSetting Get<TSetting>()
        {
            Type key = typeof(TSetting);

            if (_settings.TryGetValue(key, out var value))
            {
                return (TSetting)value;
            }

            return default(TSetting);
        }

        /// <summary>
        /// Check if contains the setting value
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <returns></returns>
        public bool Contains<TSetting>()
        {
            Type key = typeof(TSetting);

            return _settings.ContainsKey(key);
        }
    }
}
