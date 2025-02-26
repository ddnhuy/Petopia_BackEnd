using Application.DTOs.User;

namespace Application.DTOs.Comment;
public sealed record CommentDto
{
    public Guid Id { get; init; }
    public string Content { get; init; }
    public DateTime UpdatedAt { get; init; }
    public UserShortInfoDto User { get; init; }
}
