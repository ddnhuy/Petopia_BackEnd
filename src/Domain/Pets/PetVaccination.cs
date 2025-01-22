using SharedKernel;

namespace Domain.Pets;
public sealed class PetVaccination : Entity
{
    public Guid PetId { get; private set; }
    public DateTime Date { get; set; }
    public string VaccineName { get; set; }
    public string? Description { get; set; }
    public VaccinationFrequency Frequency { get; set; }

    public PetVaccination(Guid petId, DateTime date, string vaccineName, string? description, VaccinationFrequency frequency)
    {
        PetId = petId;
        Date = date;
        VaccineName = vaccineName;
        Description = description;
        Frequency = frequency;
    }
}
