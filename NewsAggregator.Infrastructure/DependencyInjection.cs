using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsAggregator.Application.Common.Authentication;
using NewsAggregator.Application.Common.Caching;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Messaging;
using NewsAggregator.Infrastructure.Authentication;
using NewsAggregator.Infrastructure.Caching;
using NewsAggregator.Infrastructure.Messaging;
using NewsAggregator.Infrastructure.Persistence;
using NewsAggregator.Infrastructure.Rss;
using NewsAggregator.Infrastructure.Services;
using RabbitMQ.Client;
using StackExchange.Redis;

namespace NewsAggregator.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddShared(configuration);

        services.AddScoped<IUserContext, UserContext>();

        var connectionString = configuration
            .GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException();

        services.AddHangfire(
            x =>
                x.UsePostgreSqlStorage(
                    options =>
                        options.UseNpgsqlConnection(
                            connectionString)));

        return services;
    }

    public static IServiceCollection AddWorkerInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddShared(configuration);
    }

    private static IServiceCollection AddShared(
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

        services.AddHttpClient();

        services.AddScoped<IRssParser, RssParser>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

        if (configuration.GetValue<bool>("UseRedisCache"))
        {
            var redisConnectionString = configuration
                .GetConnectionString("Redis")
                ?? throw new InvalidOperationException("Redis connection string is not configured.");

            var redisOptions = ConfigurationOptions.Parse(redisConnectionString);
            redisOptions.AllowAdmin = true;

            services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect(redisOptions));

            services.AddSingleton<ICacheService, RedisCacheService>();
        }
        else
        {
            services.AddSingleton<ICacheService, MemoryCacheService>();
        }

        services.Configure<RabbitMqOptions>(
            configuration.GetSection(RabbitMqOptions.SectionName));

        var rabbitMqOptions = configuration
            .GetSection(RabbitMqOptions.SectionName)
            .Get<RabbitMqOptions>()!;

        var connectionFactory = new ConnectionFactory
        {
            HostName = rabbitMqOptions.HostName,
            Port = rabbitMqOptions.Port,
            UserName = rabbitMqOptions.UserName,
            Password = rabbitMqOptions.Password
        };

        var connection = connectionFactory.CreateConnectionAsync().GetAwaiter().GetResult();
        var channel = connection.CreateChannelAsync().GetAwaiter().GetResult();

        RabbitMqTopology.DeclareAsync(channel).GetAwaiter().GetResult();

        services.AddSingleton(connection);
        services.AddSingleton(channel);
        services.AddSingleton<IMessagePublisher, RabbitMqMessagePublisher>();

        return services;
    }
}