using FluentValidation;

namespace Application.Comments.Delete;
public class DeletePetCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeletePetCommandValidator()
    {
        RuleFor(c => c.CommentId).NotEmpty();
    }
}
