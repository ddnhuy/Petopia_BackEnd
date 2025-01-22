using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Pets.Update;
internal sealed class UpdatePetCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<UpdatePetCommand>
{
    public async Task<Result> Handle(UpdatePetCommand command, CancellationToken cancellationToken)
    {
        Pet? pet = await context.Pets
            .SingleOrDefaultAsync(p => p.Id == command.Id && p.OwnerId == userContext.UserId, cancellationToken);

        if (pet is null)
        {
            return Result.Failure(PetErrors.PetNotFound);
        }

        pet.Type = command.Type;
        pet.Name = command.Name;
        pet.ImageUrl = command.ImageUrl;
        pet.Description = command.Description;
        pet.BirthDate = command.BirthDate;
        pet.DeathDate = command.DeathDate;
        pet.Gender = command.Gender;
        pet.IsSterilized = command.IsSterilized;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
