using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Posts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Posts.Update;
internal sealed class UpdatePostCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext) : ICommandHandler<UpdatePostCommand>
{
    public async Task<Result> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
    {
        Post? post = await context.Posts
            .SingleOrDefaultAsync(p => p.Id == command.Id && p.UserId == userContext.UserId, cancellationToken);

        if (post is null)
        {
            return Result.Failure(PostErrors.PostNotFound);
        }

        post.Caption = command.Caption;
        post.HashTag = command.HashTag;
        post.ImageUrl = command.ImageUrl;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
