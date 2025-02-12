using Domain.PetAlerts;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.PetAlerts;
internal sealed class PetAlertConfiguration : IEntityTypeConfiguration<PetAlert>
{
    public void Configure(EntityTypeBuilder<PetAlert> builder)
    {
        builder.HasKey(pa => pa.Id);

        builder.HasOne(pa => pa.Pet)
            .WithMany()
            .HasForeignKey(pa => pa.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
