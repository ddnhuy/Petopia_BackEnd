using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Pets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PetWeights.Delete;
internal sealed class DeletePetWeightCommandHandler(
    IApplicationDbContext context) : ICommandHandler<DeletePetWeightCommand>
{
    public async Task<Result> Handle(DeletePetWeightCommand command, CancellationToken cancellationToken)
    {
        PetWeight? petWeight = await context.PetWeights
            .SingleOrDefaultAsync(p => p.Id == command.PetWeightId, cancellationToken);

        if (petWeight is null)
        {
            return Result.Failure(PetErrors.WeightNotFound);
        }

        context.PetWeights.Remove(petWeight);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
