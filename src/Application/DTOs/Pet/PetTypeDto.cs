namespace Application.DTOs.Pet;
public sealed record PetTypeDto
{
    public int Number { get; }
    public string Name { get; }
    public string DisplayName { get; }

    public PetTypeDto(int number, string name, string displayName)
    {
        Number = number;
        Name = name;
        DisplayName = displayName;
    }
}
