using FluentValidation;

namespace Application.PetVaccinations.Delete;
public class DeletePetVaccinationCommandValidator : AbstractValidator<DeletePetVaccinationCommand>
{
    public DeletePetVaccinationCommandValidator()
    {
        RuleFor(p => p.PetVaccinationId).NotEmpty();
    }
}
