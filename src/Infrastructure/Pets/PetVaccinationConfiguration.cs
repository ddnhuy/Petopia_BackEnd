using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Pets;
internal sealed class PetVaccinationConfiguration : IEntityTypeConfiguration<PetVaccination>
{
    public void Configure(EntityTypeBuilder<PetVaccination> builder)
    {
        builder.HasKey(v => v.Id);

        builder.HasOne<Pet>()
            .WithMany(v => v.Vaccinations)
            .HasForeignKey(v => v.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
