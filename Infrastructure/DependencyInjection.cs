using Application.Common.Interfaces.Services;

using Infrastructure.Constants;
using Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Settings;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AppSettings>(
            configuration.GetSection(SectionsNames.SettingsConfiguration));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}