using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Likvido.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddImplementations<T>(
        this IServiceCollection services,
        AddImplementationFilter addImplementationFilter = AddImplementationFilter.All)
    {
        return services.AddImplementations<T>(typeof(T), addImplementationFilter);
    }

    public static IServiceCollection AddImplementations<T>(
        this IServiceCollection services,
        Type baseType,
        AddImplementationFilter addImplementationFilter = AddImplementationFilter.All)
    {
        var serviceTypes = baseType.Assembly
            .DefinedTypes
            .Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        foreach (var serviceType in serviceTypes)
        {
            var types = serviceType.GetTypeInfo().ImplementedInterfaces.Where(i => typeof(T).IsAssignableFrom(i)).ToList();

            switch (addImplementationFilter)
            {
                case AddImplementationFilter.ExcludeBase:
                    types = types.Where(t => t != typeof(T)).ToList();
                    break;
                case AddImplementationFilter.OnlyBase:
                    types = types.Where(t => t == typeof(T)).ToList();
                    break;
                case AddImplementationFilter.All:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(addImplementationFilter), addImplementationFilter, null);
            }

            foreach (var serviceInterface in types)
            {
                services.AddScoped(serviceInterface, serviceType);
            }
        }

        return services;
    }
}
