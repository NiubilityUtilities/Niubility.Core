using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Niubility.Injection
{
    public static class Register
    {
        public static void RegisterService(this IServiceCollection services, Type service)
        {
            foreach (var attribute in service.GetCustomAttributes<InjectionAttribute>())
            {
                var implementationType = service;
                var serviceType = attribute.ServiceType ?? implementationType;
                switch (attribute.Lifetime)
                {
                    case InjectionLifetimeTypes.Singleton:
                        ServiceCollectionServiceExtensions.AddSingleton(services, serviceType, implementationType);
                        break;
                    case InjectionLifetimeTypes.Scoped:
                        ServiceCollectionServiceExtensions.AddScoped(services, serviceType, implementationType);
                        break;
                    case InjectionLifetimeTypes.Transient:
                    default:
                        ServiceCollectionServiceExtensions.AddTransient(services, serviceType, implementationType);
                        break;
                }
            }
        }

        public static void RegisterService<T>(this IServiceCollection services)
        {
            var service = typeof(T);
            RegisterService(services, service);
        }

        public static void RegisterServices(this IServiceCollection services, Assembly assembly)
        {
            if (null == assembly)
            {
                return;
            }

            foreach (var service in assembly.GetTypes())
            {
                RegisterService(services, service);
            }
        }
    }
}