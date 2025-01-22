using Domain.Pets;

namespace Application.DTOs.Pet;
public record PetVaccinationDto
{
    public string Id { get; init; }
    public Guid PetId { get; set; }
    public DateTime Date { get; set; }
    public string VaccineName { get; set; }
    public string? Description { get; set; }
    public VaccinationFrequency Frequency { get; set; }

    public DateTime UpdatedAt { get; set; }
}
