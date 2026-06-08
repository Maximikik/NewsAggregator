using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Domain.Configurations;

public sealed class UserArticleLikeConfiguration
    : IEntityTypeConfiguration<UserArticleLike>
{
    public void Configure(
        EntityTypeBuilder<UserArticleLike> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Ignore(x => x.DomainEvents);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.ArticleId
        })
        .IsUnique();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Article)
            .WithMany()
            .HasForeignKey(x => x.ArticleId);
    }
}
