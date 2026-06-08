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

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.BaseUrl)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Ignore(x => x.DomainEvents);

        builder.Navigation(x => x.Articles)
            .UsePropertyAccessMode(
                PropertyAccessMode.Field);
    }
}