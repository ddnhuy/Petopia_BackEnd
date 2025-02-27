using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Notifications.Read;

internal sealed class ReadNotificationCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<ReadNotificationCommand>
{
    public async Task<Result> Handle(ReadNotificationCommand command, CancellationToken cancellationToken)
    {
        Notification? notification = await context.Notifications
            .SingleOrDefaultAsync(t => t.Id == command.NotificationId && t.UserId == userContext.UserId, cancellationToken);

        if (notification is null)
        {
            return Result.Failure(NotificationErrors.NotFound);
        }

        if (notification.IsRead)
        {
            return Result.Success();
        }

        notification.MarkAsRead();

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
