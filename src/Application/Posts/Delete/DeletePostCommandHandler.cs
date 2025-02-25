using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain;
using Domain.Pets;
using Domain.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Posts.Delete;
internal sealed class DeletePostCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext,
    IPublisher publisher)
    : ICommandHandler<DeletePostCommand>
{
    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        Post? post = await context.Posts
            .SingleOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post is null)
        {
            return Result.Failure(PetErrors.PetNotFound);
        }

        if (post.UserId != userContext.UserId)
        {
            return Result.Failure(PetErrors.PetNotOwned);
        }
        context.Posts.Remove(post);

        if (!string.IsNullOrEmpty(post.ImagePublicId))
        {
            await publisher.Publish(new DeleteEntityHasImageDomainEvent(post.ImagePublicId), cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
