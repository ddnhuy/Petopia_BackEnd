using FluentValidation;

namespace Application.PetWeights.Create;
public class CreatePetWeightCommandValidator : AbstractValidator<CreatePetWeightCommand>
{
    public CreatePetWeightCommandValidator()
    {
        RuleFor(p => p.PetId).NotEmpty();
        RuleFor(p => p.Value).GreaterThan(0);
    }
}
