using Application.Abstractions.Messaging;
using Application.DTOs;
using Domain.Notifications;
using SharedKernel;

namespace Application.Statics.GetNotificationTypes;
internal sealed class GetNotificationTypesQueryHandler : IQueryHandler<GetNotificationTypesQuery, List<StaticTypeDto>>
{
    public Task<Result<List<StaticTypeDto>>> Handle(GetNotificationTypesQuery query, CancellationToken cancellationToken)
    {
        List<StaticTypeDto> notificationTypeList = [];

        foreach (object notificationType in Enum.GetValues(typeof(NotificationType)))
        {
            notificationTypeList.Add(new StaticTypeDto((int)notificationType, ((Enum)notificationType).ToString(), EnumHelper.GetDisplayName((Enum)notificationType)));
        }

        return Task.FromResult(Result.Success(notificationTypeList));
    }
}
