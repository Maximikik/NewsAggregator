using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Infrastructure.Persistence.Configurations;

public sealed class ArticleConfiguration
    : IEntityTypeConfiguration<Article>
{
    public void Configure(
        EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Ignore(x => x.DomainEvents);
        builder.Property(x => x.Title)
            .HasMaxLength(500);

        builder.Property(x => x.Description)
            .HasMaxLength(5000);
    }
}