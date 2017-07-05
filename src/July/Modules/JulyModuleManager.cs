using July.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using System.Linq;
using July.Startup;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;

namespace July.Modules
{
    [Ignore]
    public class JulyModuleManager
    {
        private JulyModuleInfo StartupModule { get; set; }

        private Type StartupModuleType { get; set; }

        private JulyModuleCollection _moduleList;

        private List<JulyModuleInfo> ModuleList { get; set; }

        private ILogger<JulyModuleManager> Logger { get; set; }

        public JulyModuleManager(Type startupModule)
        {
            StartupModuleType = startupModule;
        }

        public void Initialize(IocBuilder iocBuilder, IStartupService startupConfiguration)
        {
            iocBuilder.RegisterInstance(this).AsSelf().SingleInstance();

            Logger = startupConfiguration.LoggerFactory.CreateLogger<JulyModuleManager>();

            _moduleList = new JulyModuleCollection(StartupModuleType);

            LoadAllModules(iocBuilder, startupConfiguration);

            var sortedModules = _moduleList.GetSortedModuleListByDependency();

            foreach (var module in sortedModules)
            {
                module.Instance.Initialize();
                Logger.LogDebug($"Module: {module.Type.AssemblyQualifiedName} initialized");
            }
        }

        public void ConfigureServices(IocBuilder iocBuilder)
        {
            var sortedModules = _moduleList.GetSortedModuleListByDependency();

            foreach (var module in sortedModules)
            {
                module.Instance.ConfigureServices(iocBuilder);
                Logger.LogDebug($"Module: {module.Type.AssemblyQualifiedName} configure services completed");
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            var sortedModules = _moduleList.GetSortedModuleListByDependency();

            foreach (var module in sortedModules)
            {
                module.Instance.Configure(app);
                Logger.LogDebug($"Module: {module.Type.AssemblyQualifiedName} configure completed");
            }
        }

        public void OnApplicationStart()
        {
            var sortedModules = _moduleList.GetSortedModuleListByDependency();

            foreach (var module in sortedModules)
            {
                module.Instance.OnApplicationStart();
                Logger.LogDebug($"Module: {module.Type.AssemblyQualifiedName} OnApplicationStart");
            }
        }

        public void OnApplicationShutdown()
        {
            var sortedModules = _moduleList.GetSortedModuleListByDependency().Reverse<JulyModuleInfo>();

            foreach (var module in sortedModules)
            {
                Logger.LogDebug($"Module: {module.Type.AssemblyQualifiedName} OnApplicationShutdown");
                module.Instance.OnApplicationShutdown();
            }
        }

        private void LoadAllModules(IocBuilder iocBuilder, IStartupService startupConfiguration)
        {
            Logger.LogDebug("Loading modules...");
            
            var moduleTypes = FindAllModuleTypes().Distinct().ToList();

            Logger.LogDebug("Found " + moduleTypes.Count + " modules in total.");
            
            CreateModules(iocBuilder, startupConfiguration, moduleTypes);

            _moduleList.EnsureKernelModuleToBeFirst();
            _moduleList.EnsureStartupModuleToBeLast();

            SetDependencies();

            Logger.LogDebug("{0} modules loaded.", _moduleList.Count);
        }

        private List<Type> FindAllModuleTypes()
        {
            var modules = JulyModule.FindDependedModuleTypesRecursivelyIncludingGivenModule(_moduleList.StartupModuleType);

            return modules;
        }

        private void CreateModules(IocBuilder iocBuilder, IStartupService startupConfiguration, ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = Activator.CreateInstance(moduleType) as JulyModule;
                if (moduleObject == null)
                {
                    throw new InvalidOperationException("This type is not a July Module: " + moduleType.AssemblyQualifiedName);
                }
                
                moduleObject.StartupService = startupConfiguration;

                var moduleInfo = new JulyModuleInfo(moduleType);
                moduleInfo.Instance = moduleObject;

                _moduleList.Add(moduleInfo);

                if (moduleType == _moduleList.StartupModuleType)
                {
                    StartupModule = moduleInfo;
                }

                iocBuilder.RegisterInstance(moduleObject).AsSelf().SingleInstance();

                Logger.LogDebug("Create module instance: " + moduleType.AssemblyQualifiedName);
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _moduleList)
            {
                moduleInfo.Dependencies.Clear();

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in JulyModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _moduleList.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new InvalidOperationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }
    }
}
