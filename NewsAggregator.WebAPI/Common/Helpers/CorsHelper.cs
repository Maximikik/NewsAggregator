using NewsAggregator.WebAPI.Common.Options;

namespace NewsAggregator.WebAPI.Common.Helpers;

public static class CorsHelper
{
    public static IServiceCollection AddConfiguredCors(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CorsPolicyOptions>(
            configuration.GetSection(CorsPolicyOptions.SectionName));

        var corsOptions =
            configuration
            .GetSection(CorsPolicyOptions.SectionName)
            .Get<CorsPolicyOptions>()!;

        services.AddCors(options =>
        {
            options.AddPolicy("Default", policy =>
            {
                policy
                    .WithOrigins(corsOptions.AllowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}
