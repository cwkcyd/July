using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Builder;
using System.Reflection;
using July.Aspect;
using July.Extensions;
using Autofac.Core;
using System.Linq;
using System.Globalization;
using Castle.DynamicProxy.Internal;
using Autofac;
using Castle.DynamicProxy;
using Autofac.Features.Scanning;

namespace July.Ioc.Conventions
{
    public class AspectRegister : IConventionRegister
    {
        public IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> Register(IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration, Type type)
        {
            var attribute = type.GetFirstAttribute<InterceptAttribute>();
            if (attribute == null)
            {
                return registration;
            }

            if (attribute.EnableClassInterceptors)
            {
                registration = EnableClassInterceptors(registration);
            }
            if (attribute.EnableInterfaceInterceptors)
            {
                registration = EnableInterfaceInterceptors(registration);
            }
            if (!attribute.InterceptBy.IsNullOrEmpty())
            {
                registration = InterceptedBy(registration, attribute.InterceptBy);
            }

            return registration;
        }

        private const string InterceptorsPropertyName = "Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName";

        private const string AttributeInterceptorsPropertyName = "Autofac.Extras.DynamicProxy.RegistrationExtensions.AttributeInterceptorsPropertyName";

        private static readonly IEnumerable<Service> EmptyServices = Enumerable.Empty<Service>();

        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        /// <summary>
        /// Enable class interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or added with InterceptedBy().
        /// Only virtual methods can be intercepted this way.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> EnableClassInterceptors<TLimit, TRegistrationStyle>(
            IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> registration)
        {
            return EnableClassInterceptors(registration, ProxyGenerationOptions.Default);
        }

