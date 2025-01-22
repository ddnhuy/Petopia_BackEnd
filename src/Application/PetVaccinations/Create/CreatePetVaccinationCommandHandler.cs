using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetVaccinations.Create;
internal sealed class CreatePetVaccinationCommandHandler(
    IApplicationDbContext context) : ICommandHandler<CreatePetVaccinationCommand>
{
    public async Task<Result> Handle(CreatePetVaccinationCommand command, CancellationToken cancellationToken)
    {
        Pet? pet = await context.Pets
            .FirstOrDefaultAsync(p => p.Id == command.PetId, cancellationToken);

        if (pet is null)
        {
            return Result.Failure(PetErrors.PetNotFound);
        }

        PetVaccination petVaccination = new(command.PetId, command.Date, command.VaccineName, command.Description, command.Frequency);

        await context.PetVaccinations.AddAsync(petVaccination, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
