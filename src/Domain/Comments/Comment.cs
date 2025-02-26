using Domain.Posts;
using Domain.Users;
using SharedKernel;

namespace Domain.Comments;
public sealed class Comment : Entity
{
    public string UserId { get; set; }
    public Guid PostId { get; set; }
    public string Content { get; set; }

    public ApplicationUser User { get; set; }
    public Post Post { get; set; }

    public Comment(string userId, Guid postId, string content)
    {
        UserId = userId;
        PostId = postId;
        Content = content;
    }
}
