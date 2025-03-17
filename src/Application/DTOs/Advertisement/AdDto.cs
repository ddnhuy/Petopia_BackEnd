using Domain.Users;

namespace Application.DTOs.Advertisement;
public sealed class AdDto
{
    public Guid Id { get; set; }
    public Uri TargetUrl { get; set; }
    public Uri ImageUrl { get; set; }
}
