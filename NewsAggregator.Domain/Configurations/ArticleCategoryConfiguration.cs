using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Domain.Configurations;

public sealed class ArticleCategoryConfiguration
    : IEntityTypeConfiguration<ArticleCategory>
{
    public void Configure(
        EntityTypeBuilder<ArticleCategory> builder)
    {
        builder.HasKey(x => new
        {
            x.ArticleId,
            x.CategoryId
        });

        builder.HasOne(x => x.Article)
            .WithMany(x => x.ArticleCategories)
            .HasForeignKey(x => x.ArticleId);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.ArticleCategories)
            .HasForeignKey(x => x.CategoryId);
    }
}
