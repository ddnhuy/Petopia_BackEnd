using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain;
using Domain.Pets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Pets.Delete;
internal sealed class DeletePetCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext,
    IPublisher publisher)
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

        if (pet.ImagePublicId is not null)
        {
            await publisher.Publish(new DeleteEntityHasImageDomainEvent(pet.ImagePublicId), cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
