using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.DTOs.PetAlert;
using AutoMapper;
using Domain.PetAlerts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetAlerts.GetById;
internal class GetPetAlertByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper) : IQueryHandler<GetPetAlertByIdQuery, PetAlertDto>
{
    public async Task<Result<PetAlertDto>> Handle(GetPetAlertByIdQuery query, CancellationToken cancellationToken)
    {
        PetAlert petAlert = await context.PetAlerts
            .Include(pa => pa.Pet)
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (petAlert is null)
        {
            return Result.Failure<PetAlertDto>(PetAlertErrors.PetNotFound);
        }

        return Result.Success(mapper.Map<PetAlertDto>(petAlert));
    }
}
