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

        return Result.Success(mapper.Map<IEnumerable<AdStatisticsDto>>(ads));
    }
}
