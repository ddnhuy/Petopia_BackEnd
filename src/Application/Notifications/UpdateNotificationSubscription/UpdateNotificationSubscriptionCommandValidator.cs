using FluentValidation;

namespace Application.Notifications.UpdateNotificationSubscription;

internal sealed class UpdateNotificationSubscriptionCommandValidator : AbstractValidator<UpdateNotificationSubscriptionCommand>
{
    public UpdateNotificationSubscriptionCommandValidator()
    {
        RuleFor(x => x.SubscriptionId).NotEmpty();
    }
}
