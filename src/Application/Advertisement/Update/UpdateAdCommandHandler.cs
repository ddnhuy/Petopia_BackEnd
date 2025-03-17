using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Advertisement;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Advertisement.Update;
internal sealed class UpdateAdCommandHandler(
    IApplicationDbContext context) : ICommandHandler<UpdateAdCommand>
{
    public async Task<Result> Handle(UpdateAdCommand command, CancellationToken cancellationToken)
    {
        Ad ad = await context.Ads
            .FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken);

        if (ad is null)
        {
            return Result.Failure(AdErrors.AdNotFound);
        }

        ad.Title = command.Title;
        ad.TargetUrl = command.TargetUrl;
        ad.TotalCost = command.TotalCost;
        ad.IsActive = command.IsActive;
        ad.ImageUrl = command.ImageUrl;
        ad.ImagePublicId = command.ImagePublicId;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
