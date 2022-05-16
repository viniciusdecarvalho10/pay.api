using Pay.Api.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Pay.Api.Host.DependencyGroups
{
    public static class AutoRegisterDependencyGroupExtension
    {
        public static void RegisterDependencyGroupFromAssemblies(this IServiceCollection serviceCollection)
        {
            var serviceDependencyType = typeof(IDependencyGroup);
            var serviceDependencies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => serviceDependencyType.IsAssignableFrom(p) && !p.IsInterface)
                .ToList();

            serviceDependencies.ForEach(type =>
            {
                var instance = (IDependencyGroup)Activator.CreateInstance(type);
                instance.Register(serviceCollection);
            });
        }
    }
}