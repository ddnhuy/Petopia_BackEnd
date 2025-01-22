using FluentValidation;

namespace Application.Pets.Create;
public class CreatePetCommandValidator : AbstractValidator<CreatePetCommand>
{
    public CreatePetCommandValidator()
    {
        RuleFor(p => p.Type)
            .IsInEnum()
            .WithErrorCode("Pets.InvalidPetType")
            .WithMessage("Pet type not found.");
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.ImageUrl).Must(p => p is null or { IsAbsoluteUri: true });
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.BirthDate)
            .NotEmpty()
            .LessThan(DateTime.Now)
            .WithErrorCode("Pets.InvalidPetBirthDate")
            .WithMessage("Birth date must be less than or equal to the current date.");
        RuleFor(p => p.Gender)
            .IsInEnum()
            .WithErrorCode("Pets.InvalidPetGender")
            .WithMessage("Pet gender not found.");
        RuleFor(p => p.IsSterilized).NotEmpty();
    }
}
