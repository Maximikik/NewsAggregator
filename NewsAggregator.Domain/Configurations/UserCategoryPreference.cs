using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Domain.Configurations;

public sealed class UserCategoryPreferenceConfiguration
 : IEntityTypeConfiguration<UserCategoryPreference>
{
    public void Configure(
        EntityTypeBuilder<UserCategoryPreference> builder)
    {
        builder.HasKey(x => new
        {
            x.UserId,
            x.CategoryId
        });

        builder.Property(x => x.Weight)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Preferences)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Preferences)
            .HasForeignKey(x => x.CategoryId);
    }
}
