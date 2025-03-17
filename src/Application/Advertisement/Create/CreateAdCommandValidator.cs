using FluentValidation;

namespace Application.Advertisement.Create;
internal class CreateAdCommandValidator : AbstractValidator<CreateAdCommand>
{
    public CreateAdCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.TargetUrl)
            .NotEmpty()
            .Must(x => x.IsAbsoluteUri);

        RuleFor(x => x.TotalCost)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .Must(x => x.IsAbsoluteUri);

        RuleFor(x => x.ImagePublicId)
            .NotEmpty();
    }
}
