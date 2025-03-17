using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Advertisement;
using SharedKernel;

namespace Application.Advertisement.Create;
internal sealed class CreateAdCommandHandler(
    IApplicationDbContext context) : ICommandHandler<CreateAdCommand>
{
    public async Task<Result> Handle(CreateAdCommand command, CancellationToken cancellationToken)
    {
        var ad = new Ad(command.Title, command.TargetUrl, command.TotalCost, command.ImageUrl, command.ImagePublicId);

        context.Ads.Add(ad);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
