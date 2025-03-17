using Domain.Advertisement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Advertisement;
internal class AdEventConfiguration : IEntityTypeConfiguration<AdEvent>
{
    public void Configure(EntityTypeBuilder<AdEvent> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasOne(x => x.Ad)
            .WithMany()
            .HasForeignKey(x => x.AdId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
