using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Domain.Common;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Infrastructure;

public class NewsAggregatorDbContext
    : DbContext, INewsAggregatorDbContext
{
    private readonly IDateTime _dateTimeService;
    private readonly IDomainEventDispatcher _dispatcher;

    public NewsAggregatorDbContext(
        DbContextOptions<NewsAggregatorDbContext> options,
        IDateTime dateTimeService,
        IDomainEventDispatcher dispatcher)
        : base(options)
    {
        _dateTimeService = dateTimeService;
        _dispatcher = dispatcher;
    }

    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Source> Sources => Set<Source>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserArticleLike> UserArticleLikes => Set<UserArticleLike>();
    public DbSet<UserCategoryPreference> UserCategoryPreferences => Set<UserCategoryPreference>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IBaseEntity).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = _dateTimeService.Now; //TODO: add other fields
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = _dateTimeService.Now; //TODO: add other fields
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    break;
            }
        }

        var events =
            ChangeTracker
            .Entries<IHasDomainEvents>()
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in events)
        {
            await _dispatcher.DispatchAsync(
                domainEvent,
                cancellationToken);
        }

        foreach (var entity in ChangeTracker
                     .Entries<IHasDomainEvents>())
        {
            entity.Entity.ClearDomainEvents();
        }

        return result;
    }
}
