using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Advertisement;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Advertisement.RecordEvent;
internal sealed class RecordEventCommandHandler(
    IApplicationDbContext context) : ICommandHandler<RecordEventCommand>
{
    public async Task<Result> Handle(RecordEventCommand command, CancellationToken cancellationToken)
    {
        Ad? ad = await context.Ads.FirstOrDefaultAsync(a => a.Id == command.AdId, cancellationToken);
        if (ad is null)
        {
            return Result.Failure(AdErrors.AdNotFound);
        }

        var adEvent = new AdEvent(command.AdId, command.EventType);
        context.AdEvents.Add(adEvent);

        switch (command.EventType)
        {
            case AdEventType.Impression:
                ad.TotalImpressions++;
                break;
            case AdEventType.Click:
                ad.TotalClicks++;
                break;
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
