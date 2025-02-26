using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comments;
using SharedKernel;

namespace Application.Comments.Create;
internal sealed class CreateCommentCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext) : ICommandHandler<CreateCommentCommand>
{
    public async Task<Result> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = new Comment(userContext.UserId, command.PostId, command.Content);

        context.Comments.Add(comment);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
