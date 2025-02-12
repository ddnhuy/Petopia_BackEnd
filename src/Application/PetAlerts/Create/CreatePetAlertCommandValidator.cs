using FluentValidation;

namespace Application.PetAlerts.Create;
public class CreatePetAlertCommandValidator : AbstractValidator<CreatePetAlertCommand>
{
    public CreatePetAlertCommandValidator()
    {
        RuleFor(x => x.PetId).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Note).NotEmpty();
        RuleFor(x => x.LastSeen).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
    }
}
