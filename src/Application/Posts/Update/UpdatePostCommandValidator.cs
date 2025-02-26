using FluentValidation;

namespace Application.Posts.Update;
internal class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.Caption)
            .NotEmpty();

        RuleFor(x => x.HashTag)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ImageUrl)
            .NotEmpty();

        RuleFor(x => x.ImagePublicId)
            .NotEmpty();
    }
}
