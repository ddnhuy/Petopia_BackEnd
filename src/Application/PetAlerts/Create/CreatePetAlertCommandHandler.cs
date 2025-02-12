using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.PetAlerts;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetAlerts.Create;
internal sealed class CreatePetAlertCommandHandler(
    IApplicationDbContext context) : ICommandHandler<CreatePetAlertCommand>
{
    public async Task<Result> Handle(CreatePetAlertCommand command, CancellationToken cancellationToken)
    {
        Pet pet = await context.Pets.FirstOrDefaultAsync(p => p.Id == command.PetId, cancellationToken);

        if (pet is null)
        {
            return Result.Failure(PetErrors.PetNotFound);
        }

        PetAlert petAlert = new()
        {
            PetId = command.PetId,
            PhoneNumber = command.PhoneNumber,
            Note = command.Note,
            LastSeen = command.LastSeen,
            Address = command.Address,
            Pet = pet
        };

        await context.PetAlerts.AddAsync(petAlert, cancellationToken);

        petAlert.Raise(new PetAlertCreatedDomainEvent(petAlert));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
