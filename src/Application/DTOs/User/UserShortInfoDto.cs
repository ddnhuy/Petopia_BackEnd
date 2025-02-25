namespace Application.DTOs.User;
public sealed record UserShortInfoDto
{
    public string Id { get; init; }

    public string UserName { get; init; }

    public string FullName { get; init; }

    public string? ImageUrl { get; set; }
}
