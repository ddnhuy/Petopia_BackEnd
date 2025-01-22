namespace Application.DTOs.Pet;
public record PetWeightDto
{
    public string Id { get; init; }
    public Guid PetId { get; set; }
    public double Value { get; set; }

    public DateTime UpdatedAt { get; set; }
}
