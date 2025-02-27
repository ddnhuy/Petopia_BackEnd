using Application.Abstractions.Messaging;
using Application.DTOs.Notification;

namespace Application.Notifications.GetByCurrentUser;
public sealed record GetNotificationsByCurrentUser : IQuery<List<NotificationDto>>;
