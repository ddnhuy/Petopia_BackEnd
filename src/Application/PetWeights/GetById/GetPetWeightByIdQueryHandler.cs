using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Pet;
using AutoMapper;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetWeights.GetById;
internal sealed class GetPetWeightByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper) : IQueryHandler<GetPetWeightByIdQuery, PetWeightDto>
{
    public async Task<Result<PetWeightDto>> Handle(GetPetWeightByIdQuery query, CancellationToken cancellationToken)
    {
        PetWeight? petWeight = await context.PetWeights
            .SingleOrDefaultAsync(pw => pw.Id == query.PetWeightId, cancellationToken);

        if (petWeight is null)
        {
            return Result.Failure<PetWeightDto>(PetErrors.PetWeightNotFound);
        }

        return Result.Success(mapper.Map<PetWeightDto>(petWeight));
    }
}
