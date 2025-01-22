using FluentValidation;

namespace Application.Pets.Delete;
public class DeletePetCommandValidator : AbstractValidator<DeletePetCommand>
{
    public DeletePetCommandValidator()
    {
        RuleFor(p => p.PetId).NotEmpty();
    }
}
