using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Likvido.Extensions.DependencyInjection;

public static class OptionsExtensions
{
    public static IServiceCollection AddOptions<T>(this IServiceCollection services, IConfigurationSection configurationSection, bool validate = false) where T : class
    {
        var optionsBuilder = services
            .AddOptions<T>()
            .Bind(configurationSection);

        if (validate)
        {
            optionsBuilder.ValidateDataAnnotations();
        }

        services.AddSingleton(sp =>
        {
            try
            {
                return sp.GetRequiredService<IOptions<T>>().Value;
            }
            catch (Exception e)
            {
                var logger = sp.GetRequiredService<ILogger<T>>();
                logger.LogCritical(e, "Failed to validate options of type {OptionsType}", typeof(T).FullName);
                throw;
            }
        });

        return services;
    }
}
