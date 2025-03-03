using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Application.DTOs;
using Application.DTOs.Pet;
using Application.DTOs.Post;
using AutoMapper;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedKernel;

namespace Application.Pets.Get;
internal sealed class GetPetsQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICacheService cacheService)
    : IQueryHandler<GetPetsQuery, PaginatedByOffsetListDto<PetDto>>
{
    public async Task<Result<PaginatedByOffsetListDto<PetDto>>> Handle(GetPetsQuery query, CancellationToken cancellationToken)
    {
        if (query.Page < 1 || query.PageSize < 1)
        {
            return Result.Failure<PaginatedByOffsetListDto<PetDto>>(CommonErrors.InvalidPaginationParameters(query.Page, query.PageSize));
        }

        string cacheKey = $"pets:{query.UserId}:{query.Page}:{query.PageSize}";
        string cacheData = await cacheService.GetCacheAsync(cacheKey);

        PaginatedByOffsetListDto<PetDto> result;
        if (!string.IsNullOrEmpty(cacheData))
        {
            result = JsonConvert.DeserializeObject<PaginatedByOffsetListDto<PetDto>>(cacheData);
            if (result is not null && result.Items is not null)
            {
                return Result.Success(result);
            }
        }

        List<Pet> pets = await context.Pets
            .Where(pet => pet.OwnerId == query.UserId)
            .ToListAsync(cancellationToken);

        if (query.Page < 1)
        {
            return Result.Failure<PaginatedByOffsetListDto<PetDto>>(CommonErrors.PageLessThanOne);
        }

        if (query.PageSize < 1)
        {
            return Result.Failure<PaginatedByOffsetListDto<PetDto>>(CommonErrors.InvalidPageSize);
        }

        int totalItems = pets.Count;
        if (totalItems == 0)
        {
            return Result.Success(new PaginatedByOffsetListDto<PetDto>());
        }

        int totalPages = (int)Math.Ceiling((double)totalItems / query.PageSize);
        if (query.Page > totalPages)
        {
            return Result.Failure<PaginatedByOffsetListDto<PetDto>>(CommonErrors.GreaterThanTotalPages(query.Page, totalPages));
        }

        result = new PaginatedByOffsetListDto<PetDto>(mapper.Map<List<PetDto>>(pets.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)), totalItems, query.Page, query.PageSize);

        await cacheService.SetCacheAsync(cacheKey, result);

        return Result.Success(result);
    }
}
