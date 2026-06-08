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

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.PublishedAt)
            .IsRequired();

        builder.Ignore(x => x.DomainEvents);

        builder.HasOne(x => x.Source)
            .WithMany(x => x.Articles)
            .HasForeignKey(x => x.SourceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.ArticleCategories)
            .UsePropertyAccessMode(
                PropertyAccessMode.Field);
    }
}