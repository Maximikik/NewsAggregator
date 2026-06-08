using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Infrastructure.Persistence.Configurations;

public sealed class SourceConfiguration
    : IEntityTypeConfiguration<Source>
{
    public void Configure(
        EntityTypeBuilder<Source> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Ignore(x => x.DomainEvents);
        builder.Property(x => x.Name)
            .HasMaxLength(200);

        builder.Property(x => x.BaseUrl)
            .HasMaxLength(500);

        builder.HasMany(x => x.Articles)
            .WithOne(x => x.Source)
            .HasForeignKey(x => x.SourceId);
    }
}