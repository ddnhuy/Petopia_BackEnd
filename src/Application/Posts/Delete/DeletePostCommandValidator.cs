using FluentValidation;

namespace Application.Posts.Delete;
public class DeletePetCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePetCommandValidator()
    {
        RuleFor(p => p.PostId).NotEmpty();
    }
}
