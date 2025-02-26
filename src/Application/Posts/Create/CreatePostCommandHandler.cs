using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Posts;
using SharedKernel;

namespace Application.Posts.Create;
internal sealed class CreatePostCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext) : ICommandHandler<CreatePostCommand>
{
    public async Task<Result> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var post = new Post(
            userContext.UserId,
            command.Caption,
            command.HashTag,
            command.ImageUrl,
            command.ImagePublicId);

        context.Posts.Add(post);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
