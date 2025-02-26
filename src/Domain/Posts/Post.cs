using Domain.Comments;
using Domain.Users;
using SharedKernel;

namespace Domain.Posts;
public sealed class Post : Entity
{
    public string UserId { get; set; }
    public string Caption { get; set; }
    public string HashTag { get; set; }

    public Uri? ImageUrl { get; set; }
    public string? ImagePublicId { get; set; }

    public ApplicationUser User { get; set; }
    public ICollection<Comment> Comments { get; set; } = [];

    public Post(string userId, string caption, string hashTag, Uri imageUrl, string imagePublicId)
    {
        UserId = userId;
        Caption = caption;
        HashTag = hashTag;
        ImageUrl = imageUrl;
        ImagePublicId = imagePublicId;
    }
}
