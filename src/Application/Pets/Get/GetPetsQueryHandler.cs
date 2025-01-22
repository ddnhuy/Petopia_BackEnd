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
    : IQueryHandler<GetPetsQuery, List<PetDto>>
{
    public async Task<Result<List<PetDto>>> Handle(GetPetsQuery query, CancellationToken cancellationToken)
    {
        List<Pet> pets = await context.Pets
            .Where(pet => pet.OwnerId == query.UserId)
            .ToListAsync(cancellationToken);

        return Result.Success(mapper.Map<List<PetDto>>(pets));
    }
}
