using FluentValidation;

namespace Application.Advertisement.Update;
internal class UpdateAdCommandValidator : AbstractValidator<UpdateAdCommand>
{
    public UpdateAdCommandValidator()
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
