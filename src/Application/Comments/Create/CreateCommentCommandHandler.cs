using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Advertisement;
using Domain.Comments;
using Domain.Posts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Comments.Create;
internal sealed class CreateCommentCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext) : ICommandHandler<CreateCommentCommand>
{
    public async Task<Result> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        Post? post = await context.Posts.FirstOrDefaultAsync(p => p.Id == command.PostId, cancellationToken);

        if (post is null)
        {
            return Result.Failure(CommentErrors.PostNotFound);
        }

        var comment = new Comment(userContext.UserId, command.PostId, command.Content);

        context.Comments.Add(comment);

        comment.Raise(new CommentCreatedDomainEvent(comment));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
