using SharedKernel;

namespace Domain.Pets;
public sealed class Pet : Entity
{
    public string OwnerId { get; set; }
    public PetType Type { get; set; }
    public string Name { get; set; }
    public Uri? ImageUrl { get; set; }
    public string Description { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public Gender Gender { get; set; }
    public bool IsSterilized { get; set; }

    public string? ImagePublicId { get; set; }

    public ICollection<PetWeight> Weights { get; set; } = [];
    public ICollection<PetVaccination> Vaccinations { get; set; } = [];

    public Pet() { }

    public Pet(
        string ownerId,
        PetType type,
        string name,
        Uri? imageUrl,
        string description,
        DateTime birthDate,
        DateTime? deathDate,
        Gender gender,
        bool isSterilized,
        string? imagePublicId)
    {
        OwnerId = ownerId;
        Type = type;
        Name = name;
        ImageUrl = imageUrl;
        Description = description;
        BirthDate = birthDate;
        DeathDate = deathDate;
        Gender = gender;
        IsSterilized = isSterilized;
        ImagePublicId = imagePublicId;
    }
}
