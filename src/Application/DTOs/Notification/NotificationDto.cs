using Domain.Notifications;

namespace Application.DTOs.Notification;
public sealed class NotificationDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid TargetId { get; set; }

    public NotificationType Type { get; set; }
}
