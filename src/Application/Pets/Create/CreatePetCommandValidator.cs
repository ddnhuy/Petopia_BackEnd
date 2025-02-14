using FluentValidation;

namespace Application.Pets.Create;
public class CreatePetCommandValidator : AbstractValidator<CreatePetCommand>
{
    public CreatePetCommandValidator()
    {
        RuleFor(p => p.Type)
            .IsInEnum()
            .WithErrorCode("Pets.InvalidPetType")
            .WithMessage("Không tìm thấy loại thú cưng.");
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.ImageUrl).Must(p => p is null or { IsAbsoluteUri: true });
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.BirthDate)
            .NotEmpty()
            .LessThan(DateTime.Now)
            .WithErrorCode("Pets.InvalidPetBirthDate")
            .WithMessage("Ngày sinh phải nhỏ hơn hoặc bằng ngày hiện tại.");
        RuleFor(p => p.Gender)
            .IsInEnum()
            .WithErrorCode("Pets.InvalidPetGender")
            .WithMessage("Không tìm thấy giới tính của thú cưng.");
    }
}
