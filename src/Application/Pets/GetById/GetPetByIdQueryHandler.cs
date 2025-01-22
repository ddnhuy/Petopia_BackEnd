using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Pet;
using AutoMapper;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Pets.GetById;
internal sealed class GetPetByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper)
    : IQueryHandler<GetPetByIdQuery, PetDto>
{
    public async Task<Result<PetDto>> Handle(GetPetByIdQuery query, CancellationToken cancellationToken)
    {
        Pet pet = await context.Pets
            .FirstOrDefaultAsync(p => p.Id == query.PetId, cancellationToken);

        if (pet is null)
        {
            return Result.Failure<PetDto>(PetErrors.PetNotFound);
        }

        return Result.Success(mapper.Map<PetDto>(pet));
    }
}
