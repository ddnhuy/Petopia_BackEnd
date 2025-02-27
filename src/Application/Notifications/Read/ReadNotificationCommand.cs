using Application.Abstractions.Messaging;

namespace Application.Notifications.Read;

public sealed record ReadNotificationCommand(Guid NotificationId) : ICommand;
