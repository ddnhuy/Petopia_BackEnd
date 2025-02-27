using System.ComponentModel.DataAnnotations;

namespace Domain.Notifications;
public enum NotificationType
{
    [Display(Name = "Bài đăng mới")]
    NewPost,
    [Display(Name = "Bình luận mới")]
    NewComment,
    [Display(Name = "Trả lời mới")]
    NewReply,
    [Display(Name = "Người theo dõi mới")]
    NewFollower,
    [Display(Name = "Lượt bày tỏ cảm xúc mới")]
    NewReaction,
    [Display(Name = "Được tag mới")]
    NewMention,
    [Display(Name = "Tin nhắn mới")]
    NewMessage
}
