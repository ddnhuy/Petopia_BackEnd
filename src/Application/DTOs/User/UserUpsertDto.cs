namespace Application.DTOs.User;
public sealed record UserUpsertDto
{
    public string Id { get; init; }

    public string UserName { get; init; }

    public string Email { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string? ImageUrl { get; set; }
    public string? ImagePublicId { get; set; }
}
