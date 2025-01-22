using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetWeights.Get;
internal sealed class GetPetWeightsQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetPetWeightsQuery, List<PetWeight>>
{
    public async Task<Result<List<PetWeight>>> Handle(GetPetWeightsQuery query, CancellationToken cancellationToken)
    {
        List<PetWeight> petWeights = await context.PetWeights
            .Where(pw => pw.PetId == query.PetId)
            .ToListAsync(cancellationToken);

        return Result.Success(petWeights);
    }
}
