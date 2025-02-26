using Application.DTOs.User;

namespace Application.DTOs.Post;
public sealed class PostDto
{
    public Guid Id { get; set; }
    public string Caption { get; set; }
    public string HashTag { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Uri? ImageUrl { get; set; }
    public string? ImagePublicId { get; set; }

    public UserShortInfoDto User { get; set; }

    public bool IsLiked { get; set; }
    public int TotalReactions { get; set; }
    public int TotalComments { get; set; }
}
