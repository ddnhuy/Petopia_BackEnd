using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comments;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Comments.Update;
internal sealed class UpdateCommentCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext) : ICommandHandler<UpdateCommentCommand>
{
    public async Task<Result> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        Comment? comment = await context.Comments
            .SingleOrDefaultAsync(c => c.Id == command.Id && c.PostId == command.PostId, cancellationToken);

        if (comment is null)
        {
            return Result.Failure(CommentErrors.CommentNotFound);
        }

        if (comment.UserId != userContext.UserId)
        {
            return Result.Failure(CommentErrors.CommentNotHavePermission);
        }

        comment.Content = command.Content;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
