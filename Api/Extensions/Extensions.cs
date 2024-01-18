using Infrastructure.Constants;
using Infrastructure.Settings;

namespace Api.Extensions;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        AppSettings? appSettings = builder.Configuration
            .GetSection(SectionsNames.SettingsConfiguration).Get<AppSettings>();
        CorsSettings? corsSettings = appSettings?.CorsSettings;

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        ConfigureCors(builder, corsSettings);

        builder.Services
            .AddApi()
            .AddInfrastructure(builder.Configuration);

        return builder;
    }

    private static void ConfigureCors(WebApplicationBuilder builder, CorsSettings? corsSettings)
    {
        try
        {
            if (corsSettings is null)
            {
                throw new ArgumentNullException(nameof(corsSettings));
            }

            var policyName = corsSettings.PolicyName ?? throw new ArgumentNullException(nameof(corsSettings));
            var origins = corsSettings.Origins ?? throw new ArgumentNullException(nameof(corsSettings));
            var methods = corsSettings.Methods ?? throw new ArgumentNullException(nameof(corsSettings));
            var headers = corsSettings.Headers ?? throw new ArgumentNullException(nameof(corsSettings));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(policyName, builder =>
                {
                    builder.WithOrigins(origins)
                           .WithHeaders(headers)
                           .WithMethods(methods);
                });
            });
        }
        catch
        {

        }
    }
}