using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetWeights.GetById;
internal sealed class GetPetWeightByIdQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetPetWeightByIdQuery, PetWeight>
{
    public async Task<Result<PetWeight>> Handle(GetPetWeightByIdQuery query, CancellationToken cancellationToken)
    {
        PetWeight? petWeight = await context.PetWeights
            .SingleOrDefaultAsync(pw => pw.Id == query.PetWeightId, cancellationToken);

        if (petWeight is null)
        {
            return Result.Failure<PetWeight>(PetErrors.PetWeightNotFound);
        }

        return Result.Success(petWeight);
    }
}
