using Domain.Pets;
using SharedKernel;

namespace Application.DTOs.Pet;
public record PetDto
{
    public string Id { get; init; }
    public PetType Type { get; set; }
    public string Name { get; set; }
    public Uri? ImageUrl { get; set; }
    public string Description { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public Gender Gender { get; set; }
    public bool IsSterilized { get; set; }
}
