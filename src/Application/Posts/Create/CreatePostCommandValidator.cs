using FluentValidation;

namespace Application.Posts.Create;
internal class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Caption)
            .NotEmpty();

        RuleFor(x => x.ImageUrl)
            .NotEmpty();

        RuleFor(x => x.ImagePublicId)
            .NotEmpty();
    }
}
