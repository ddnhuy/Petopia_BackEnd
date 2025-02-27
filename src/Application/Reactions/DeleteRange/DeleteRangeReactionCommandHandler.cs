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

        var targetIds = command.TargetList.Select(t => t.TargetId).ToHashSet();
        var targetTypes = command.TargetList.Select(t => t.TargetType).ToHashSet();

        List<Reaction> reactionsToDelete = await context.Reactions
            .Where(r => r.UserId == userId && targetIds.Contains(r.TargetId) && targetTypes.Contains(r.TargetType))
            .ToListAsync(cancellationToken);

        if (reactionsToDelete.Count > 0)
        {
            context.Reactions.RemoveRange(reactionsToDelete);
            await context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
