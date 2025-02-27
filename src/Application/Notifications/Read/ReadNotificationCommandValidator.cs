using FluentValidation;

namespace Application.Notifications.Read;

internal sealed class ReadNotificationCommandValidator : AbstractValidator<ReadNotificationCommand>
{
    public ReadNotificationCommandValidator()
    {
        RuleFor(n => n.NotificationId).NotEmpty();
    }
}
