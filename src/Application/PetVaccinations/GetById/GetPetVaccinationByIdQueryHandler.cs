using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetVaccinations.GetById;
internal sealed class GetPetVaccinationByIdQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetPetVaccinationByIdQuery, PetVaccination>
{
    public async Task<Result<PetVaccination>> Handle(GetPetVaccinationByIdQuery query, CancellationToken cancellationToken)
    {
        PetVaccination? petVaccination = await context.PetVaccinations
            .SingleOrDefaultAsync(pv => pv.Id == query.PetVaccinationId, cancellationToken);

        if (petVaccination is null)
        {
            return Result.Failure<PetVaccination>(PetErrors.PetVaccinationNotFound);
        }

        return Result.Success(petVaccination);
    }
}
