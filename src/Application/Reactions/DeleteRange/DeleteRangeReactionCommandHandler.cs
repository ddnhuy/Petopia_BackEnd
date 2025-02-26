using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reactions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reactions.DeleteRange;
internal sealed class DeleteRangeReactionCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<DeleteRangeReactionCommand>
{
    public async Task<Result> Handle(DeleteRangeReactionCommand command, CancellationToken cancellationToken)
    {
        if (command.TargetList is null || command.TargetList.Count == 0)
        {
            return Result.Success();
        }

        string userId = userContext.UserId;

        var targetSet = command.TargetList.Select(t => new { t.TargetId, t.TargetType }).ToHashSet();

        List<Reaction> reactionList = await context.Reactions
            .Where(r => r.UserId == userId && targetSet.Contains(new { r.TargetId, r.TargetType }))
            .ToListAsync(cancellationToken);

        if (reactionList.Any())
        {
            context.Reactions.RemoveRange(reactionList);
            await context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
