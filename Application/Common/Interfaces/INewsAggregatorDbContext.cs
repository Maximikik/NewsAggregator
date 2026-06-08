using Microsoft.EntityFrameworkCore;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Application.Common.Interfaces;

public interface INewsAggregatorDbContext
{
    DbSet<Article> Articles { get; }
    DbSet<Feed> Feeds { get; }
    DbSet<Category> Categories { get; }
    DbSet<Source> Sources { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}
