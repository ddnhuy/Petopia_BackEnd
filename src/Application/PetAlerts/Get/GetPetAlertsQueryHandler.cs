using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Application.DTOs.Pet;
using Domain.PetAlerts;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedKernel;

namespace Application.PetAlerts.Get;
internal sealed class GetPetAlertsQueryHandler(
    IApplicationDbContext context,
    ICacheService cacheService) : IQueryHandler<GetPetAlertsQuery, (List<PetAlert>, int, int)>
{
    public async Task<Result<(List<PetAlert>, int, int)>> Handle(GetPetAlertsQuery query, CancellationToken cancellationToken)
    {
        string cacheKey = $"pet-alerts:{query.Page}:{query.PageSize}";
        string cacheData = await cacheService.GetCacheAsync(cacheKey);

        (List<PetAlert> petList, int, int) result;
        if (!string.IsNullOrEmpty(cacheData))
        {
            result = JsonConvert.DeserializeObject<(List<PetAlert>, int, int)>(cacheData);
            if (result.petList is not null)
            {
                return Result.Success(result);
            }
        }

        List<PetAlert> petAlerts = await context.PetAlerts
            .ToListAsync(cancellationToken);

        if (query.Page < 1)
        {
            return Result.Failure<(List<PetAlert>, int, int)>(CommonErrors.PageLessThanOne);
        }

        if (query.PageSize < 1)
        {
            return Result.Failure<(List<PetAlert>, int, int)>(CommonErrors.InvalidPageSize);
        }

        int totalItems = petAlerts.Count;
        if (totalItems == 0)
        {
            return Result.Success((new List<PetAlert>(), 0, 0));
        }

        int totalPages = (int)Math.Ceiling((double)totalItems / query.PageSize);
        if (query.Page > totalPages)
        {
            return Result.Failure<(List<PetAlert>, int, int)>(CommonErrors.GreaterThanTotalPages(query.Page, totalPages));
        }

        result = (petAlerts.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToList(), totalItems, totalPages);

        await cacheService.SetCacheAsync(cacheKey, result);

        return result;
    }
}
