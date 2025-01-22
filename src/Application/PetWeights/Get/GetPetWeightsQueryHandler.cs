using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Pet;
using AutoMapper;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetWeights.Get;
internal sealed class GetPetWeightsQueryHandler(
    IApplicationDbContext context,
    IMapper mapper) : IQueryHandler<GetPetWeightsQuery, List<PetWeightDto>>
{
    public async Task<Result<List<PetWeightDto>>> Handle(GetPetWeightsQuery query, CancellationToken cancellationToken)
    {
        List<PetWeight> petWeights = await context.PetWeights
            .Where(pw => pw.PetId == query.PetId)
            .ToListAsync(cancellationToken);

        return Result.Success(mapper.Map<List<PetWeightDto>>(petWeights));
    }
}
