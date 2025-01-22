using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Pets.Delete;
internal sealed class DeletePetCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<DeletePetCommand>
{
    public async Task<Result> Handle(DeletePetCommand request, CancellationToken cancellationToken)
    {
        Pet? pet = await context.Pets
            .SingleOrDefaultAsync(p => p.Id == request.PetId, cancellationToken);

        if (pet is null)
        {
            return Result.Failure(PetErrors.PetNotFound);
        }

        if (pet.OwnerId != userContext.UserId)
        {
            return Result.Failure(PetErrors.PetNotOwned);
        }

        context.Pets.Remove(pet);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
