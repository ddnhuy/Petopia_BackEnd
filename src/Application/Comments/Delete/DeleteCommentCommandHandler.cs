using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Advertisement;
using Domain.Comments;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Comments.Delete;
internal sealed class DeleteCommentCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<DeleteCommentCommand>
{
    public async Task<Result> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        Comment? comment = await context.Comments
            .SingleOrDefaultAsync(p => p.Id == command.CommentId, cancellationToken);

        if (comment is null)
        {
            return Result.Failure(CommentErrors.CommentNotFound);
        }

        if (comment.UserId != userContext.UserId)
        {
            return Result.Failure(CommentErrors.CommentNotHavePermission);
        }

        context.Comments.Remove(comment);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
