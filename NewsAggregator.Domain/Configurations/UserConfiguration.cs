using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Domain.Entities;

namespace NewsAggregator.Domain.Configurations;

public sealed class UserConfiguration
   : IEntityTypeConfiguration<User>
{
    public void Configure(
        EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(320);

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Navigation(x => x.Likes)
            .UsePropertyAccessMode(
                PropertyAccessMode.Field);

        builder.Navigation(x => x.Preferences)
            .UsePropertyAccessMode(
                PropertyAccessMode.Field);
    }
}
