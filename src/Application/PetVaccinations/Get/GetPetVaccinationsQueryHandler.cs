using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.Pet;
using AutoMapper;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetVaccinations.Get;
internal sealed class GetPetVaccinationsQueryHandler(
    IApplicationDbContext context,
    IMapper mapper) : IQueryHandler<GetPetVaccinationsQuery, List<PetVaccinationDto>>
{
    public async Task<Result<List<PetVaccinationDto>>> Handle(GetPetVaccinationsQuery query, CancellationToken cancellationToken)
    {
        List<PetVaccination> petVaccinations = await context.PetVaccinations
            .Where(pv => pv.PetId == query.PetId)
            .ToListAsync(cancellationToken);

        return Result.Success(mapper.Map<List<PetVaccinationDto>>(petVaccinations));
    }
}
