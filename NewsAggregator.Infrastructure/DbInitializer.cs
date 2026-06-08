namespace NewsAggregator.Infrastructure;

public static class DbInitializer
{
    public static void Initialize(NewsAggregatorDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
