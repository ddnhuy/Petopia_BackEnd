using FluentValidation;

namespace Application.PetWeights.Delete;
public class DeletePetWeightCommandValidator : AbstractValidator<DeletePetWeightCommand>
{
    public DeletePetWeightCommandValidator()
    {
        RuleFor(p => p.PetWeightId).NotEmpty();
    }
}