        /// <summary>
        /// Enable class interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or added with InterceptedBy().
        /// Only virtual methods can be intercepted this way.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TConcreteReflectionActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> EnableClassInterceptors<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>(
            IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> registration)
            where TConcreteReflectionActivatorData : ConcreteReflectionActivatorData
        {
            return EnableClassInterceptors(registration, ProxyGenerationOptions.Default);
        }

        /// <summary>
        /// Enable class interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or added with InterceptedBy().
        /// Only virtual methods can be intercepted this way.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="options">Proxy generation options to apply.</param>
        /// <param name="additionalInterfaces">Additional interface types. Calls to their members will be proxied as well.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> EnableClassInterceptors<TLimit, TRegistrationStyle>(
            IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> registration,
            ProxyGenerationOptions options,
            params Type[] additionalInterfaces)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            registration.ActivatorData.ConfigurationActions.Add((t, rb) => EnableClassInterceptors(rb, options, additionalInterfaces));
            return registration;
        }

        /// <summary>
        /// Enable class interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or added with InterceptedBy().
        /// Only virtual methods can be intercepted this way.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TConcreteReflectionActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="options">Proxy generation options to apply.</param>
        /// <param name="additionalInterfaces">Additional interface types. Calls to their members will be proxied as well.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> EnableClassInterceptors<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>(
            IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> registration,
            ProxyGenerationOptions options,
            params Type[] additionalInterfaces)
            where TConcreteReflectionActivatorData : ConcreteReflectionActivatorData
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            registration.ActivatorData.ImplementationType =
                ProxyGenerator.ProxyBuilder.CreateClassProxyType(
                    registration.ActivatorData.ImplementationType,
                    additionalInterfaces ?? new Type[0],
                    options);

            var interceptorServices = GetInterceptorServicesFromAttributes(registration.ActivatorData.ImplementationType);
            AddInterceptorServicesToMetadata(registration, interceptorServices, AttributeInterceptorsPropertyName);

            registration.OnPreparing(e =>
            {
                var proxyParameters = new List<Parameter>();
                int index = 0;

                if (options.HasMixins)
                {
                    foreach (var mixin in options.MixinData.Mixins)
                    {
                        proxyParameters.Add(new PositionalParameter(index++, mixin));
                    }
                }

                proxyParameters.Add(new PositionalParameter(index++, GetInterceptorServices(e.Component, registration.ActivatorData.ImplementationType)
                    .Select(s => e.Context.ResolveService(s))
                    .Cast<IInterceptor>()
                    .ToArray()));

                if (options.Selector != null)
                {
                    proxyParameters.Add(new PositionalParameter(index, options.Selector));
                }

                e.Parameters = proxyParameters.Concat(e.Parameters).ToArray();
            });

            return registration;
        }

        /// <summary>
        /// Enable interface interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or interface, or added with InterceptedBy() calls.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> EnableInterfaceInterceptors<TLimit, TActivatorData, TSingleRegistrationStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration)
        {
            return EnableInterfaceInterceptors(registration, null);
        }

        /// <summary>
        /// Enable interface interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or interface, or added with InterceptedBy() calls.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="options">Proxy generation options to apply.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> EnableInterfaceInterceptors<TLimit, TActivatorData, TSingleRegistrationStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration, ProxyGenerationOptions options)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            registration.RegistrationData.ActivatingHandlers.Add((sender, e) =>
            {
                EnsureInterfaceInterceptionApplies(e.Component);

                var proxiedInterfaces = e.Instance
                    .GetType()
                    .GetInterfaces()
                    .Where(i =>
                    {
                        var ti = i.GetTypeInfo();
                        return ti.IsVisible || ti.Assembly.IsInternalToDynamicProxy();
                    })
                    .ToArray();

                if (!proxiedInterfaces.Any())
                {
                    return;
                }

                var theInterface = proxiedInterfaces.First();
                var interfaces = proxiedInterfaces.Skip(1).ToArray();

                var interceptors = GetInterceptorServices(e.Component, e.Instance.GetType())
                    .Select(s => e.Context.ResolveService(s))
                    .Cast<IInterceptor>()
                    .ToArray();

                e.Instance = options == null
                    ? ProxyGenerator.CreateInterfaceProxyWithTarget(theInterface, interfaces, e.Instance, interceptors)
                    : ProxyGenerator.CreateInterfaceProxyWithTarget(theInterface, interfaces, e.Instance, options, interceptors);
            });

            return registration;
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServices">The interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        private static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy<TLimit, TActivatorData, TStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            params Service[] interceptorServices)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (interceptorServices == null || interceptorServices.Any(s => s == null))
            {
                throw new ArgumentNullException(nameof(interceptorServices));
            }

            AddInterceptorServicesToMetadata(builder, interceptorServices, InterceptorsPropertyName);

            return builder;
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServiceNames">The names of the interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        private static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy<TLimit, TActivatorData, TStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            params string[] interceptorServiceNames)
        {
            if (interceptorServiceNames == null || interceptorServiceNames.Any(n => n == null))
            {
                throw new ArgumentNullException(nameof(interceptorServiceNames));
            }

            return InterceptedBy(builder, interceptorServiceNames.Select(n => new KeyedService(n, typeof(IInterceptor))).ToArray());
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServiceTypes">The types of the interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        private static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy<TLimit, TActivatorData, TStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            params Type[] interceptorServiceTypes)
        {
            if (interceptorServiceTypes == null || interceptorServiceTypes.Any(t => t == null))
            {
                throw new ArgumentNullException(nameof(interceptorServiceTypes));
            }

            return InterceptedBy(builder, interceptorServiceTypes.Select(t => new TypedService(t)).ToArray());
        }
        
        private static void EnsureInterfaceInterceptionApplies(IComponentRegistration componentRegistration)
        {
            if (componentRegistration.Services
                .OfType<IServiceWithType>()
                .Select(s => s.ServiceType.GetTypeInfo())
                .Any(s => !s.IsInterface || (!s.Assembly.IsInternalToDynamicProxy() && !s.IsVisible)))
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "The component {0} cannot use interface interception as it provides services that are not publicly visible interfaces. Check your registration of the component to ensure you're not enabling interception and registering it as an internal/private interface type.",
                        componentRegistration));
            }
        }

        private static void AddInterceptorServicesToMetadata<TLimit, TActivatorData, TStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            IEnumerable<Service> interceptorServices,
            string metadataKey)
        {
            object existing;
            if (builder.RegistrationData.Metadata.TryGetValue(metadataKey, out existing))
            {
                builder.RegistrationData.Metadata[metadataKey] =
                    ((IEnumerable<Service>)existing).Concat(interceptorServices).Distinct();
            }
            else
            {
                builder.RegistrationData.Metadata.Add(metadataKey, interceptorServices);
            }
        }

        private static IEnumerable<Service> GetInterceptorServices(IComponentRegistration registration, Type implType)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            if (implType == null)
            {
                throw new ArgumentNullException(nameof(implType));
            }

            var result = EmptyServices;

            object services;
            if (registration.Metadata.TryGetValue(InterceptorsPropertyName, out services))
            {
                result = result.Concat((IEnumerable<Service>)services);
            }

            return registration.Metadata.TryGetValue(AttributeInterceptorsPropertyName, out services)
                ? result.Concat((IEnumerable<Service>)services)
                : result.Concat(GetInterceptorServicesFromAttributes(implType));
        }

        private static IEnumerable<Service> GetInterceptorServicesFromAttributes(Type implType)
        {
            var implTypeInfo = implType.GetTypeInfo();
            if (!implTypeInfo.IsClass) return Enumerable.Empty<Service>();

            var classAttributeServices = implTypeInfo
                .GetCustomAttributes(typeof(InterceptAttribute), true)
                .Cast<InterceptAttribute>()
                .SelectMany(att => att.InterceptBy.Select(t => new TypedService(t)));

            var interfaceAttributeServices = implType
                .GetInterfaces()
                .SelectMany(i => i.GetTypeInfo().GetCustomAttributes(typeof(InterceptAttribute), true))
                .Cast<InterceptAttribute>()
                .SelectMany(att => att.InterceptBy.Select(t => new TypedService(t)));

            return classAttributeServices.Concat(interfaceAttributeServices);
        }
    }
}
