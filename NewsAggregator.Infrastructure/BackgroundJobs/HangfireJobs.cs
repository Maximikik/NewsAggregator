using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace NewsAggregator.Infrastructure.BackgroundJobs;

public static class HangfireJobs
{
    public static IServiceProvider
        RegisterRecurringJobs(
        this IServiceProvider services)
    {
        var recurringJobs =
            services.GetRequiredService<
                IRecurringJobManager>();

        recurringJobs.AddOrUpdate<
            ImportArticlesJob>(
            "import-articles",
            x => x.Execute(
                CancellationToken.None),
            Cron.MinuteInterval(5));

        return services;
    }
}