using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Users;
internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Token).HasMaxLength(200);

        builder.HasIndex(rt => rt.Token).IsUnique();

        builder.HasOne(rt => rt.User).WithMany().HasForeignKey(rt => rt.UserId);
    }
}
