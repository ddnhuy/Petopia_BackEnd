using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Advertisement;
using AutoMapper;
using Domain.Advertisement;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Advertisement.GetStatistics;
internal class GetAdStatisticsQueryHandler(
    IApplicationDbContext context,
    IMapper mapper) : IQueryHandler<GetAdStatisticsQuery, IEnumerable<AdStatisticsDto>>
{
    public async Task<Result<IEnumerable<AdStatisticsDto>>> Handle(GetAdStatisticsQuery query, CancellationToken cancellationToken)
    {
        List<Ad> ads = query.IsActiveOnly == true
            ? await context.Ads
                        .Where(a => a.IsActive)
                        .OrderByDescending(a => a.CreatedAt)
                        .ToListAsync(cancellationToken)
            : await context.Ads
                        .OrderByDescending(a => a.CreatedAt)
                        .ToListAsync(cancellationToken);

        IEnumerable<AdStatisticsDto> result = mapper.Map<List<AdStatisticsDto>>(ads);

        // get the total impressions and clicks for each ad by date
        foreach (AdStatisticsDto ad in result)
        {
            ad.TotalImpressionsByDate = await context.AdEvents
                .Where(s => s.AdId == ad.Id && s.EventType == AdEventType.Impression)
                .GroupBy(s => s.CreatedAt.Date)
                .Select(g => new AdStatisticByDateDto
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .ToListAsync(cancellationToken);

            ad.TotalClicksByDate = await context.AdEvents
                .Where(s => s.AdId == ad.Id && s.EventType == AdEventType.Click)
                .GroupBy(s => s.CreatedAt.Date)
                .Select(g => new AdStatisticByDateDto
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .ToListAsync(cancellationToken);
        }

        return Result.Success(result);
    }
}
