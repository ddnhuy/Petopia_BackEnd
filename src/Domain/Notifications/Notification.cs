using Domain.Users;
using SharedKernel;

namespace Domain.Notifications;
public sealed class Notification : Entity
{
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; private set; }

    public Guid TargetId { get; set; }

    public NotificationType Type { get; set; }

    public ApplicationUser User { get; set; }

    public Notification()
    {
    }

    public Notification(string userId, string title, string message, Guid targetId, NotificationType notificationType)
    {
        UserId = userId;
        Title = title;
        Message = message;
        TargetId = targetId;
        Type = notificationType;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}
