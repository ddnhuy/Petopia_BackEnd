using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetWeights.Create;
internal sealed class CreatePetWeightCommandHandler(
    IApplicationDbContext context) : ICommandHandler<CreatePetWeightCommand>
{
    public async Task<Result> Handle(CreatePetWeightCommand command, CancellationToken cancellationToken)
    {
        Pet? pet = await context.Pets
            .FirstOrDefaultAsync(p => p.Id == command.PetId, cancellationToken);

        if (pet is null)
        {
            return Result.Failure(PetErrors.PetNotFound);
        }

        PetWeight petWeight = new(command.PetId, command.Value);

        await context.PetWeights.AddAsync(petWeight, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
