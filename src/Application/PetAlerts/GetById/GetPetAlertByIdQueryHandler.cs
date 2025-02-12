using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.PetAlerts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetAlerts.GetById;
internal class GetPetAlertByIdQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetPetAlertByIdQuery, PetAlert>
{
    public async Task<Result<PetAlert>> Handle(GetPetAlertByIdQuery query, CancellationToken cancellationToken)
    {
        PetAlert petAlert = await context.PetAlerts
            .Include(p => p.Pet)
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (petAlert is null)
        {
            return Result.Failure<PetAlert>(PetAlertErrors.PetNotFound);
        }

        return Result.Success(petAlert);
    }
}
