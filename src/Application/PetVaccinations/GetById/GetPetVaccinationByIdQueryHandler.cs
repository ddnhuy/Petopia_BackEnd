using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Pet;
using AutoMapper;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetVaccinations.GetById;
internal sealed class GetPetVaccinationByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper) : IQueryHandler<GetPetVaccinationByIdQuery, PetVaccinationDto>
{
    public async Task<Result<PetVaccinationDto>> Handle(GetPetVaccinationByIdQuery query, CancellationToken cancellationToken)
    {
        PetVaccination? petVaccination = await context.PetVaccinations
            .SingleOrDefaultAsync(pv => pv.Id == query.PetVaccinationId, cancellationToken);

        if (petVaccination is null)
        {
            return Result.Failure<PetVaccinationDto>(PetErrors.PetVaccinationNotFound);
        }

        return Result.Success(mapper.Map<PetVaccinationDto>(petVaccination));
    }
}
