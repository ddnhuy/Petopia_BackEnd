using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetVaccinations.Get;
internal sealed class GetPetVaccinationsQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetPetVaccinationsQuery, List<PetVaccination>>
{
    public async Task<Result<List<PetVaccination>>> Handle(GetPetVaccinationsQuery query, CancellationToken cancellationToken)
    {
        List<PetVaccination> petVaccinations = await context.PetVaccinations
            .Where(pv => pv.PetId == query.PetId)
            .ToListAsync(cancellationToken);

        return Result.Success(petVaccinations);
    }
}
