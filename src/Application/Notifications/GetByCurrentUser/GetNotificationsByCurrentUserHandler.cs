using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Notification;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Notifications.GetByCurrentUser;
internal sealed class GetNotificationsByCurrentUserHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IUserContext userContext) : IQueryHandler<GetNotificationsByCurrentUser, List<NotificationDto>>
{
    public async Task<Result<List<NotificationDto>>> Handle(GetNotificationsByCurrentUser query, CancellationToken cancellationToken)
    {
        List<NotificationDto> notifications = await context.Notifications
            .Where(n => n.UserId == userContext.UserId)
            .OrderByDescending(n => n.CreatedAt)
            .ProjectTo<NotificationDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result.Success(notifications);
    }
}
