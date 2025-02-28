using Domain.Users;

namespace Domain.Notifications;
public sealed class NotificationSubscription
{
    public string UserId { get; set; }
    public string SubscriptionId { get; set; }

    public ApplicationUser User { get; set; }

    public NotificationSubscription(string userId, string subscriptionId)
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }
}
