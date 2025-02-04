using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Pets.Create;
internal sealed class CreatePetCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<CreatePetCommand>
{
    public async Task<Result> Handle(CreatePetCommand command, CancellationToken cancellationToken)
    {
        if (await context.Pets.AnyAsync(p => p.OwnerId == userContext.UserId && p.Name == command.Name, cancellationToken))
        {
            return Result.Failure(PetErrors.PetAlreadyExists);
        }

        var pet = new Pet(
            userContext.UserId,
            command.Type,
            command.Name,
            command.ImageUrl,
            command.Description,
            command.BirthDate,
            null,
            command.Gender,
            command.IsSterilized,
            command.ImagePublicId);

        context.Pets.Add(pet);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
