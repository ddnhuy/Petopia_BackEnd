namespace Application.DTOs;
public sealed record StaticTypeDto
{
    public int Number { get; }
    public string Name { get; }
    public string DisplayName { get; }

    public StaticTypeDto(int number, string name, string displayName)
    {
        Number = number;
        Name = name;
        DisplayName = displayName;
    }
}
