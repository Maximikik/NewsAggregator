using Hangfire;
using Hangfire.PostgreSql;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewsAggregator.Application.Common.Authentication;
using NewsAggregator.Application.Common.Caching;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Infrastructure.Authentication;
using NewsAggregator.Infrastructure.Caching;
using NewsAggregator.Infrastructure.Messaging;
using NewsAggregator.Infrastructure.Messaging.Consumers;
using NewsAggregator.Infrastructure.Persistence;
using NewsAggregator.Infrastructure.Rss;
using NewsAggregator.Infrastructure.Services;

namespace NewsAggregator.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<NewsAggregatorDbContext>(options =>
                options.UseInMemoryDatabase("NewsAggregatorDb"));
        }
        else
        {
            services.AddDbContext<NewsAggregatorDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(NewsAggregatorDbContext).Assembly.FullName)));
        }

        services.AddScoped<INewsAggregatorDbContext>(
            provider => provider.GetRequiredService<NewsAggregatorDbContext>());

        services.AddTransient<IDateTime, DateTimeService>();

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

        services.AddHttpClient();

        services.AddScoped<IRssParser,
            RssParser>();

        services.AddScoped<IUserContext,
            UserContext>();

        services.AddSingleton<ICacheService,
            MemoryCacheService>();

        var connectionString = configuration
            .GetConnectionString(
                "DefaultConnection")
            ?? throw new InvalidOperationException();

        services.AddHangfire(
            x =>
                x.UsePostgreSqlStorage(
                    options =>
                        options.UseNpgsqlConnection(
                            connectionString)));

        services.Configure<RabbitMqOptions>(
            configuration.GetSection(RabbitMqOptions.SectionName));

        services.AddScoped<IEventBus, MassTransitEventBus>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<ArticleImportedIntegrationConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqOptions = context
                    .GetRequiredService<IOptions<RabbitMqOptions>>()
                    .Value;

                cfg.Host(
                    rabbitMqOptions.Host,
                    rabbitMqOptions.VirtualHost,
                    h =>
                    {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
                    });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
