using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Application.DTOs.PetAlert;
using AutoMapper;
using Domain.PetAlerts;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedKernel;

namespace Application.PetAlerts.Get;
internal sealed class GetPetAlertsQueryHandler(
    IApplicationDbContext context,
    ICacheService cacheService,
    IMapper mapper) : IQueryHandler<GetPetAlertsQuery, (List<PetAlertDto>, int, int)>
{
    public async Task<Result<(List<PetAlertDto>, int, int)>> Handle(GetPetAlertsQuery query, CancellationToken cancellationToken)
    {
        string cacheKey = $"pet-alerts:{query.Page}:{query.PageSize}";
        string cacheData = await cacheService.GetCacheAsync(cacheKey);

        (List<PetAlertDto> petList, int, int) result;
        if (!string.IsNullOrEmpty(cacheData))
        {
            result = JsonConvert.DeserializeObject<(List<PetAlertDto>, int, int)>(cacheData);
            if (result.petList is not null)
            {
                return Result.Success(result);
            }
        }

        List<PetAlert> petAlerts = await context.PetAlerts
            .Include(pa => pa.Pet)
            .ToListAsync(cancellationToken);

        if (query.Page < 1)
        {
            return Result.Failure<(List<PetAlertDto>, int, int)>(CommonErrors.PageLessThanOne);
        }

        if (query.PageSize < 1)
        {
            return Result.Failure<(List<PetAlertDto>, int, int)>(CommonErrors.InvalidPageSize);
        }

        int totalItems = petAlerts.Count;
        if (totalItems == 0)
        {
            return Result.Success((new List<PetAlertDto>(), 0, 0));
        }

        int totalPages = (int)Math.Ceiling((double)totalItems / query.PageSize);
        if (query.Page > totalPages)
        {
            return Result.Failure<(List<PetAlertDto>, int, int)>(CommonErrors.GreaterThanTotalPages(query.Page, totalPages));
        }

        result = (mapper.Map<List<PetAlertDto>>(petAlerts.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToList()), totalItems, totalPages);

        await cacheService.SetCacheAsync(cacheKey, result);

        return result;
    }
}
