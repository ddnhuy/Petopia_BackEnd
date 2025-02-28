using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Notifications.UpdateNotificationSubscription;

internal sealed class UpdateNotificationSubscriptionCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<UpdateNotificationSubscriptionCommand>
{
    public async Task<Result> Handle(UpdateNotificationSubscriptionCommand command, CancellationToken cancellationToken)
    {
        NotificationSubscription? notificationSubscription = await context.NotificationSubscriptions
            .SingleOrDefaultAsync(x => x.SubscriptionId == command.SubscriptionId && x.UserId == userContext.UserId, cancellationToken);

        if (notificationSubscription is null)
        {
            context.NotificationSubscriptions.Add(new NotificationSubscription(userContext.UserId, command.SubscriptionId));
        }
        else
        {
            context.NotificationSubscriptions.Update(notificationSubscription);
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
