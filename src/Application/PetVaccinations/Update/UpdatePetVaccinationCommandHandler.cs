using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetVaccinations.Update;
internal sealed class UpdatePetVaccinationCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext) : ICommandHandler<UpdatePetVaccinationCommand>
{
    public async Task<Result> Handle(UpdatePetVaccinationCommand command, CancellationToken cancellationToken)
    {
        PetVaccination? petVaccination = await context.PetVaccinations
            .SingleOrDefaultAsync(x => x.Id == command.PetVaccinationId, cancellationToken);

        if (petVaccination is null)
        {
            return Result.Failure(PetErrors.PetWeightNotFound);
        }

        Pet? pet = await context.Pets
            .FirstOrDefaultAsync(p => p.Id == petVaccination.PetId, cancellationToken);

        if (pet is null)
        {
            return Result.Failure(PetErrors.PetNotFound);
        }

        if (pet.OwnerId != userContext.UserId)
        {
            return Result.Failure(UserErrors.Unauthorized);
        }

        petVaccination.Date = command.Date;
        petVaccination.VaccineName = command.VaccineName;
        petVaccination.Description = command.Description;
        petVaccination.Frequency = command.Frequency;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
