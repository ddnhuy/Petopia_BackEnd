using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Pets;
internal sealed class PetWeightConfiguration : IEntityTypeConfiguration<PetWeight>
{
    public void Configure(EntityTypeBuilder<PetWeight> builder)
    {
        builder.HasKey(w => w.Id);

        builder.HasOne<Pet>()
            .WithMany(p => p.Weights)
            .HasForeignKey(w => w.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
