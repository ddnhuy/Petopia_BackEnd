using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain;
using Domain.Advertisement;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Advertisement.Delete;
internal class DeleteAdCommandHandler(
    IApplicationDbContext context,
    IPublisher publisher) : ICommandHandler<DeleteAdCommand>
{
    public async Task<Result> Handle(DeleteAdCommand command, CancellationToken cancellationToken)
    {
        Ad? ad = await context.Ads.FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken);
        if (ad is null)
        {
            return Result.Failure(AdErrors.AdNotFound);
        }

        context.Ads.Remove(ad);

        if (!string.IsNullOrEmpty(ad.ImagePublicId))
        {
            await publisher.Publish(new DeleteEntityHasImageDomainEvent(ad.ImagePublicId), cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
