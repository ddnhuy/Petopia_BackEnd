using FluentValidation;

namespace Application.PetVaccinations.Update;
public class UpdatePetVaccinationCommandValidator : AbstractValidator<UpdatePetVaccinationCommand>
{
    public UpdatePetVaccinationCommandValidator()
    {
        RuleFor(x => x.PetVaccinationId).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.VaccineName).NotEmpty();
        RuleFor(x => x.Frequency)
            .IsInEnum()
            .WithErrorCode("Pets.InvalidPetVaccinationFrequency")
            .WithMessage("Invalid vaccination frequency.");
    }
}
