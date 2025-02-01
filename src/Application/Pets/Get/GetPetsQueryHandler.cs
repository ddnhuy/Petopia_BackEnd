using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Pet;
using AutoMapper;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Pets.Get;
internal sealed class GetPetsQueryHandler(
    IApplicationDbContext context,
    IMapper mapper)
    : IQueryHandler<GetPetsQuery, (List<PetDto>, int, int)>
{
    public async Task<Result<(List<PetDto>, int, int)>> Handle(GetPetsQuery query, CancellationToken cancellationToken)
    {
        List<Pet> pets = await context.Pets
            .Where(pet => pet.OwnerId == query.UserId)
            .ToListAsync(cancellationToken);

        if (query.Page < 1)
        {
            return Result.Failure<(List<PetDto>, int, int)>(CommonErrors.PageLessThanOne);
        }

        if (query.PageSize < 1)
        {
            return Result.Failure<(List<PetDto>, int, int)>(CommonErrors.InvalidPageSize);
        }

        int totalItems = pets.Count;
        if (totalItems == 0)
        {
            return Result.Success((new List<PetDto>(), 0, 0));
        }

        int totalPages = (int)Math.Ceiling((double)totalItems / query.PageSize);
        if (query.Page > totalPages)
        {
            return Result.Failure<(List<PetDto>, int, int)>(CommonErrors.GreaterThanTotalPages(query.Page, totalPages));
        }

        return Result.Success((mapper.Map<List<PetDto>>(
            pets.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)).ToList(),
            totalPages,
            totalItems));
    }
}
