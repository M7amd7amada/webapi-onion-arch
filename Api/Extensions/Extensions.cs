using Application;

using Domain;

using Infrastructure.Constants;
using Infrastructure.Settings;

using Serilog;

namespace Api.Extensions;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        AppSettings appSettings = builder.Configuration
            .GetSection(SectionsNames.SettingsConfiguration).Get<AppSettings>()!;
        CorsSettings corsSettings = appSettings?.CorsSettings!;

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services
            .AddApi()
            .AddDomain()
            .AddApplication()
            .AddInfrastructure(builder.Configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(corsSettings.PolicyName!, builder =>
            {
                builder.WithOrigins(corsSettings.Origins!)
                       .WithHeaders(corsSettings.Headers!)
                       .WithMethods(corsSettings.Methods!);
            });
        });

        return builder;
    }
}