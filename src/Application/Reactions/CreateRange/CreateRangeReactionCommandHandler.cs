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

        var reactionList = new List<Reaction>();
        foreach (ReactionDto target in command.TargetList)
        {
            reactionList.Add(new Reaction(target.TargetType, userId, target.TargetId));
        }

        await context.Reactions.AddRangeAsync(reactionList, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
