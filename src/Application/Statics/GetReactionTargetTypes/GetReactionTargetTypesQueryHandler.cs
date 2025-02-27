using Application.Abstractions.Messaging;
using Application.DTOs;
using Domain.Reactions;
using SharedKernel;

namespace Application.Statics.GetReactionTargetTypes;
internal sealed class GetReactionTargetTypesQueryHandler : IQueryHandler<GetReactionTargetTypesQuery, List<StaticTypeDto>>
{
    public Task<Result<List<StaticTypeDto>>> Handle(GetReactionTargetTypesQuery query, CancellationToken cancellationToken)
    {
        List<StaticTypeDto> reactionTargetTypeList = [];

        foreach (object reactionTargetType in Enum.GetValues(typeof(ReactionTargetType)))
        {
            reactionTargetTypeList.Add(new StaticTypeDto((int)reactionTargetType, ((Enum)reactionTargetType).ToString(), EnumHelper.GetDisplayName((Enum)reactionTargetType)));
        }

        return Task.FromResult(Result.Success(reactionTargetTypeList));
    }
}
