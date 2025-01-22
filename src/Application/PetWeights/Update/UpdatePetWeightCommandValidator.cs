using FluentValidation;

namespace Application.PetWeights.Update;
public class UpdatePetWeightCommandValidator : AbstractValidator<UpdatePetWeightCommand>
{
    public UpdatePetWeightCommandValidator()
    {
        RuleFor(p => p.PetWeightId).NotEmpty();
        RuleFor(p => p.Value).GreaterThan(0);
    }
}
