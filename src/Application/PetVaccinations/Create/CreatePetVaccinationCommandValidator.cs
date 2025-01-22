using FluentValidation;

namespace Application.PetVaccinations.Create;
public class CreatePetVaccinationCommandValidator : AbstractValidator<CreatePetVaccinationCommand>
{
    public CreatePetVaccinationCommandValidator()
    {
        RuleFor(x => x.PetId).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.VaccineName).NotEmpty();
        RuleFor(x => x.Frequency)
            .IsInEnum()
            .WithErrorCode("Pets.InvalidPetVaccinationFrequency")
            .WithMessage("Invalid vaccination frequency.");
    }
}
