using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Reaction;
using Domain.Reactions;
using SharedKernel;

namespace Application.Reactions.CreateRange;
internal sealed class CreateRangeReactionCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext) : ICommandHandler<CreateRangeReactionCommand>
{
    public async Task<Result> Handle(CreateRangeReactionCommand command, CancellationToken cancellationToken)
    {
        if (command.TargetList is null || command.TargetList.Count == 0)
        {
            return Result.Success();
        }

        string userId = userContext.UserId;

        var targetIds = command.TargetList.Select(t => t.TargetId).ToHashSet();
        var targetTypes = command.TargetList.Select(t => t.TargetType).ToHashSet();

        var existingReactions = context.Reactions
            .Where(r => r.UserId == userId && targetIds.Contains(r.TargetId) && targetTypes.Contains(r.TargetType))
            .Select(r => new { r.TargetId, r.TargetType })
            .ToHashSet();

        var newReactions = command.TargetList
            .Where(t => !existingReactions.Any(e => e.TargetId == t.TargetId && e.TargetType == t.TargetType))
            .Select(t => new Reaction(t.TargetType, userId, t.TargetId))
            .ToList();

        if (newReactions.Count > 0)
        {
            foreach (Reaction reaction in newReactions)
            {
                reaction.Raise(new ReactionCreatedDomainEvent(reaction));
            }

            await context.Reactions.AddRangeAsync(newReactions, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
