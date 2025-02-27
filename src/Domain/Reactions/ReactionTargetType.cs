using System.ComponentModel.DataAnnotations;

namespace Domain.Reactions;
public enum ReactionTargetType
{
    [Display(Name = "Bài đăng")]
    Post,
    [Display(Name = "Bình luận")]
    Comment
}
