using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Application.DTOs.Pet;
using AutoMapper;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedKernel;

namespace Application.Pets.GetById;
internal sealed class GetPetByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICacheService cacheService)
    : IQueryHandler<GetPetByIdQuery, PetDto>
{
    public async Task<Result<PetDto>> Handle(GetPetByIdQuery query, CancellationToken cancellationToken)
    {
        string cacheKey = $"pet:{query.PetId}";
        string cacheData = await cacheService.GetCacheAsync(cacheKey);

        PetDto result;
        if (!string.IsNullOrEmpty(cacheData))
        {
            result = JsonConvert.DeserializeObject<PetDto>(cacheData);
            if (result is not null)
            {
                return Result.Success(result);
            }
        }

        Pet? pet = await context.Pets
            .FirstOrDefaultAsync(p => p.Id == query.PetId, cancellationToken);

        if (pet is null)
        {
            return Result.Failure<PetDto>(PetErrors.PetNotFound);
        }

        result = mapper.Map<PetDto>(pet);

        await cacheService.SetCacheAsync(cacheKey, result);

        return Result.Success(result);
    }
}
