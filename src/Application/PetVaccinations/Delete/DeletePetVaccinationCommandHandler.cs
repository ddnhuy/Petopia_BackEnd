using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetVaccinations.Delete;
internal sealed class DeletePetVaccinationCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext) : ICommandHandler<DeletePetVaccinationCommand>
{
    public async Task<Result> Handle(DeletePetVaccinationCommand command, CancellationToken cancellationToken)
    {
        PetVaccination? petVaccination = await context.PetVaccinations
            .SingleOrDefaultAsync(p => p.Id == command.PetVaccinationId, cancellationToken);

        if (petVaccination is null)
        {
            return Result.Failure(PetErrors.PetVaccinationNotFound);
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

        context.PetVaccinations.Remove(petVaccination);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
