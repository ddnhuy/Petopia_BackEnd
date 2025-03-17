using Application.Abstractions.Messaging;
using Application.DTOs;
using Domain.Advertisement;
using SharedKernel;

namespace Application.Statics.GetAdEventTypes;
internal class GetAdEventTypesQueryHandler : IQueryHandler<GetAdEventTypesQuery, List<StaticTypeDto>>
{
    public Task<Result<List<StaticTypeDto>>> Handle(GetAdEventTypesQuery request, CancellationToken cancellationToken)
    {
        List<StaticTypeDto> adEventTypes = [];

        foreach (object adEventType in Enum.GetValues(typeof(AdEventType)))
        {
            adEventTypes.Add(new StaticTypeDto((int)adEventType, ((Enum)adEventType).ToString(), EnumHelper.GetDisplayName((Enum)adEventType)));
        }

        return Task.FromResult(Result.Success(adEventTypes));
    }
}
