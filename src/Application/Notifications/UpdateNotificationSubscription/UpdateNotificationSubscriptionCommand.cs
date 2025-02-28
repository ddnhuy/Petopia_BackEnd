using Application.Abstractions.Messaging;

namespace Application.Notifications.UpdateNotificationSubscription;

public sealed record UpdateNotificationSubscriptionCommand(string SubscriptionId) : ICommand;
