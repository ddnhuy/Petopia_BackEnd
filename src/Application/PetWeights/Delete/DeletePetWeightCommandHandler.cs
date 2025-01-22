using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetWeights.Delete;
internal sealed class DeletePetWeightCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext) : ICommandHandler<DeletePetWeightCommand>
{
    public async Task<Result> Handle(DeletePetWeightCommand command, CancellationToken cancellationToken)
    {
        PetWeight? petWeight = await context.PetWeights
            .SingleOrDefaultAsync(p => p.Id == command.PetWeightId, cancellationToken);

        if (petWeight is null)
        {
            return Result.Failure(PetErrors.PetWeightNotFound);
        }

        Pet? pet = await context.Pets
            .FirstOrDefaultAsync(p => p.Id == petWeight.PetId, cancellationToken);

        if (pet is null)
        {
            return Result.Failure(PetErrors.PetNotFound);
        }

        if (pet.OwnerId != userContext.UserId)
        {
            return Result.Failure(UserErrors.Unauthorized);
        }

        context.PetWeights.Remove(petWeight);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
