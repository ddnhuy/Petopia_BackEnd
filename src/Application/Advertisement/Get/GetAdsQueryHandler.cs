using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Advertisement;
using AutoMapper;
using Domain.Advertisement;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Advertisement.Get;
internal class GetAdsQueryHandler(
    IApplicationDbContext context,
    IMapper mapper) : IQueryHandler<GetAdsQuery, IEnumerable<AdDto>>
{
    public async Task<Result<IEnumerable<AdDto>>> Handle(GetAdsQuery query, CancellationToken cancellationToken)
    {
        List<Ad> ads = query.IsActiveOnly == true
            ? await context.Ads
                        .Where(a => a.IsActive)
                        .OrderByDescending(a => a.CreatedAt)
                        .ToListAsync(cancellationToken)
            : await context.Ads
                        .OrderByDescending(a => a.CreatedAt)
                        .ToListAsync(cancellationToken);

        return Result.Success(mapper.Map<IEnumerable<AdDto>>(ads));
    }
}
